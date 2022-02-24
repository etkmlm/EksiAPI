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
            var f = new EntryService().GetDEBE().ToList();
            Console.ReadKey();
        }
    }
}
