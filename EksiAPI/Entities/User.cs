using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiAPI.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Photo { get; set; }
        public bool IsNoob { get; set; }
        public int FollowerCount { get; set; }
        public int FollowCount { get; set; }
        public int EntryCount { get; set; }
    }
}
