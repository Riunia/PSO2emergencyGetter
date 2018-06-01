using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PSO2emergencyGetter
{
    interface IDatabase
    {
        int connect();
        int disconnect();
        object ListParamCommand(string que, List<object> par);
        object ParamCommand(string que, params object[] par);
        object command(string que);
    }

    //EventDataのデータをDBに書き込むためのインターフェイス
    interface IEventDataBase : IDatabase
    {
        (string que, List<object> param) EventDataConvertQue(List<EventData> data);
        void cleartable();
        void setTable(string tablename);
    }
}
