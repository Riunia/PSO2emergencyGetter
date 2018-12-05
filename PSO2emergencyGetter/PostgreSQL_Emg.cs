using System;
using System.Collections.Generic;
using System.Text;

namespace PSO2emergencyGetter
{
    class PostgreSQL_Emg : postgreSQL,IEventDataBase
    {
        string tablename;

        public PostgreSQL_Emg(string address, string DBname, string user, string password) : base(address, DBname, user, password)
        {
            tablename = "PSO2EventTable";
        }

        public void cleartable()
        {
            logOutput.writeLog("緊急クエストのテーブル内容を削除します。");
            string que = string.Format("truncate table {0} restart identity", tablename);
            command(que);
        }

        public void droptable()
        {
            logOutput.writeLog("緊急クエストのテーブルを削除します。");
            command(string.Format("DROP TABLE {0};", tablename));
        }

        public void createtable()
        {
            logOutput.writeLog("緊急クエストのテーブルを作成します。");
            string queStr = string.Format(
                "CREATE TABLE {0} (ID int primary key,EmgName text,LiveName text,EmgTime timestamp,EmgType int);", 
                tablename);

            command(queStr);
        }

        public void setTable(string t)
        {
            this.tablename = t;
        }

        public string EventDataConvertQue(List<EventData> data)
        {
            if(data.Count == 0)
            {
                logOutput.writeLog("実行するクエリがありません。");
                return "";
            }

            string outQue = string.Format("INSERT INTO {0} (ID, EmgName, LiveName, EmgTime, EmgType) VALUES ",tablename);
            int count = 1;
            string tmpdata = "";

            foreach (EventData ev in data)
            {
                string adddata = "";
                if (ev is emgQuest) {
                    emgQuest emg = ev as emgQuest;


                    adddata += string.Format("('{0}','{1}','{2}','{3}','0')",
                            count.ToString(),
                            myFunction.escapeStr(emg.eventName),
                            myFunction.escapeStr(emg.live),
                            emg.eventTime.ToString());
                }

                if(ev is casino)
                {
                    casino ca = ev as casino;

                    adddata += string.Format("('{0}','{1}','','{2}','1')",
                        count.ToString(),
                        myFunction.escapeStr(ca.eventName),
                        ca.eventTime.ToString());
                }

                if(data.Count != count)
                {
                    adddata += ",";
                }
                else
                {
                    adddata += ";";
                }

                tmpdata += adddata;
                count++;
            }

            outQue += tmpdata;

            //logOutput.writeLog("QUE:{0}", outQue);
            return outQue;
        }
    }
}
