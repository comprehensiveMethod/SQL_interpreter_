using System;
using System.Collections.Generic;

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
                        //select()
                        break;
                    case "DELETE":
                        //delete()
                        break;
                    case "UPDATE":
                        //update()
                        break;
                    case "CREATE":
                        if (sqlQuery[1] == "TABLE")
                        {
                            //createTable()
                        }
                        break;
                    case "INSERT":
                        if (sqlQuery[1] == "INTO")
                        {
                            //insertInto()
                        }
                        break;
                    case "DROP":
                        //drop()
                        break;
                    default:
                        Console.WriteLine("SQL>Syntax Error");
                        break;
                }

            }
            
        }
    }
}
