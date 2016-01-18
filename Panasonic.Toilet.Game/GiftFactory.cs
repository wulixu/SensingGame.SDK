using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace TronCell.Game
{

    public struct GiftModel
    {
        public string Source;
        public int Score;
        public Rect ColideRect;
        public int TileHeight;
        public int TileWidth;
        public BitmapImage gift;
    }
    public class GiftFactory
    {
        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string SpriteDirectory = "Sprites/";
        private const string SpriteConfig = "Sprites.xml";
        private List<GiftModel> _models = new List<GiftModel>();
        private static GiftFactory factory = new GiftFactory();
        private Random random = new Random();
        public static GiftFactory Instance
        {
            get
            {
                return factory;
            }
        }
        private GiftFactory()
        {
            ReadModels();
        }

        private void ReadModels()
        {
            var appRoot = AppDomain.CurrentDomain.BaseDirectory;
            var basePath = appRoot + SpriteDirectory;
            var filePath = basePath + SpriteConfig;
            try
            {
                var xml = XDocument.Load(filePath);
                var sprites = xml.Element("Sprites");
                if (sprites == null) return;
                foreach (var sprite in sprites.Elements("Sprite"))
                {
                    var source = sprite.Attribute("Source").Value;
                    var colideRect = sprite.Attribute("ColideRect").Value;
                    var rect = colideRect.Split(',');
                    var colide = new Rect(0, 0, 0, 0);
                    if (rect.Length == 4)
                    {
                        colide = new Rect();
                    }
                    else
                    {
                        logger.Error("The ColideRect must have fore parameter.");
                    }
                    var score = int.Parse(sprite.Attribute("Score").Value);
                    var tileHeight = int.Parse(sprite.Attribute("TileHeight").Value);
                    var tileWidth = int.Parse(sprite.Attribute("TileWidth").Value);

                    var giftModel = new GiftModel()
                    {
                        Score = score,
                        Source = basePath + source,
                        ColideRect = colide,
                        TileHeight = tileHeight,
                        TileWidth = tileWidth
                    };
                    
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(giftModel.Source);
                    bitmap.EndInit();
                    giftModel.gift = bitmap;

                    _models.Add(giftModel);
                }
            }
            catch (Exception ex)
            {
                logger.Error("There exist the fatal errors in loading the sprits",ex);
            }
        }


        public GiftModel GetGift(int i)
        {
            return _models[i];

        }

        public int Count
        {
            get {return _models.Count(); } 
        }
    }
}
