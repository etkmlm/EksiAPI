using EksiAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiAPI.Services
{
    public class UserService : Service<User>
    {
        private const string USER = "https://eksisozluk.com/biri/";

        /// <summary>
        /// Search for user.
        /// </summary>
        /// <param name="value">Username.</param>
        /// <returns>It returns a user that matches with query. If not exist, returns null.</returns>
        public override User Search(object value)
        {
            var page = Search("@", value);

            var title = page.GetElementbyId("user-profile-title");
            if (title == null)
                return null;
            var badges = page.GetElementbyId("user-text-badges").SelectNodes(".//li");
            var img = page.GetElementbyId("profile-logo").SelectSingleNode(".//img");
            return new()
            {
                Username = title.InnerText.Trim(),
                IsNoob = badges?.Any(x => x.InnerHtml.Contains("çaylak")) ?? false,
                Photo = img.GetAttributeValue("src", ""),
                EntryCount = Convert.ToInt32(page.GetElementbyId("entry-count-total").InnerText),
                FollowCount = Convert.ToInt32(page.GetElementbyId("user-following-count").InnerText),
                FollowerCount = Convert.ToInt32(page.GetElementbyId("user-follower-count").InnerText)
            };
        }
    }
}
