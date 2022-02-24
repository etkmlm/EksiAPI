using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiAPI.Entities
{
    public class Entry
    {
        public int ID { get; set; }
        public User Writer { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int FavCount { get; set; }
        public Thread Thread { get; set; }
    }
}
