using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PSO2emergencyGetter
{
    class AkiEmgGetter : AbstractEmgGetter
    {
        const string getUrl = "https://akakitune87.net/api/v4/pso2emergency";

        public AkiEmgGetter(HttpClient hc) : base(getUrl, Encoding.UTF8, hc)
        {

        }

        public override string getEmgFromHttp(int day)
        {
            DateTime getEmgTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            getEmgTime += new TimeSpan(day, 0, 0, 0, 0);

            //JSONを生成
            sendjson_eventgetter jsonData = new sendjson_eventgetter();
            jsonData.EventDate = getEmgTime.ToString("yyyyMMdd");
            string sendStr = JsonConvert.SerializeObject(jsonData, Formatting.Indented);

            //HTTP_POST
            StringContent sc = new StringContent(sendStr, encode, "application/json");

            Task<string> resultHTTP;
            try
            {
                resultHTTP = AsyncHttpPOST(sc);
                resultHTTP.Wait();

                return resultHTTP.Result;

            }
            catch (System.NullReferenceException)
            {
                logOutput.writeLog("緊急クエストの取得に失敗しました。");

                return "";
            }
        }

        public override List<EventData> ConvertStringToData(string data)
        {
            //Jsonをパース
            List<JsonPSO2Event> EVData = new List<JsonPSO2Event>();
            EVData = JsonConvert.DeserializeObject<List<JsonPSO2Event>>(data);

            List<EventData> output = new List<EventData>();

            //出力するEventDataを作成

            //ライブ関係
            bool live = false;
            string livename = "";

            foreach (JsonPSO2Event ev in EVData)
            {
                DateTime emgDT = new DateTime(DateTime.Now.Year, ev.Month, ev.Date, ev.Hour, ev.Minute, 0);

                if (ev.EventType == "緊急")
                {
                    emgQuest emg;
                    if (live == true)
                    {
                        emg = new emgQuest(emgDT, ev.EventName, livename);
                        live = false;
                        livename = "";
                    }
                    else
                    {
                        emg = new emgQuest(emgDT, ev.EventName);
                    }
                    output.Add(emg);
                }

                if (ev.EventType == "ライブ")
                {
                    live = true;
                    livename = ev.EventName;
                }

                if (ev.EventType == "カジノイベント")
                {
                    casino cas = new casino(emgDT);
                    output.Add(cas);
                }
            }

            return output;
        }
    }

    class sendjson_eventgetter
    {
        [JsonProperty("EventDate")]
        public string EventDate { get; set; }

    }

    class JsonPSO2Event
    {
        public string EventName { get; set; }
        public string EventType { get; set; }
        public int Month { get; set; }
        public int Date { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
