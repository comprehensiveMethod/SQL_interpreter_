using System;
using System.Collections.Generic;
using SQL.DBFile;
using System.Linq;
using SQL.Commands;

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
                            Select select = new Select();
                            select.Execute(sqlQuery);
                            break;
                        }
                        
                        break;
                    case "DELETE":
                        if (sqlQuery.Contains("DELETE"))
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
                            CreateTable createTable = new CreateTable();
                            createTable.Execute(sqlQuery);
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
                        Drop drop = new Drop();
                        drop.Execute(sqlQuery);
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
