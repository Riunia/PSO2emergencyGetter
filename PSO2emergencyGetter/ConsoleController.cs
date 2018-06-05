using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PSO2emergencyGetter
{
    class ConsoleController
    {

        private bool end;   //終了フラグ
        private Controller controll;

        public ConsoleController()
        {
            end = false;
            controll = new Controller();
            loop();
        }

        private (string command,List<string> param) commandSepalate(string RawCommand)
        {
            string[] sepalate = RawCommand.Split(' ');

            string outCommand = "";
            List<string> outPara = new List<string>();
            int count = 0;

            foreach (string s in sepalate)
            {
                if(count == 0)
                {
                    outCommand = s;
                }
                else
                {
                    outPara.Add(s);
                }
            }

            return (outCommand, outPara);

        }

        private void outputPronpt()
        {
            Console.Write("PSO2 Getter >");
        }

        private void loop()
        {
            do
            {
                outputPronpt();
                string inCommand = Console.ReadLine();
                (string command, List<string> param) = commandSepalate(inCommand);
                process(command,param);

            } while (end == false);
        }

        private void process(string command , List<string> param)
        {
            if(command == "end")
            {
                end = true;
                logOutput.writeLog("PSO2emergencyGetterを終了します。");
            }

            if (command == "drop")
            {
                if(param[0] == "chp")
                {
                    logOutput.writeLog("覇者の紋章のテーブルを削除します。");
                    controll.dropChpTable();
                }

                if(param[0] == "emg")
                {
                    logOutput.writeLog("緊急クエストのテーブルを削除します。");
                    controll.dropEmgTable();
                }

                if(param[0] != "chp" && param[0] != "emg")
                {
                    Console.WriteLine("値が不正です。");
                }
            }

            if (command == "create")
            {
                if (param[0] == "chp")
                {
                    logOutput.writeLog("覇者の紋章のテーブルを作成します。");
                    controll.createChpTable();
                }

                if (param[0] == "emg")
                {
                    logOutput.writeLog("緊急クエストのテーブルを作成します。");
                    controll.createEmgTable();
                }

                if (param[0] != "chp" && param[0] != "emg")
                {
                    Console.WriteLine("値が不正です。");
                }
            }

            if (command == "clear")
            {
                if (param[0] == "chp")
                {
                    logOutput.writeLog("覇者の紋章のテーブルをクリアします。");
                    controll.clearChpTable();
                }

                if (param[0] == "emg")
                {
                    logOutput.writeLog("緊急クエストのテーブルをクリアします。");
                    controll.clearEmgTable();
                }

                if (param[0] != "chp" && param[0] != "emg")
                {
                    Console.WriteLine("値が不正です。");
                }
            }

            if(command == "get")
            {
                if (param[0] == "chp")
                {
                    logOutput.writeLog("覇者の紋章情報を取得します。");
                    Task t = controll.AsyncWriteChpDB();
                }

                if (param[0] == "emg")
                {
                    logOutput.writeLog("緊急クエストを取得します。");
                    Task t = controll.AsyncWriteEmg();
                    controll.clearEmgTable();
                }

                if (param[0] != "chp" && param[0] != "emg")
                {
                    logOutput.writeLog("緊急クエストと覇者の紋章の情報を取得します。");
                    (Task em, Task ch) = controll.getHttp();
                }
            }
        }
    }
}
