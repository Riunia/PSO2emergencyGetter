using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace PSO2emergencyGetter
{
    abstract class AbstractChampion : AbstractGet
    {
        public AbstractChampion(string url, Encoding enc, HttpClient cl) : base(url, enc, cl)
        {
        }

        protected override List<string> getData()
        {
            List<string> outputStr = new List<string>();

            string fromHttp = getChpFromHttp();
            outputStr.Add(fromHttp);

            return outputStr;
        }

        protected override List<object> getProcess(List<string> data)
        {
            List<string> stringList = new List<string>();

            foreach(string d in data)
            {
                List<string> outPars = parser(d);
                foreach(string o in outPars)
                {
                    stringList.Add(o);
                }
            }

            List<object> output = new List<object>();

            foreach(string d in stringList)
            {
                output.Add(d);
            }

            outputLog(stringList);
            return output;
        }

        //サーバから覇者の紋章情報を取ってくる方法
        protected abstract string getChpFromHttp();

        //データをパース
        protected abstract List<string> parser(string str);

        //ログ出力
        protected void outputLog(List<string> list)
        {
            string log = "覇者の紋章キャンペーン情報を取得しました。以下の通りです。";

            int count = 1;
            foreach(string s in list)
            {
                log += s;
                if(count != list.Count)
                {
                    log += Environment.NewLine;
                }
                count++;
            }

            logOutput.writeLog(log);
        }

    }
}
