using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PSO2emergencyGetter
{
    class postgreSQL : AbstractDB,IPostgreSQL
    {
        private NpgsqlConnection connection;

        public postgreSQL(string address, string DBname, string user, string password) : base(address,DBname,user,password)
        {

        }

        public override int connect()
        {
            string connectStr = string.Format("Server={0};Port=5432;User Id={1};Password={2};Database={3};", address, user, password, DBname);
            connection = new NpgsqlConnection(connectStr);

            try
            {
                connection.Open();
            }
            catch (System.Net.Sockets.SocketException)
            {
                logOutput.writeLog("データベースへの接続に失敗しました。");
                return 1;
            }

            logOutput.writeLog("データベースに接続しました。");
            return 0;
        }

        public override int disconnect()
        {
            connection.Close();
            return 0;
        }

        public override object command(string que)
        {
            NpgsqlCommand command = new NpgsqlCommand(que, connection);
            var result = command.ExecuteReader();

            return result;
        }

        public override object ParmCommand(string que, List<object> par)
        {
            List<NpgsqlParameter> paramsList = new List<NpgsqlParameter>();

            foreach (object obj in par)
            {
                if(obj is NpgsqlParameter)
                {
                    NpgsqlParameter np = obj as NpgsqlParameter;
                    paramsList.Add(np);
                }
            }

            var result = ParmCommand(que, paramsList);

            return result;

        }

        public object ParmCommand(string que,List<NpgsqlParameter> param)
        {
            NpgsqlCommand command = new NpgsqlCommand(que, connection);

            foreach (NpgsqlParameter p in param)
            {
                command.Parameters.Add(p);
            }

            return command.ExecuteReader();
        }


    }
}
