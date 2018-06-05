using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PSO2emergencyGetter
{
    abstract class AbstractDBWriter
    {
        protected string tablename;
        public IDatabase db;

        public AbstractDBWriter(IDatabase db,string tablename)
        {
            this.db = db;
            this.tablename = tablename;
            //db.connect();
        }

        //データベースにテーブルを設定する方法
        abstract protected void setDBtable();
    }
}
