using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace PSO2emergencyGetter
{
    abstract class AbstractEmgGetter : AbstractGet
    {
        public AbstractEmgGetter(string url, Encoding enc, HttpClient cl) : base(url, enc, cl)
        {

        }

        protected override List<string> getData()
        {
            //曜日にかかわらず一週間分取得するように変更でもいい気がする

            //取得する緊急クエストの日数を計算
            DateTime dt = DateTime.Now;

            int getDays = 7 - ((int)dt.DayOfWeek + 4) % 7;    //この先の緊急を取得する日数

            if (getDays == 7)   //水曜日の時
            {
                DateTime dt1630 = new DateTime(dt.Year, dt.Month, dt.Day, 17, 00, 0);   //今日の17:00
                if (DateTime.Compare(dt, dt1630) <= 0)
                {
                    getDays = 0;
                }
            }

            List<string> output = new List<string>();

            //for(int i = 0; i < getDays; i++)
            for (int i = 0; i < 7; i++)
            {
                string tmp = getEmgFromHttp(i);
                output.Add(tmp);
            }

            return output;
            
        }

        //実行した日からday日後の緊急クエストを取得(0で今日の緊急クエスト)
        public abstract string getEmgFromHttp(int day);

        //data:1日の緊急クエストのデータ
        override protected List<object> getProcess(List<string> data)
        {
            List<EventData> output = new List<EventData>();

            foreach(string d in data)
            {
                List<EventData> ondayData = ConvertStringToData(d);

                foreach (EventData ev in ondayData)
                {
                    output.Add(ev);
                }
            }

            List<object> outputObj = new List<object>();

            foreach(EventData o in output)
            {
                outputObj.Add(o);
            }
            
            outputlog(output);
            return outputObj;
        }

        //ObjectからEventDataに変換
        public List<EventData> ConvertEventData(List<object> objectData)
        {
            List<EventData> outEvData = new List<EventData>();

            foreach(object o in objectData)
            {
                if(o is EventData)
                {
                    outEvData.Add(o as EventData);
                }
            }

            return outEvData;
        }

        //Httpで取得した文字列をEventDataに変換(dataは1日の緊急クエストの一覧)
        abstract public List<EventData> ConvertStringToData(string data);

        public void outputlog(List<EventData> data) //ログ出力
        {
            string log = "緊急クエストの情報を取得しました。\n";
            int count = 1;

            foreach (EventData ev in data)
            {
                log += string.Format("({0}){1}", ev.eventTime.ToString("MM/dd HH:mm"), ev.eventName);

                if (data.Count != count)
                {
                    log += Environment.NewLine;
                }

                count++;
            }

            logOutput.writeLog(log);
        }
    }
}
