using System;
using System.Collections.Generic;
using SQL.DBFile;

namespace SQL
{
    class Program
    {
        static void Main(string[] args)
        {
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
