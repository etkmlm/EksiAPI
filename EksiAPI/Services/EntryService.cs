using EksiAPI.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiAPI.Services
{
    public class EntryService : Service<Entry>
    {
        private const string DEBE = "https://eksisozluk.com/debe";
        private const string SEARCH_ENTRY = "https://eksisozluk.com/entry/";

        public override Entry Search(object id) =>
            GetEntriesFromPage(GetHtml(SEARCH_ENTRY + id)).FirstOrDefault();

        public IEnumerable<Entry> GetEntriesFromPage(string url) =>
            GetEntriesFromPage(GetHtml(url));

        internal static IEnumerable<Entry> GetEntriesFromPage(HtmlDocument doc)
        {
            var list = doc.GetElementbyId("entry-item-list");
            if (list != null)
            {
                var title = doc.GetElementbyId("title");
                Thread th = new()
                {
                    Title = title.GetDataAttribute("title").Value,
                    ID = int.TryParse(title.GetDataAttribute("id").Value, out int id) ? id : -1
                };
                foreach (var x in list.SelectNodes(".//li"))
                {
                    var footer = x.SelectSingleNode(".//footer/div[2]/div[1]");
                    bool s = int.TryParse(x.SelectSingleNode(".//footer/div[1]")?.InnerHtml, out int a);
                    string date = footer.SelectSingleNode(".//div[1]/div[2]/a").InnerText;
                    yield return new()
                    {
                        ID = Convert.ToInt32(x.GetDataAttribute("id").Value),
                        Content = x.SelectSingleNode(".//div[1]").InnerHtml.Replace("<br>", "\n"),
                        Writer = new User
                        {
                            Username = footer.SelectSingleNode(".//div[1]/div[1]/div/a").InnerText,
                            Photo = footer.SelectSingleNode(".//div[2]/a/img").GetAttributeValue("src", "")
                        },
                        Date = DateTime.Parse(date.Contains("~") ? date.Split(" ~ ")[0] : date),
                        FavCount = s ? a : 0,
                        Thread = th
                    };
                }
            }
        }

        public IEnumerable<Entry> GetDEBE()
        {
            var page = GetHtml(DEBE);
            var all = page.GetElementbyId("content-body").SelectNodes("//ul/li/a");

            foreach(var x in all)
            {
                if (int.TryParse(x.GetAttributeValue("href", "//-1").Split('/')[^1], out int id) && id != -1)
                    yield return Search(id);
            }
        }
    }
}
