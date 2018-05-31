using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PSO2emergencyGetter
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string user;
            string password;
            string db;
            string address;

            Console.Write("Address:");
            address = Console.ReadLine();
            Console.Write("Database:");
            db = Console.ReadLine();
            Console.Write("username:");
            user = Console.ReadLine();
            Console.Write("password:");
            password = Console.ReadLine();

            AbstractDB DBConnect = new postgreSQL(address, db, user, password);
            */

            HttpClient hc = new HttpClient();

            AbstractEmgGetter getter = new AkiEmgGetter(hc);
            AbstractChampion chpGetter = new AkiChpGetter(hc);

            Task<List<object>> res = getter.AsyncGetData();
            Task<List<object>> chpRes = chpGetter.AsyncGetData();

            res.Wait();
            chpRes.Wait();
            Console.ReadLine();
        }
    }
}
