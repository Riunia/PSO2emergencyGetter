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
            string que = string.Format("truncate table {0} restart identity", tablename);
            command(que);
        }

        public void setTable(string t)
        {
            this.tablename = t;
        }

        public string EventDataConvertQue(List<EventData> data)
        {
            string outQue = string.Format("INSERT INTO {0} (ID, EmgName, LiveName, EmgTime, EmgType) VALUES ",tablename);
            int count = 1;
            string tmpdata = "";

            foreach (EventData ev in data)
            {
                string adddata = "";
                if (ev is emgQuest) {
                    emgQuest emg = ev as emgQuest;
                    if (emg.liveEnable == false)
                    {
                        adddata += string.Format("('{0}','{1}','{2}','{3}','0')",
                            count.ToString(),
                            emg.eventName,
                            0,
                            emg.eventTime.ToString());
                    }
                    else
                    {
                        adddata += string.Format("('{0}','{1}','{2}','{3}','0')",
                           count.ToString(),
                           emg.eventName,
                           emg.live,
                           emg.eventTime.ToString());
                    }
                }

                if(ev is casino)
                {
                    casino ca = ev as casino;

                    address += string.Format("('{0}','{1}','','{2}','1')",
                        count.ToString(),
                        ca.eventName,
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

            logOutput.writeLog("QUE:{0}", outQue);
            return outQue;
        }
    }
}
