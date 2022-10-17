using EksiAPI.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiAPI.Services
{
    /// <summary>
    /// The base service for API.
    /// </summary>
    /// <typeparam name="T">Base service type.</typeparam>
    public abstract class Service<T>
    {
        public const string SEARCH = "https://eksisozluk.com/?q=";
        private readonly HtmlWeb web;

        public Service()
        {
            web = new()
            {
                AutoDetectEncoding = true,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:100.0) Gecko/20100101 Firefox/100.0"
            };
        }

        /// <summary>
        /// Get any web page.
        /// </summary>
        /// <param name="url">URL of web page.</param>
        /// <returns>Web page in HtmlDocument.</returns>
        protected HtmlDocument GetHtml(string url) =>
            web.Load(url);

        /// <summary>
        /// Base search function.
        /// </summary>
        /// <param name="value">Query parameter.</param>
        /// <returns>Search result.</returns>
        public abstract T Search(object value);

        protected HtmlDocument Search(string prefix, object value) =>
            GetHtml(SEARCH + prefix + value);

        protected string GetPageUrl() =>
            web.ResponseUri.AbsoluteUri;
    }
}
