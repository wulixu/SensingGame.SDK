
using Sensing.SDK.Contract;

namespace SensingAds.ViewBanner.AdsItem
{
    public class AdsFactory
    {
        public static BaseAd CreateAd(AdsSdkModel ad)
        {
            string fileType = ad.Type.ToLower();
            int durtion = (int)(ad.TimeSpan?.TotalSeconds ?? 0);
            if (ad.IsCustom && !string.IsNullOrEmpty(ad.CustomContent))
            {
                return CustomAd.Parse(ad.CustomContent, ad.Id, ad.Name, durtion);
            }
            else if (fileType == "image")
            {
                return new ImageAd { FileUrl = ad.FileUrl, Id = ad.Id, Name = ad.Name, TimeSpan = durtion };
            }
            else if (fileType == "video")
            {
                return new VideoAd { FileUrl = ad.FileUrl, Id = ad.Id, Name = ad.Name, TimeSpan = durtion };
            }
            else if (fileType == "web")
            {
                return new H5Ad { FileUrl = ad.FileUrl, Id = ad.Id, DownloadMode = DownloadMode.Self, Name = ad.Name, TimeSpan = durtion };
            }
            else if (fileType == "pdf")
            {
                return new PdfAd { FileUrl = ad.FileUrl, Id = ad.Id, Name = ad.Name, TimeSpan = durtion };
            }
            else if (fileType == "windows")
            {
                return new WindowAppAd { FileUrl = ad.FileUrl, Id = ad.Id, Name = ad.Name, TimeSpan = durtion };
            }
            else if (fileType == "other")
            {
                return new H5Ad { FileUrl = ad.FileUrl, Id = ad.Id, DownloadMode = DownloadMode.None, Name = ad.Name, TimeSpan = durtion };
            }
            else
            {
                return new NotSupportAd { FileUrl = ad.FileUrl, Id = ad.Id, Name = ad.Name, TimeSpan = durtion };
            }
        }
    }
}
