using System;
using System.Net.Http;

namespace PSO2emergencyGetter
{
    class Program
    {
        static void Main(string[] args)
        {
            string user;
            string password;
            string db;
            string address;

            Console.Write("Address:");
            address = Console.ReadLine();
            Console.Write("Database:");
            db = Console.ReadLine();
            Console.Write("username:");
            user = Console.ReadLine();
            Console.Write("password:");
            password = Console.ReadLine();

            AbstractDB DBConnect = new postgreSQL(address, db, user, password);
            Console.ReadLine();
        }
    }
}
