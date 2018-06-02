using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
