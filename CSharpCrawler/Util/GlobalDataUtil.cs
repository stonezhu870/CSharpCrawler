﻿using CSharpCrawler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CSharpCrawler.Util
{
    public class GlobalDataUtil
    {
        private const string ConfigPath = "./config/0_0.xml";

        private static object obj = new object();
        private static GlobalDataUtil _instance;

        private ConfigStruct crawlerConfig;

        internal ConfigStruct CrawlerConfig
        {
            get
            {
                return crawlerConfig;
            }

            private set
            {
                crawlerConfig = value;
            }
        }

        public static GlobalDataUtil GetInstance()
        {
            if(_instance == null)
            {
                lock(obj)
                {
                    if (_instance == null)
                        _instance = new GlobalDataUtil();
                }
            }
            return _instance;
        }

        public GlobalDataUtil()
        {
            CrawlerConfig = LoadConfig();
        }

        private ConfigStruct LoadConfig()
        {
            ConfigStruct configStruct = new ConfigStruct();

            FetchUrlConfig urlConfig = new FetchUrlConfig()
            {
                Depth = "1",
                IgnoreUrlCheck = false
            };
            FetchImageConfig imageConfig = new FetchImageConfig()
            {
                Depth = "1",
                IgnoreUrlCheck = false,
                MaxResolution = "0",
                MinResolution = "0",
                MaxSize = 0,
                MinSize = 0
            };

            try
            {
                XDocument doc = XDocument.Load(ConfigPath);
                XElement eleUrl = doc.Root.Element("FetchUrl");
                XElement eleImage = doc.Root.Element("FetchImage");

                urlConfig.Depth = eleUrl.Element("Depth").Value;
                urlConfig.IgnoreUrlCheck = Convert.ToBoolean(eleUrl.Element("IgnoreUrlCheck").Value);

                imageConfig.Depth = eleImage.Element("Depth").Value;
                imageConfig.IgnoreUrlCheck = Convert.ToBoolean(eleImage.Element("IgnoreUrlCheck").Value);
                imageConfig.MaxResolution = eleImage.Element("MaxResolution").Value;
                imageConfig.MinResolution = eleImage.Element("MinResolution").Value;
                imageConfig.MinSize = Convert.ToInt32(eleImage.Element("MinSize").Value);
                imageConfig.MaxSize = Convert.ToInt32(eleImage.Element("MaxSize").Value);
            }
            catch(Exception ex)
            {
                
            }

            configStruct.ImageConfig = imageConfig;
            configStruct.UrlConfig = urlConfig;

            return configStruct;
        }
    }
}