using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace PSO2emergencyGetter
{
    abstract class AbstractGet : HttpSocket
    {
        public AbstractGet(string url,Encoding enc,HttpClient cl) : base(url,enc,cl)
        {
            
        }

        //サーバからデータを取ってくる方法を定義
        public abstract List<string> getData();

        //サーバからデータを取ったあとの処理
        public abstract List<EventData> getProcess(List<string> data);

        //非同期処理
        public async Task<List<EventData>> AsyncGetData()
        {
            List<EventData> res = await Task.Run(() =>
            {
                List<string> getStr = getData();
                List<EventData> resData = getProcess(getStr);
                return resData;
            });

            return res;
        }
    }
}
