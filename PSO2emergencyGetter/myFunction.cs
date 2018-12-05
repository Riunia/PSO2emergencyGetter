using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PSO2emergencyGetter
{
    static class myFunction //クラスにするほどでもない関数群
    {
        static public long convertToUnixTime(DateTime time)    //UNIX時間に変換
        {
            DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime target = time.ToUniversalTime();
            TimeSpan ts = target - UNIX_EPOCH;

            return (long)ts.Seconds;
        }

        static public string getAssemblyVersion()
        {
            System.Diagnostics.FileVersionInfo ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                System.Reflection.Assembly.GetExecutingAssembly().Location);

            string version = ver.ProductVersion;

            return version;
        }

        static public string getLiveEmgStr(emgQuest e, string section = "->")   //クーナライブがある時に使う
        {
            if (e.liveEnable == true)
            {
                if (Regex.IsMatch(e.live, "^クーナスペシャルライブ「.*」") == true) //他のライブの時は無理
                {
                    //もっといい方法がありそう
                    string str = Regex.Replace(e.live, "^クーナスペシャルライブ「", "");
                    str = Regex.Replace(str, "」$", "");
                    return string.Format("{0}{1}{2}", str, section, e.eventName);
                }
                else
                {
                    return string.Format("{0}{1}{2}", e.live, section, e.eventName);
                }
            }

            return e.eventName;
        }

        static public string generateEmgArrStr(List<EventData> evn)
        {
            string output = "";

            foreach (EventData e in evn)
            {
                if (e is emgQuest)
                {
                    string eventStr = getLiveEmgStr((emgQuest)e);
                    output += string.Format("{0} {1}\n", e.eventTime.ToString("HH:mm"), eventStr);
                }
            }

            return output;
        }

        static public string EmgArrStrNumbered(List<EventData> evn)
        {
            string output = "";
            int number = 1;

            foreach (EventData e in evn)
            {
                string eventStr = "";
                if (e is emgQuest)
                {
                    eventStr = getLiveEmgStr((emgQuest)e);
                }
                else
                {
                    eventStr = e.eventName;
                }

                output += string.Format("[{0}] {1} {2}\n", number,e.eventTime.ToString("MM/dd HH:mm"), eventStr);
                number++;
            }

            return output;
        }

        //HTML特殊文字を置き換え（一部）
        static public string replaceHTMLcharacter(string str)
        {
            string output = str;
            output = output.Replace("<br>", ""); //<br>
            //ローマ数字
            output = output.Replace("&#8544;", "I");
            output = output.Replace("&#8545;", "II");
            output = output.Replace("&#8546;", "III");
            output = output.Replace("&#8547;", "IV");
            output = output.Replace("&#8548;", "V");
            output = output.Replace("&#8549;", "VI");
            output = output.Replace("&#8550;", "VII");
            output = output.Replace("&#8551;", "VIII");
            output = output.Replace("&#8552;", "IX");
            output = output.Replace("&#8553;", "X");

            return output;
        }

        //"とか'をエスケープ
        static public string escapeStr(string str)
        {
            string esp_str = str.Replace("'", "");
            //esp_str = esp_str.Replace("\"", "\\\"");

            return esp_str;
        }
    }
}
