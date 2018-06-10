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
            if (args.Length != 0) //引数のファイルから
            {
                ConsoleController cc = new ConsoleController(args[0]);
            }
            else
            {
                ConsoleController cc = new ConsoleController();
            }
            //ConsoleController cc = new ConsoleController(@"config/textconfig.txt");
        }
    }
}
