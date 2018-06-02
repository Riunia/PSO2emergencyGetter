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

            IEventDataBase DBConnect = new PostgreSQL_Emg(address, db, user, password);

            HttpClient hc = new HttpClient();

            AbstractEmgGetter getter = new AkiEmgGetter(hc);
            AbstractChampion chpGetter = new AkiChpGetter(hc);

            Task<List<object>> res = getter.AsyncGetData();
            Task<List<object>> chpRes = chpGetter.AsyncGetData();

            res.Wait();
            chpRes.Wait();

            List<EventData> resEV = getter.ConvertEventData(res.Result);

            EmgDatabase_Writer writer = new EmgDatabase_Writer(DBConnect);
            Task t = writer.AsyncWriteDB(resEV);
            t.Wait();
            Console.ReadLine();
        }
    }
}
