using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PSO2emergencyGetter
{
    class AkiChpGetter : AbstractChampion
    {
        const string getUrl = "https://xpow0wu0s5.execute-api.ap-northeast-1.amazonaws.com/v2";

        public AkiChpGetter(HttpClient hc) : base(getUrl, Encoding.UTF8, hc)
        {

        }

        protected override string getChpFromHttp()
        {
            Task<string> getTask = AsyncHttpGET();
            getTask.Wait();

            return getTask.Result;
        }

        protected override List<string> parser(string str)
        {
            List<string> outputStr = new List<string>();
            JsonChanpion JsonResult = JsonConvert.DeserializeObject<JsonChanpion>(str);

            foreach(string s in JsonResult.TargetList)
            {
                string convert = myFunction.replaceHTMLcharacter(s);
                outputStr.Add(convert);
            }

            return outputStr;

        }
    }

    class JsonChanpion
    {
        [JsonProperty("UpdateTime")]
        public string UpdateTime { get; set; }

        [JsonProperty("TargetList")]
        public List<string> TargetList { get; set; }
    }
}
