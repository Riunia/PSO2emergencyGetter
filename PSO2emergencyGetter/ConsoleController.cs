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
            outputTitle();
            end = false;
            controll = new Controller();
            loop();
        }

        public ConsoleController(string textfile)
        {
            outputTitle();
            end = false;
            controll = new Controller(textfile);
            loop();
        }

        private (string command,List<string> param) commandSepalate(string RawCommand)
        {
            //outputTitle();
            try
            {
                string[] sepalate = RawCommand.Split(' ');

                string outCommand = "";
                List<string> outPara = new List<string>();
                int count = 0;

                foreach (string s in sepalate)
                {
                    if (count == 0)
                    {
                        outCommand = s;
                    }
                    else
                    {
                        outPara.Add(s);
                    }

                    count++;
                }

                return (outCommand, outPara);
            }
            catch
            {
                string outCom = "";
                List<string> lstStr = new List<string>();

                return (outCom,lstStr);
            }
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
            bool run = false;

            if(command == "")
            {
                //outputPronpt();
                return;
            }

            if(command == "end" || command == "quit" || command == "stop" || command == "exit")
            {
                end = true;
                logOutput.writeLog("PSO2emergencyGetterを終了します。");
                run = true;
            }

            if (command == "drop")
            {
                if (param.Count != 0)
                {
                    if (param[0] == "chp")
                    {
                        logOutput.writeLog("覇者の紋章のテーブルを削除します。");
                        controll.dropChpTable();
                        run = true;
                    }

                    if (param[0] == "emg")
                    {
                        logOutput.writeLog("緊急クエストのテーブルを削除します。");
                        controll.dropEmgTable();
                        run = true;
                    }

                    if (param[0] != "chp" && param[0] != "emg")
                    {
                        Console.WriteLine("値が不正です。");
                        run = true;
                    }
                }
                else
                {
                    Console.WriteLine("削除するテーブルを指定してください。");
                    run = true;
                }
            }

            if (command == "create")
            {
                if (param.Count != 0)
                {
                    if (param[0] == "chp")
                    {
                        //logOutput.writeLog("覇者の紋章のテーブルを作成します。");
                        controll.createChpTable();
                        run = true;
                    }

                    if (param[0] == "emg")
                    {
                        //logOutput.writeLog("緊急クエストのテーブルを作成します。");
                        controll.createEmgTable();
                        run = true;
                    }

                    if (param[0] != "chp" && param[0] != "emg")
                    {
                        Console.WriteLine("値が不正です。");
                        run = true;
                    }
                }
                else
                {
                    Console.WriteLine("作成するテーブルを指定してください。");
                    run = true;
                }
            }

            if (command == "clear")
            {
                if (param.Count != 0)
                {
                    if (param[0] == "chp")
                    {
                        //logOutput.writeLog("覇者の紋章のテーブルをクリアします。");
                        controll.clearChpTable();
                        run = true;
                    }

                    if (param[0] == "emg")
                    {
                        //logOutput.writeLog("緊急クエストのテーブルをクリアします。");
                        controll.clearEmgTable();
                        run = true;
                    }

                    if (param[0] != "chp" && param[0] != "emg")
                    {
                        Console.WriteLine("値が不正です。");
                        run = true;
                    }
                }
                else
                {
                    Console.WriteLine("クリアするテーブルを指定してください。");
                    run = true;
                }
            }

            if(command == "get")
            {
                if (param.Count != 0)
                {
                    if (param[0] == "chp")
                    {
                        logOutput.writeLog("覇者の紋章情報を取得します。");
                        Task t = controll.AsyncWriteChpDB();
                        run = true;
                        t.Wait();
                    }

                    if (param[0] == "emg")
                    {
                        logOutput.writeLog("緊急クエストを取得します。");
                        Task t = controll.AsyncWriteEmg();
                        controll.clearEmgTable();
                        run = true;
                        t.Wait();
                    }

                    if (param[0] != "chp" && param[0] != "emg")
                    {
                        logOutput.writeLog("値が不正です。");
                        run = true;
                    }
                }
                else
                {
                    logOutput.writeLog("緊急クエストと覇者の紋章の情報を取得します。");
                    (Task em, Task ch) = controll.getHttp();
                    run = true;
                    em.Wait();
                    ch.Wait();
                }
            }

            if(command == "init")
            {
                Console.WriteLine("初期コマンドを実行します。");
                /*
                controll.dropChpTable();
                controll.dropEmgTable();
                controll.createChpTable();
                controll.createEmgTable();
                (Task em, Task ch) = controll.getHttp();
                em.Wait();
                ch.Wait();
                */
                controll.migration();
                run = true;
            }

            if(command == "version")
            {
                outputVersion();
                run = true;
            }

            if(command == "help")
            {
                outputHelp();
                run = true;
            }

            if(run == false)
            {
                //Console.WriteLine("コマンドが見つかりません。");
                outputHelp();
            }
            else
            {
               // outputPronpt();
            }
        }

        private void outputTitle()
        {
            Console.WriteLine("-----------------------------");
            outputVersion();
            Console.WriteLine("-----------------------------");

        }

        private void outputVersion()
        {
            Console.WriteLine("PSO2EmergencyGetter");
            Console.WriteLine("Version {0}", myFunction.getAssemblyVersion());
            Console.WriteLine("Copyright (c) 2018 Kousokujin.");
            Console.WriteLine("Released under the MIT license.");
        }

        private void outputHelp()
        {
            Console.WriteLine("create [emg|chp] :緊急クエストまたは覇者の紋章のテーブル作成");
            Console.WriteLine("clear [emg|chp] :緊急クエストまたは覇者の紋章のテーブルをクリア");
            Console.WriteLine("drop [emg|chp] :緊急クエストまたは覇者の紋章のテーブル削除");
            Console.WriteLine("get [emg|chp] :緊急クエストまたは覇者の紋章の情報を取得しテーブルに書き込み");
            Console.WriteLine("init :緊急クエストと覇者の紋章の紋章のテーブルをクリアし、テーブルに書き込み");
            Console.WriteLine("chp:覇者の紋章");
            Console.WriteLine("emg:緊急クエスト");
        }
    }
}
