using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ServiceLayer.SearchService.Concrete
{
    public class ContentService
    {

        public string GetRss()
        {
            var url = "https://learningenglish.voanews.com/api/zpyp_e-rm_";
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            return feed.Items.First().Title.Text;
        }
    }


}
