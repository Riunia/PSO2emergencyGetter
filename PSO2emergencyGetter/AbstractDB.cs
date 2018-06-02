using System;
using System.Collections.Generic;
using System.Text;

namespace PSO2emergencyGetter
{
    abstract class AbstractDB : IDatabase
    {
        protected string address;
        protected string DBname;
        protected string user;
        protected string password;

        public AbstractDB(string address,string DBname,string user,string password)
        {
            this.address = address;
            this.DBname = DBname;
            this.user = user;
            this.password = password;

            //connect();
        }

        //データベースへの接続
        public abstract object connect();

        //データベースから切断
        public abstract int disconnect(object obj);

        //Queryの実行
        public abstract object command(string que);

        //パラメータを指定してQuery実行
        public abstract object ListParamCommand(string que, List<object> par);

        public object ParamCommand(string que,params object[] par)
        {
            List<object> objList = new List<object>();

            foreach(object o in par)
            {
                objList.Add(o);
            }

            return ListParamCommand(que, objList);
        }

    }
}
