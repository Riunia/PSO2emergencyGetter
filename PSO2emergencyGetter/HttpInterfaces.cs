using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace PSO2emergencyGetter
{
    interface IAsyncPOST
    {
        //Task<HttpResponseMessage> AsyncHttpPOST(StringContent content);
        Task<string> AsyncHttpPOST(StringContent content);
    }

    interface IAsyncGET
    {
        Task<string> AsyncHttpGET();
    }
}
