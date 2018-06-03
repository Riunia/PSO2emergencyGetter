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
            //デバッグ用コード

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


            //覇者の紋章・緊急クエストの取得
            HttpClient hc = new HttpClient();

            AbstractEmgGetter getter = new AkiEmgGetter(hc);
            AbstractChampion chpGetter = new AkiChpGetter(hc);

            Task<List<object>> res = getter.AsyncGetData();
            Task<List<object>> chpRes = chpGetter.AsyncGetData();

            res.Wait();
            chpRes.Wait();

            //データベースへの書き込み
            IEventDataBase DBConnect = new PostgreSQL_Emg(address, db, user, password);
            List<EventData> resEV = getter.ConvertEventData(res.Result);
            EmgDatabase_Writer writer = new EmgDatabase_Writer(DBConnect);
            Task t = writer.AsyncWriteDB(resEV);

            IChpDataBase DB_Hasha = new PostgreSQL_Chp(address, db, user, password);
            List<string> resChp = chpGetter.convertListData(chpRes.Result);
            ChpDatabase_Writer chpWriter = new ChpDatabase_Writer(DB_Hasha);
            Task s = chpWriter.AsyncWriteDB(resChp);

            t.Wait();
            s.Wait();

            Console.ReadLine();
        }
    }
}
