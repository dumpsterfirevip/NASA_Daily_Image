using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace NASA_Daily_Image_Feed
{
    public class NASARSSReader
    {

        public IEnumerable<NASAPost> ReadFeed(string url)
        {
            var rssFeed = XDocument.Load(url);
            
            var posts = from item in rssFeed.Descendants("item")
                        select new NASAPost
                        {
                            Title = item.Element("title").Value,
                            Description = item.Element("description").Value,
                            PublishedDate = item.Element("pubDate").Value,
                            Link = item.Element("link").Value,
                            Enclosure = item.Element("enclosure").Attribute("url").Value
                        };

            return posts;
        }
    }

    public class NASAPost
    {
        public string PublishedDate;
        public string Description;
        public string Title;
        public string Enclosure;
        public string Link;
    }

}
