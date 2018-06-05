using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PSO2emergencyGetter
{
    class postgreSQL : AbstractDB
    {
        //private NpgsqlConnection connection;

        public postgreSQL(string address, string DBname, string user, string password) : base(address,DBname,user,password)
        {

        }

        public override object connect()
        {
            string connectStr = string.Format("Server={0};Port=5432;User Id={1};Password={2};Database={3};", address, user, password, DBname);
            NpgsqlConnection connection = new NpgsqlConnection(connectStr);

            try
            {
                connection.Open();
            }
            catch (System.Net.Sockets.SocketException)
            {
                logOutput.writeLog("データベースへの接続に失敗しました。");
                return 1;
            }
            catch (System.TimeoutException)
            {
                logOutput.writeLog("タイムアウトしました。");
                return 1;
            }

            //logOutput.writeLog("データベースに接続しました。");
            return connection;
        }

        public NpgsqlConnection NpgConnect()
        {
            object objConnect = connect();
            NpgsqlConnection np = objConnect as NpgsqlConnection;

            return np;
        }

        public override int disconnect(object obj)
        {
            if (obj is NpgsqlConnection)
            {
                NpgsqlConnection con = obj as NpgsqlConnection;
                con.Close();
            }
            return 0;
        }

        public override object command(string que)
        {
            NpgsqlConnection con = NpgConnect();
            NpgsqlCommand command = new NpgsqlCommand(que, con);
            if(con != null) {

                try
                {
                    var result = command.ExecuteReader();
                    disconnect(con);
                    return result;
                }
                catch (Npgsql.PostgresException e)
                {
                    logOutput.writeLog("SQLを実行できません。({0})", e.MessageText);
                    return 1;
                }
            }
            else
            {
                logOutput.writeLog("データベースへのクエリの実行ができません。");
                return 1;
            }
        }

        public override object ListParamCommand(string que, List<object> par)
        {
            NpgsqlConnection connection = NpgConnect();
            NpgsqlCommand command = new NpgsqlCommand(que,connection);

            foreach (object obj in par)
            {
                if(obj is NpgsqlParameter)
                {
                    NpgsqlParameter np = obj as NpgsqlParameter;
                    command.Parameters.Add(np);
                }
            }

            var result = command.ExecuteReader();
            disconnect(connection);
            return result;

        }

        public override string getDBType()
        {
            return "POSTGRE_SQL";
        }

    }
}
