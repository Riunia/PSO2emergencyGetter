using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace PSO2emergencyGetter
{
    class Controller
    {
        ConfigController conf;

        AbstractEmgGetter emgGet;
        AbstractChampion chpGet;
        EmgDatabase_Writer emgWriter;
        ChpDatabase_Writer chpWriter;

        HttpClient http;

        public Controller(ConfigController conf)
        {
            this.conf = conf;

            http = new HttpClient();
            emgGet = new AkiEmgGetter(http);
            chpGet = new AkiChpGetter(http);

            emgWriter = new EmgDatabase_Writer(conf.eveDB);
            chpWriter = new ChpDatabase_Writer(conf.chpDB);
        }

        public Controller() //設定ファイルから（設定ファイルがない場合は作られる。)
        {
            conf = new ConfigController();

            http = new HttpClient();
            emgGet = new AkiEmgGetter(http);
            chpGet = new AkiChpGetter(http);

            emgWriter = new EmgDatabase_Writer(conf.eveDB);
            chpWriter = new ChpDatabase_Writer(conf.chpDB);
        }

        public void writeChpDB()
        {
            Task<List<object>> AsyncData = chpGet.AsyncGetData();
            AsyncData.Wait();
            List<string> data = chpGet.convertListData(AsyncData.Result);
            Task t = chpWriter.AsyncWriteDB(data);
            t.Wait();
        }

        public void writeEmgDB()
        {
            Task<List<object>> AsyncData = emgGet.AsyncGetData();
            AsyncData.Wait();
            List<EventData> data = emgGet.ConvertEventData(AsyncData.Result);
            Task t = emgWriter.AsyncWriteDB(data);
            t.Wait();
        }

        //非同期
        public async Task AsyncWriteChpDB()
        {
            await Task.Run(() => {
                writeChpDB();
            });
        }

        public async Task AsyncWriteEmg()
        {
            await Task.Run(()=>{
                writeEmgDB();
            });
        }
    }
}
