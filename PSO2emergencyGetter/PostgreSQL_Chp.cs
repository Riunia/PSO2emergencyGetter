using System;
using System.Collections.Generic;
using System.Text;

namespace PSO2emergencyGetter
{
    class PostgreSQL_Chp : postgreSQL, IChpDataBase
    {
        string tablename;

        public PostgreSQL_Chp(string address, string DBname, string user, string password) : base(address, DBname, user, password)
        {
            tablename = "PSO2ChpTable";
        }

        public void cleartable()
        {
            logOutput.writeLog("覇者の紋章のテーブル内容を削除します。");
            string que = string.Format("truncate table {0} restart identity", tablename);
            command(que);
        }

        public void droptable()
        {
            logOutput.writeLog("覇者の紋章のテーブルを削除します。");
            command(string.Format("DROP TABLE {0};",tablename));
        }

        public void createtable()
        {
            logOutput.writeLog("覇者の紋章のテーブルを作成します。");
            string queStr = string.Format("CREATE TABLE {0} (ID int primary key,ChpName text);", tablename);

            command(queStr);
        }

        public void setTable(string table)
        {
            this.tablename = table;
        }

        public string ChpDataConvertQue(List<string> data)
        {
            string outQue = string.Format("INSERT INTO {0} (ID, ChpName) VALUES ", tablename);
            int count = 1;
            string addQue = "";

            foreach(string d in data)
            {
                string tmpData = string.Format("('{0}','{1}')", count.ToString(), d);

                if(count == data.Count)
                {
                    tmpData += ";";
                }
                else
                {
                    tmpData += ",";
                }

                addQue += tmpData;
                count++;
            }

            outQue += addQue;

            return outQue;
        }
    }
}
