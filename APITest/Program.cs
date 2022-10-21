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
            var today = new ThreadService().GetFromTopics(ThreadCategory.TODAY);
            
            Console.ReadKey();
        }
    }
}
