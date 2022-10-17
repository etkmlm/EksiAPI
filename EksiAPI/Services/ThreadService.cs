using EksiAPI.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiAPI.Services
{
    public class ThreadService : Service<Thread>
    {
        private const string TODAY = "https://eksisozluk.com/basliklar/bugun/";
        private const string POPULAR = "https://eksisozluk.com/basliklar/gundem?p=";

        /// <summary>
        /// Search for thread.
        /// </summary>
        /// <param name="value">Thread Title.</param>
        /// <returns>It returns a thread that matches with query. If not exist, returns null.</returns>
        public override Thread Search(object value) =>
            GetThreadFromPage(Search("", value));

        /// <summary>
        /// Retrieves thread.
        /// </summary>
        /// <param name="url">Thread URL.</param>
        /// <returns>It returns a thread that matches with query. If not exist, returns null.</returns>
        public Thread GetThread(string url) =>
            GetThreadFromPage(GetHtml(url));

        private Thread GetThreadFromPage(HtmlDocument page)
        {
            Thread thread = new();
            var title = page.GetElementbyId("title");
            thread.ID = int.Parse(title.GetDataAttribute("id").Value);
            thread.Title = title.SelectSingleNode(".//a/span").InnerText;

            thread.URL = GetPageUrl();

            IEnumerable<Entry> entries = EntryService.GetEntriesFromPage(GetHtml(thread.URL + "?p=" + 1));

            var last = page.DocumentNode.SelectSingleNode("//div[@class='pager']");
            thread.PageCount = last != null ? int.Parse(last.GetDataAttribute("pagecount").Value) : 1;

            for (int i = 2; i <= thread.PageCount; i++)
                entries = entries.Concat(EntryService.GetEntriesFromPage(GetHtml(thread.URL + "?p=" + i)));

            thread.Entries = entries;

            return thread;
        }

        /// <summary>
        /// Retrieve threads from topics.
        /// </summary>
        /// <param name="category">Thread Category</param>
        /// <param name="pageLimit">Page Limit</param>
        /// <returns></returns>
        public IEnumerable<Thread> GetFromTopics(ThreadCategory category, int pageLimit = 5)
        {
            if (pageLimit < 1)
                pageLimit = 1;

            string url = category switch
            {
                ThreadCategory.POPULAR => POPULAR,
                _ => null
            };

            var page = GetHtml(url + 1);
            int total = int.Parse(page.GetElementbyId("partial-index").SelectSingleNode("//div[1]/h2[1]").GetAttributeValue("title", "0").Split(' ')[0]) / 45;

            IEnumerable<Thread> threads = GetThreadsFromPage(page);
            if (pageLimit <= total)
                for (int i = 1; i <= pageLimit; i++)
                    threads = threads.Concat(GetThreadsFromPage(GetHtml(url + i)));

            return threads;
        }

        private static IEnumerable<Thread> GetThreadsFromPage(HtmlDocument doc)
        {
            var a = doc.DocumentNode.SelectNodes("//ul[@class='topic-list partial']/li/a");
            if (a != null)
                foreach (var x in a)
                    if (!x.InnerHtml.Contains("ad-double-click"))
                        yield return new Thread
                        {
                            URL = x.GetAttributeValue("href", "")
                        };
        }
    }
}
