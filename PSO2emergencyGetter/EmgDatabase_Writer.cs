using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PSO2emergencyGetter
{
    class EmgDatabase_Writer : AbstractDBWriter
    {
        public IEventDataBase EventDB;

        public EmgDatabase_Writer(IEventDataBase db) : base(db, "PSO2EventTable")
        {
            EventDB = db;
        }

        protected override void setDBtable()
        {
            EventDB.setTable(tablename);
        }

        public int writeDB(List<EventData> ev)
        {
            string que = EventDB.EventDataConvertQue(ev);
            db.command(que);

            logOutput.writeLog("データベースに緊急クエスト情報を書き込みました。");
            //とりあえず0を返す
            return 0;
        }

        //非同期で実行
        public async Task AsyncWriteDB(List<EventData> ev)
        {
            await Task.Run(() =>
            {
                writeDB(ev);
            });
        }

    }
}
