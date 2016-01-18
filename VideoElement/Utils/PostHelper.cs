using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VideoElement.Utils
{
    public class PostHelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string urlGetQRCodesForReplayScore = ConfigurationManager.AppSettings["GetQRCodesForReplayScore"];

        private HttpClient httpClient;

        public PostHelper()
        {
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(15);
        }


        public  async Task<BitmapSource> UploadFileAndGetQrCode(string fileToUpload)
        {
            if (fileToUpload == null)
            {
                throw new ArgumentException("fileToUpload can not be null!");
            }
            WeixinGameInfo gameInfo = PrepareWeixinGameInfo("结束游戏，获得二维码");
            Type type = typeof(WeixinGameInfo);
            using (MultipartFormDataContent form = new MultipartFormDataContent())
            using (FileStream stream = File.OpenRead(fileToUpload))
            {
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.GetValue(gameInfo) == null) continue;
                    string key = property.Name;

                    string value = property.GetValue(gameInfo).ToString();
                    form.Add(new StringContent(value), key);
                }

                form.Add(new StreamContent(stream), "file", Path.GetFileName(fileToUpload));
                HttpResponseMessage response = await httpClient.PostAsync(urlGetQRCodesForReplayScore, form);
                response.EnsureSuccessStatusCode();
                string jsonstring = await response.Content.ReadAsStringAsync();
                WeixinContract contract = JsonConvert.DeserializeObject<WeixinContract>(jsonstring);


                BitmapSource bitmapSource = await DownloadImageAsync(contract.QRCodesUrl);
                return bitmapSource;
            }

        }

        private async Task<BitmapSource> DownloadImageAsync(string url)
        {
            Stream stream = await httpClient.GetStreamAsync(url);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        private  WeixinGameInfo  PrepareWeixinGameInfo(string gameState)
        {
            WeixinGameInfo weixinDto = new WeixinGameInfo();
            //todo port to config file
            weixinDto.ClientName = "体感拍照";
            weixinDto.GameName = "腾龙医院体感拍照";
            weixinDto.GameState = gameState;
            return weixinDto;
        }




    }

    public class GameInfo
    {
        public string imageUrl { get; set; }
        public string ClientName { get; set; }
        public string GameName { get; set; }

        public string GameState { get; set; }
    }

    public class JsonMessage
    {
            public int Code { get; set; }
            public string Message { get; set; }

            public string FileSize { get; set; }

            public string FileName { get; set; }
        
    }

    public class WeixinGameInfo
    {
        public int Id { get; set; }
        [DisplayName(" 	用户的唯一标识 ")]
        public string Openid { get; set; }

        [DisplayName("图片地址")]
        public string ImagePath { get; set; }

        [DisplayName("游戏状态")]
        public string GameState { get; set; }

        [DisplayName("创建时间")]
        public DateTime? CreateTime { get; set; }

        [DisplayName("结束时间")]

        public DateTime? EndTime { get; set; }

        [DisplayName("游戏得分")]
        public int? Score { get; set; }

        [DisplayName("动态二维码Scene_id")]
        public string Scene_id { get; set; }

        [DisplayName("二维码图片路径")]
        public string QRCodesImg { get; set; }

        [DisplayName("媒体文件ID")]
        public string Media_id { get; set; }

        [DisplayName("描述")]
        public string DescribeInfo { get; set; }

        [DisplayName("客户端装置")]
        public string ClientName { get; set; }

        [DisplayName("游戏名称")]
        public string GameName { get; set; }
        [DisplayName("排队号码")]
        public string QueueNum { get; set; }
        [DisplayName("验证码")]
        public string SecurityCode { get; set; }
        [DisplayName("玩家手机")]
        public string PlayerPhone { get; set; }
        [DisplayName("玩家邮箱")]
        public string PlayerEmail { get; set; }
        public ScanningResult ScanningResult { get; set; }

        public bool IsPlayUsed { get; set; }
        public bool Deleted { get; set; }
    }

    public enum ScanningResult
    {
        gamestart,
        resultreplay
    }

    class WeixinContract
    {
        public string Message { get; set; }

        public string QRCodesUrl { get; set; }
    }

}
