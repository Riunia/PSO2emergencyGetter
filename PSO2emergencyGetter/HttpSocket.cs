using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace PSO2emergencyGetter
{
    abstract class HttpSocket:IAsyncGET,IAsyncPOST
    {
        public string url;
        protected Encoding encode;
        protected HttpClient hc;

        public HttpSocket(string url,Encoding enc,HttpClient cl)
        {
            this.url = url;
            this.encode = enc;
            this.hc = cl;
        }

        public async Task<string> AsyncHttpPOST(StringContent content)
        {
            if (url != null)
            {
                try
                {
                    HttpResponseMessage respons = await hc.PostAsync(url, content);
                    string resMes = await respons.Content.ReadAsStringAsync();
                    return resMes;

                }
                catch (HttpRequestException)
                {
                    logOutput.writeLog("POSTに失敗しました。");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<string> AsyncHttpGET()
        {
            try
            {
                HttpResponseMessage mes = await hc.GetAsync(url);
                string resMes = await mes.Content.ReadAsStringAsync();

                return resMes;
            }
            catch (HttpRequestException)
            {
                logOutput.writeLog("GETに失敗しました。");
                return null;
            }
        }
    }
}
