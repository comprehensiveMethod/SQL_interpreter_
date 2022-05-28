using System;
using System.Collections.Generic;
using SQL.DBFile;
using System.Linq;

namespace SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Dbf dbf = new Dbf();
            dbf.Read("test.dbf");
            string fieldname = "TEST"; //select * from testTable where TEST = HELLO
            string whereWord = "HELLO";
            foreach (DbfField field in dbf.Fields)
            {
                if (fieldname == field.Name)
                {
                    foreach (DbfRecord record in dbf.Records)
                    {
                        if (record[field].ToString() == whereWord)
                        {
                            Console.Write("\n" + record);
                        }

                    }
                }
            }
            string fieldname1 = "TEST"; //select TEST,Lox from testTable where TEST = HELLO or Lox = LOL
            string fieldname2 = "Lox";
            string whereWord1 = "HELLO";
            string whereWord2 = "lol";
            foreach (DbfField field2 in dbf.Fields)
            {
                if (fieldname1 == field2.Name)
                {
                    foreach (DbfField field1 in dbf.Fields)
                    {
                        if (fieldname2 == field1.Name)
                        {
                            foreach(DbfRecord dbfRecord in dbf.Records)
                            {
                                if(dbfRecord[field2].ToString() == whereWord1 || dbfRecord[field1].ToString() == whereWord2)
                                {
                                    Console.WriteLine(dbfRecord);
                                }
                            }
                        }

                    }
                }
            }


            bool exit = false;
            while (exit != true)
            {
                
                Console.Write("SQL>");
                string sqlCommand = Console.ReadLine();

                if (sqlCommand == "EXIT" || sqlCommand == "exit") { exit = true; continue; } //чек на выход

                if (sqlCommand == "/?") { Console.WriteLine("COMMANDS: \n SELECT \n DELETE \n UPDATE \n TRUNCATE \n CREATE TABLE \n INSERT INTO \n DROP"); continue; } //выводит список возможных команд на экран

                List<string> sqlQuery = new List<string>(sqlCommand.Split(' '));
                
                switch (sqlQuery[0])
                {
                    case "SELECT":
                        if (sqlQuery.Contains("FROM"))
                        {
                            //select()
                            break;
                        }
                        
                        break;
                    case "DELETE":
                        if (sqlQuery.Contains("FROM"))
                        {
                            //delete()
                            break;
                        }
                        break;
                    case "UPDATE":
                        if (sqlQuery.Contains("SET"))
                        {
                            //update()
                            break;
                        }
                        Console.WriteLine("SQL>Syntax Error");
                        break;
                    case "CREATE":
                        if (sqlQuery[1] == "TABLE")
                        {
                            //createTable()
                            break;
                        }
                        Console.WriteLine("SQL>Syntax Error");
                        break;
                    case "INSERT":
                        if (sqlQuery[1] == "INTO" && sqlQuery.Contains("VALUES"))
                        {
                            //insertInto()
                            break;
                        }
                        Console.WriteLine("SQL>Syntax Error");
                        break;
                    case "DROP":
                        //drop()
                        break;
                    case "TRUNCATE":
                        //truncate()
                        break;
                    default:
                        Console.WriteLine("SQL>Syntax Error");
                        break;
                }

            }
            
        }
    }
}
