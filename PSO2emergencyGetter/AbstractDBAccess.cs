using System;
using System.Collections.Generic;
using System.Text;

namespace PSO2emergencyGetter
{
    abstract class AbstractDBAccess 
    {
        protected AbstractDB db;

        public AbstractDBAccess(AbstractDB db)
        {
            this.db = db;
            connect();
        }

        public abstract int connect();
        public abstract int disconnect();
        public abstract object command(string que);
        public abstract object commandNonParam(string que, List<object> parm);

        public object commandParam(string que, params object[] param)
        {
            List<object> objList = new List<object>();

            foreach(object o in param)
            {
                objList.Add(o);
            }
            return commandNonParam(que, objList);
        }
    }
}
