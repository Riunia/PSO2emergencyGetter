﻿using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PSO2emergencyGetter
{
    interface IDatabase
    {
        object connect();
        int disconnect(object obj);
        object ListParamCommand(string que, List<object> par);
        object ParamCommand(string que, params object[] par);
        object command(string que);
        string getDBType();
    }

    //EventDataのデータをDBに書き込むためのインターフェイス
    interface IEventDataBase : IDatabase
    {
        string EventDataConvertQue(List<EventData> data);
        void cleartable();
        void droptable();
        void createtable();
        void setTable(string tablename);
    }

    interface IChpDataBase : IDatabase
    {
        string ChpDataConvertQue(List<string> data);
        void cleartable();
        void droptable();
        void createtable();
        void setTable(string tablename);
    }
}
