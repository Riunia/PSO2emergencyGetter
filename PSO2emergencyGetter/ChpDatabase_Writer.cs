using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PSO2emergencyGetter
{
    class ChpDatabase_Writer : AbstractDBWriter
    {
        IChpDataBase chpDB;

        public ChpDatabase_Writer(IChpDataBase chpDB) : base(chpDB,  "PSO2ChpTable")
        {
            this.chpDB = chpDB;
        }

        protected override void setDBtable()
        {
            chpDB.setTable(tablename);
        }

        public int writeDB(List<string> strData)
        {
            chpDB.cleartable();
            string que = chpDB.ChpDataConvertQue(strData);
            object result = chpDB.command(que);

            if(result is int)
            {
                logOutput.writeLog("覇者の紋章のデータベースへの書き込みが失敗しました。");
                return 1;
            }
            else
            {
                logOutput.writeLog("データベースに覇者の紋章の情報を書き込みました。");
                return 0;
            }
        }

        //非同期で実行
        public async Task AsyncWriteDB(List<string> stData)
        {
            await Task.Run(() => {
                writeDB(stData);
            });
        }
    }
}
