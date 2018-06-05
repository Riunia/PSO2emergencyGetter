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

        //イベント
        public event EventHandler reloadTime;

        //次の緊急クエストの取得時間
        private DateTime nextTime;

        Task loop;

        public Controller(ConfigController conf)
        {
            this.conf = conf;

            http = new HttpClient();
            emgGet = new AkiEmgGetter(http);
            chpGet = new AkiChpGetter(http);

            emgWriter = new EmgDatabase_Writer(conf.eveDB);
            chpWriter = new ChpDatabase_Writer(conf.chpDB);

            loop = startLoop();
        }

        public Controller() //設定ファイルから（設定ファイルがない場合は作られる。)
        {
            conf = new ConfigController();

            http = new HttpClient();
            emgGet = new AkiEmgGetter(http);
            chpGet = new AkiChpGetter(http);

            emgWriter = new EmgDatabase_Writer(conf.eveDB);
            chpWriter = new ChpDatabase_Writer(conf.chpDB);

            loop = startLoop();
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
                //Console.WriteLine("イベントループ開始");
                writeChpDB();
            });
        }

        public async Task AsyncWriteEmg()
        {
            await Task.Run(()=>{
                writeEmgDB();
            });
        }

        private void setNextGetTime()   //次の緊急クエスト・覇者の紋章の取得時間を設定
        {
            int getDays = 7 - ((int)DateTime.Now.DayOfWeek + 4) % 7;
            if (getDays == 7)   //水曜日の時
            {
                DateTime dt1700 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);   //今日の17:00
                if (DateTime.Compare(DateTime.Now, dt1700) <= 0)
                {
                    getDays = 0;
                }
            }

            nextTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0) + new TimeSpan(getDays, 0, 0, 0);
            logOutput.writeLog(string.Format("次の取得は{0}月{1}日{2}時{3}分です。", nextTime.Month, nextTime.Day, nextTime.Hour, nextTime.Minute));
        }

        public (Task emg,Task chp) getHttp() //緊急クエスト・覇者の紋章の情報をHTTPで取得
        {
            Task t = AsyncWriteChpDB();
            Task s = AsyncWriteEmg();

            setNextGetTime();
            return (t, s);
        }

        private async Task startLoop()  //イベントループの開始
        {
            await Task.Run(()=> {
                eventloop();
                });
        }
        private void eventloop()    //イベントループ
        {
            while (true)
            {
                if((DateTime.Now - nextTime).Seconds > 0)   //緊急クエスト・覇者の紋章の取得時間になった時
                {
                    EventArgs e = new EventArgs();
                    if (reloadTime != null)
                    {
                        reloadTime(this, e);
                    }
                    setNextGetTime();
                }

                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
