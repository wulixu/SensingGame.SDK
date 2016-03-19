using Newtonsoft.Json;
using SensngGame.ClientSDK;
using SensngGame.ClientSDK.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LuckDraw2
{
    /// <summary>
    /// Interaction logic for LuckDrawControl.xaml
    /// </summary>
    public partial class LuckDrawControl : UserControl
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private GameServiceClient m_gameService;
        private int m_lastUserCount = 0;
        private bool m_longPulling = false;

        private List<UserActionData> m_activityUsers = null;
        private List<UserActionData> m_winners = new List<UserActionData>();
        private List<AwardData> m_awards = null;
        private Dictionary<int, WhiteListItem> m_whiteList = null;
        
        private AppState m_currentState = AppState.ScanQrcode;
        private AwardData m_currentAward = null;
        private DispatcherTimer m_timer = new DispatcherTimer();
        private Random m_rnd = new Random();


        public LuckDrawControl()
        {
            InitializeComponent();
           // m_gameService = new GameServiceClient("j;lajdf;jaiuefjf", "wx37e46819d148d5fb", "2", "6");
            m_timer.Interval = TimeSpan.FromMilliseconds(100);
            m_timer.Tick += Tick;
          
        }

        public void SetGameService(GameServiceClient gameService)
        {
            m_gameService = gameService;
        }

        public void LoadData()
        {
            LoadActivityUsers();
            LoadAwardList();
            LoadWhiteList();
        }

        public void ShowAward(int awardLevel)
        {
            if (m_currentState == AppState.ShowAward)
                return;

            if(m_awards.Count() == 0)
            {
                MessageBox.Show("所有奖品已抽完");
                return;
            }
            m_currentAward = m_awards.Where(a => a.AwardSeq == awardLevel).FirstOrDefault();
            if(m_currentAward == null)
            {
                MessageBox.Show("该奖项不存在");
                return;
            }
            if(m_currentAward.PlanQty <=0 || m_currentAward.PlanQty - m_currentAward.ActualQty <=0 )
            {
                MessageBox.Show("该奖品已抽完");
                return;
            }
            
            awardImage.Source = new BitmapImage(new Uri(m_currentAward.AwardImagePath));
            awardText.Text = m_currentAward.Name;
            GoToState(AppState.ShowAward);
        }

        public void Begin()
        {
            if(m_currentState == AppState.ShowAward)
            {
                m_timer.Start();
                GoToState(AppState.Gaming);
            }
            else if(m_currentState == AppState.ShowWinner)
            {
                if(m_currentAward.PlanQty > 0)
                {
                    m_timer.Start();
                    GoToState(AppState.Gaming);
                }
                else
                {
                    MessageBox.Show("该奖项已抽完");
                }
            }
            else if (m_currentState == AppState.Gaming)
            {
                m_timer.Stop();
                UserActionData winner = null;
                var candidateIds = m_activityUsers.Select(u => u.Id).Except(m_winners.Select(u => u.Id));

                if(candidateIds.Count() == 0)
                {
                    MessageBox.Show("所有用户抽过了");
                    return;
                }

                if (m_whiteList != null && m_whiteList.ContainsKey(m_currentAward.Id))
                {
                    var whiteUsersIds = m_whiteList[m_currentAward.Id].Users;
                    var candidateIdsWhite = candidateIds.Intersect(whiteUsersIds);
                    if(candidateIdsWhite.Count() > 0)
                    {
                        winner = m_activityUsers.First(u => u.Id == candidateIdsWhite.RandomGet());
                    }
                }
                if(winner == null)
                {
                    winner = m_activityUsers.First(u => u.Id == candidateIds.RandomGet());
                }
                SetWinner(winner, m_currentAward);
                GoToState(AppState.ShowWinner);
            }
        }

        int m_tickIndex = 0;
        private void Tick(object sender, EventArgs e)
        {
            if (m_tickIndex >= m_activityUsers.Count)
                m_tickIndex = 0;
            SetWinnerImage(m_activityUsers[m_tickIndex]);
            m_tickIndex++;
        }

        private void SetWinner(UserActionData winner, AwardData award)
        {
            m_currentAward.PlanQty--;
            m_winners.Add(winner);
            SetWinnerImage(winner);
            Task.Factory.StartNew(() => {
               var userAwardResult = m_gameService.WinAwardByUser(award.Id.ToString(), winner.Id.ToString()).Result;
                if (userAwardResult.Data == null)
                {
                    Console.WriteLine("WinAwardByUser " + userAwardResult.ErrMessage);
                }
                else
                {
                    Console.WriteLine("WinAwardByUser" + userAwardResult.Data.Nickname + "win " + userAwardResult.Data.AwardID);
                }
            });
        }

        private void SetWinnerImage(UserInfoData winner)
        {
            winnerImage.Source = new BitmapImage(new Uri(winner.Headimgurl, UriKind.Absolute));
            winnerName.Text = winner.Nickname;
        }


        private void LoadWhiteList()
        {
            var fetchWhiteListTask = Task.Factory.StartNew<List<WhiteUserData>>(() => 
            {
                WhiteUsersResult whiteUserResult = null;
                int tryCount = 0;
                while (tryCount < 5)
                {
                    whiteUserResult = m_gameService.GetActivityWhiteListUsers().Result;
                    if (whiteUserResult == null)
                        continue;
                    if(whiteUserResult.Data == null)
                    {
                        log.Error("GetActivityWhiteListUser " + whiteUserResult.ErrMessage);
                        continue;
                    }
                    return whiteUserResult.Data;
                }
                return null;
            });
            fetchWhiteListTask.ContinueWith((t) =>
            {
                List<WhiteUserData> whiteList = t.Result;
                if(whiteList == null)
                {
                    MessageBox.Show("获取白名单失败");
                    return;
                }
                m_whiteList = new Dictionary<int, WhiteListItem>();
                foreach(var u in whiteList)
                {
                    if (u.AwardSeqs == null || u.AwardSeqs == "")
                        continue;

                    var awardIds = u.AwardSeqs.Split(new char[] { ','});
                    foreach(var awardId in awardIds)
                    {
                        int id = int.Parse(awardId);
                        if (!m_whiteList.ContainsKey(id))
                        {
                            m_whiteList.Add(id, new WhiteListItem());
                        }
                        m_whiteList[id].Users.Add(u.Id);
                    }
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void LoadAwardList()
        {
           var fetchAwardListTask =  Task.Factory.StartNew<List<AwardData>>(() => 
            {
                AwardsResult awardsResult;
                int tryCount=0 ;
                while (tryCount < 5)
                {
                    awardsResult = m_gameService.GetAwardsByActivity().Result;
                    tryCount++;
                    if (awardsResult == null)
                        continue;
                    if(awardsResult.Data == null)
                    {
                        log.Error("GetAwardsByActivity" + awardsResult.ErrMessage);
                        continue;
                    }
                    var appRoot = AppDomain.CurrentDomain.BaseDirectory;
                    var awardDir = appRoot + "award/";
                    if (!Directory.Exists(awardDir)) Directory.CreateDirectory(awardDir);

                    foreach(var award in awardsResult.Data)
                    {
                        if (award.AwardImagePath == null)
                        {
                            log.Error("奖品图片为空");
                            continue;
                        }
                        award.AwardImagePath = GameServiceClient.ServerBase + award.AwardImagePath;
                        var dest = awardDir + award.AwardImagePath.GetHashCode() + ".png";
                        if (File.Exists(dest))
                        {
                            award.AwardImagePath = dest;
                            continue;
                        }
                        if(DownloadImage(award.AwardImagePath, dest))
                        {
                            award.AwardImagePath = dest;
                        }
                    }
                    return awardsResult.Data;
                }
                return null;
            });

            fetchAwardListTask.ContinueWith((t) => 
            {
                List<AwardData> awardDataList = t.Result;
                if (awardDataList == null)
                {
                    MessageBox.Show("获取奖器列表失败");
                    return;
                }
                m_awards = awardDataList;

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void LoadActivityUsers()
        {
            var fetchActivityUsersTask = Task.Factory.StartNew<List<UserActionData>>(() =>
            {
                UserActionsResult userActionsResult = null;
                int tryCount = 0;
                while (tryCount < 5)
                {
                    userActionsResult = m_gameService.GetUsersByActivityAndGame(50).Result;
                    if (userActionsResult == null)
                        continue;
                    if (userActionsResult.Data == null)
                    {
                        log.Error("GetUsersByActivitiy " + userActionsResult.ErrMessage);
                        continue;
                    }
                    var appRoot = AppDomain.CurrentDomain.BaseDirectory;
                    var headDir = appRoot + "head/";
                    if (!Directory.Exists(headDir)) Directory.CreateDirectory(headDir);

                    foreach (var u in userActionsResult.Data)
                    {
                        var dest = headDir + u.Headimgurl.GetHashCode() + ".png";
                        if (File.Exists(dest))
                        {
                            u.Headimgurl = dest;
                            continue;
                        }
                        if (DownloadImage(u.Headimgurl, dest))
                        {
                            u.Headimgurl = dest;
                        }
                    }
                    return userActionsResult.Data;
                }
                return null;
            });
            fetchActivityUsersTask.ContinueWith((t) =>
            {
                List<UserActionData> activityUsers = t.Result;
                if (activityUsers == null)
                {
                    MessageBox.Show("获取活动用户失败");
                    return;
                }
                if(activityUsers.Count == 0)
                {
                    MessageBox.Show("无游戏活动用户");
                }
                m_activityUsers = activityUsers;

                wall.ClearTiles();
                usersCountText.Text = m_activityUsers.Count.ToString();

                foreach (var usr in m_activityUsers)
                {
                    wall.AddTile(usr.Id, usr.Headimgurl);
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private bool DownloadImage(string url, string dest)
        {
            WebClient webClient = new WebClient();
            try
            {
                var tmp = dest + ".downloading";
                webClient.DownloadFile(url, tmp);
                File.Move(tmp, dest);
                return true;
            }
            catch (Exception) { }
            return false;
        }

        public void ScanCheckIn(string url)
        {
            wall.AddTile(1,url);
            if (m_activityUsers == null)
                m_activityUsers = new List<UserActionData>();
            m_activityUsers.Add(new UserActionData {
                Headimgurl = url,
                Nickname = "Name " + url.GetHashCode(),
                Id = url.GetHashCode()
            });
            usersCountText.Text = m_activityUsers.Count.ToString();
        }      

        private void GoToState(AppState state)
        {
            qrcodePanel.Visibility = state == AppState.ScanQrcode ? Visibility.Visible : Visibility.Hidden;
            awardPanel.Visibility = state != AppState.ScanQrcode ? Visibility.Visible : Visibility.Hidden;
            winnerPanel.Visibility = state == AppState.ShowWinner || state == AppState.Gaming ? Visibility.Visible : Visibility.Hidden;
            m_currentState = state;
            if(m_currentState != AppState.ScanQrcode)
            {
                m_longPulling = false;
            }
        }

    }

    enum AppState
    {
        ScanQrcode,
        ShowAward,
        Gaming,
        ShowWinner
    }

    class WhiteListItem 
    {
        public WhiteListItem()
        {
            Users = new List<int>();
        }
        public List<int> Users { get; set; }
    }

    class CheckInQrcode
    {
        public string Id { get; set; }
        public string FileName { get; set; }
    }

    public static class ListExtension
    {
        public static T RandomGet<T>(this IEnumerable<T> list)
        {
            if (list.Count() == 0)
                return default(T);
            Random rnd = new Random();
            return list.ElementAt(rnd.Next(list.Count()));
        }

    }

}
