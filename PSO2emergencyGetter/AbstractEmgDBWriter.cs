using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PSO2emergencyGetter
{
    abstract class AbstractEmgDBWriter : AbstractDBAccess
    {
        protected string tablename;
        public AbstractEmgDBWriter(AbstractDB db,string tablename) : base(db)
        {
            this.tablename = tablename;
        }

        public int writeDB(List<EventData> ev)
        {
            (string que, List<object> param) = EventDataConvertQue(ev);
            commandNonParam(que, param);

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

        
        public override abstract int connect();
        public override abstract int disconnect();
        public override abstract object command(string que);
        public override abstract object commandNonParam(string que, List<object> parm);

        protected abstract void cleartable();

        //EventDataをクエリ文に変える
        protected abstract (string que, List<object> param) EventDataConvertQue(List<EventData> data);
    }
}
