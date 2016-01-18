using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TronCell.Game.ViewModel
{
    public class GameViewModel
    {
        public static string GameDataPath = "gamedata";
        public ObservableCollection<ScoreRankViewModel> RankScoreCollection { get; set; } = new ObservableCollection<ScoreRankViewModel>();

        public void AddNewScore(int score, string headImage)
        {
            int count = RankScoreCollection.Count();
            ScoreRankViewModel newRank = new ScoreRankViewModel
            {
                Score = score,
                HeadImage = headImage,
                CreateTime = DateTime.Today.ToString()
            };

            if (count == 0)
            {
                newRank.Rank = 1;
                RankScoreCollection.Add(newRank);
                return;
            }

            int rankIndex = -1;
            for(int i = 0; i < count; i++)
            {
                if(RankScoreCollection[i].Score <= score)
                {
                    rankIndex = i;
                    break;
                }
            }

            if(rankIndex !=-1 && count < 5)
            {
                RankScoreCollection.Insert(rankIndex, newRank);
            }
            else if(rankIndex != -1 && count >= 5)
            {
                RankScoreCollection.Insert(rankIndex, newRank);
                RankScoreCollection.RemoveAt(count);
            }
            else if(rankIndex == -1 && count < 5)
            {
                RankScoreCollection.Add(newRank);
            }
            else
            {
                Console.WriteLine("分数进不了前5");
            }
            UpdateRank();
            Task.Factory.StartNew(() => 
            {
                SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(AppDomain.CurrentDomain.BaseDirectory + GameDataPath);
                db.Insert(newRank);
            });
            
        }

        private void UpdateRank()
        {
            int count = RankScoreCollection.Count();
            for(int i = 0;i < count; i++)
            {
                RankScoreCollection[i].Rank = i + 1;
            }
        }

        public GameViewModel()
        {
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(AppDomain.CurrentDomain.BaseDirectory + GameDataPath);
            db.CreateTable<ScoreRankViewModel>();
        }


        public void LoadTopFive()
        {
            string loadHistory = ConfigurationManager.AppSettings["LoadHistoryData"];
            if (loadHistory == "0")
                return;
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(AppDomain.CurrentDomain.BaseDirectory + GameDataPath);

            if (loadHistory == "1")
            {
                string today = DateTime.Today.ToString();
                foreach (var r in db.Table<ScoreRankViewModel>()
                                .Where(r => r.CreateTime == today)
                                .OrderByDescending(r => r.Score)
                                .Take(5))
                {
                    RankScoreCollection.Add(r);
                }
            }
            else if(loadHistory == "2")
            {
                foreach (var r in db.Table<ScoreRankViewModel>()
                                .OrderByDescending(r => r.Score)
                                .Take(5))
                {
                    RankScoreCollection.Add(r);
                }
            }
            
           UpdateRank();
            
        }
    }
}
