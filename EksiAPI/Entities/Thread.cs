using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiAPI.Entities
{
    public class Thread
    {
        public int ID { get; set; }
        public string URL
        {
            get => "https://eksisozluk.com/" +
            Title.Replace('ğ', 'g')
            .Replace('ü', 'u')
            .Replace('ı', 'i')
            .Replace('ş', 's')
            .Replace('ç', 'c') +
            "--" + ID;
            set 
            {
                var s1 = value.Split('?')[0].Split('/');
                string b = s1[^1];
                var s2 = b.Split("--");
                Title = s2[0];
                ID = int.TryParse(s2[1], out int id) ? id : -1;
            }
        }
        public int PageCount { get; set; }
        public int EntryCount => Entries?.Count() ?? 0;
        public string Title { get; set; }
        public IEnumerable<Entry> Entries { get; set; }
        public DateTime Date { get; set; }
    }
}
