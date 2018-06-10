using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PSO2emergencyGetter
{
    class ConfigController
    {
        //データベースの種類
        public const string POSTGRE = "POSTGRE_SQL";
        public const string DEFAULT = "POSTGRE_SQL";    //デフォルト

        //ファイル名
        static public string filename = "config/config.xml";

        //ユーザー名とか
        public string username;
        public string password;
        public string address;
        public string db_name;

        //データベースを格納
        public IChpDataBase chpDB;
        public IEventDataBase eveDB;

        public ConfigController(string username,string password,string address,string db_name,string db)
        {
            this.username = username;
            this.password = password;
            this.address = address;
            this.db_name = db_name;

            if(db == POSTGRE)
            {
                chpDB = new PostgreSQL_Chp(this.address, this.db_name, this.username, this.password);
                eveDB = new PostgreSQL_Emg(this.address, this.db_name, this.username, this.password);
            }
            else
            {
                //とりあえずPostgreSQL
                chpDB = new PostgreSQL_Chp(this.address, this.db_name, this.username, this.password);
                eveDB = new PostgreSQL_Emg(this.address, this.db_name, this.username, this.password);
            }

            saveFile();
        }

        public ConfigController()    //ファイル名から作成
        {
            if (ConfigController.existConfigFile() == true)
            {
                logOutput.writeLog("設定ファイルが見つかりました。");
                object obj;

                saveClass sClass = new saveClass();
                bool result = XmlFileIO.xmlLoad(sClass.GetType(), filename, out obj);

                if (obj is saveClass)
                {
                    sClass = obj as saveClass;

                    this.username = sClass.username;
                    this.password = sClass.password;
                    this.db_name = sClass.db_name;
                    this.address = sClass.address;

                    if (sClass.db == POSTGRE)
                    {
                        chpDB = new PostgreSQL_Chp(this.address, this.db_name, this.username, this.password);
                        eveDB = new PostgreSQL_Emg(this.address, this.db_name, this.username, this.password);
                    }
                    else
                    {
                        chpDB = new PostgreSQL_Chp(this.address, this.db_name, this.username, this.password);
                        eveDB = new PostgreSQL_Emg(this.address, this.db_name, this.username, this.password);
                    }
                }

            }
            else
            {
                logOutput.writeLog("設定ファイルが見つかりません。初期設定をします。");

                Console.Write("Address:");
                this.address = Console.ReadLine();
                Console.Write("Database:");
                this.db_name = Console.ReadLine();
                Console.Write("username:");
                this.username = Console.ReadLine();
                Console.Write("password:");
                this.password = Console.ReadLine();

                //とりあえずPostgreSQL
                chpDB = new PostgreSQL_Chp(this.address, this.db_name, this.username, this.password);
                eveDB = new PostgreSQL_Emg(this.address, this.db_name, this.username, this.password);

                saveFile();
            }
        }

        public void saveFile()
        {
            saveClass sv = new saveClass();
            sv.username = username;
            sv.password = password;
            sv.address = address;
            sv.db_name = db_name;
            sv.db = chpDB.getDBType();

            XmlFileIO.xmlSave(sv.GetType(), filename, sv);
        }

        
        public static bool existConfigFile()
        {
            return File.Exists(filename);
        }
    }

    public class saveClass
    {
        public string username;
        public string password;
        public string address;
        public string db_name;
        public string db;
    }
}
