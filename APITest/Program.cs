using EksiAPI;
using EksiAPI.Entities;
using EksiAPI.Services;
using System;
using System.Linq;

namespace APITest
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new ThreadService();
            var f = service.GetFromTopics(ThreadCategory.POPULAR).ToList();
            var thread = service.GetThread(f[23].URL);
            //var s = new EntryService().GetDEBE().ToList();
            Console.ReadKey();
        }
    }
}
