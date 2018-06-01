using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PSO2emergencyGetter
{
    /*
    class EmgPostgreSQL_Writer : AbstractEmgDBWriter,IPostgreSQL
    {
        postgreSQL DB_postgre;

        public EmgPostgreSQL_Writer(string address,string DB_name,string user,string password) : base(new postgreSQL(address, DB_name, user, password),"PSO2EmgTable")
        {
            DB_postgre = (postgreSQL)db;
        }

        public override int connect()
        {
           return DB_postgre.connect();
        }

        public override int disconnect()
        {
            return DB_postgre.disconnect();
        }

        public override object command(string que)
        {
            return DB_postgre.command(que);
        }

        public override object commandNonParam(string que, List<object> obj)
        {
            List<NpgsqlParameter> npList = new List<NpgsqlParameter>();

            foreach (object o in obj)
            {
                if(o is NpgsqlParameter)
                {
                    npList.Add(o as NpgsqlParameter);
                }
            }

            return ParmCommand(que, npList);
        }

        public object ParmCommand(string que, List<NpgsqlParameter> list)
        {
            return DB_postgre.ParmCommand(que, list);
        }

        protected override void cleartable()
        {
            DB_postgre.command(string.Format("truncate table {0} restart identity",tablename));
        }
        protected override (string que, List<object> param) EventDataConvertQue(List<EventData> data)
        {
            cleartable();

        }
    }
    */
}
