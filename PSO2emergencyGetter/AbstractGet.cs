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
        protected abstract List<string> getData();

        //サーバからデータを取ったあとの処理
        protected abstract List<object> getProcess(List<string> data);

        //非同期処理
        public async Task<List<object>> AsyncGetData()
        {
            List<object> res = await Task.Run(() =>
            {
                List<string> getStr = getData();
                List<object> resData = getProcess(getStr);
                return resData;
            });

            return res;
        }
    }
}
