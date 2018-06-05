using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PSO2emergencyGetter
{
    class Program
    {
        static void Main(string[] args)
        {
            //デバッグ用コード
            /*
            Controller con = new Controller();

            (Task emg, Task chp) = con.getHttp();

            chp.Wait();
            emg.Wait();

            Console.ReadLine();
            */

            ConsoleController cc = new ConsoleController();
        }
    }
}
