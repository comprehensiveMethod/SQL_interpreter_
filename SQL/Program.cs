using System;
using System.Collections.Generic;
using SQL.Commands;

namespace SQL
{
    internal class Program
    {
        private static Dictionary<string, ICommand> registerCommands()
        {
            var commands = new Dictionary<string, ICommand>();

            commands["CREATE"] = new CreateTable();
            commands["DROP"] = new Drop();
            commands["SELECT"] = new Select();
            commands["DELETE"] = new Delete();
            //commands["TRUNCATE"] = 
            //commands["INSERT"] = 
            //commands["UPDATE"] = ;
            return commands;
        }

        private static void Main(string[] args)
        {
            var commands = registerCommands();


            var exit = false;
            while (exit != true)
            {
                Console.Write("SQL>");
                var sqlCommand = Console.ReadLine();

                if (sqlCommand == "EXIT" || sqlCommand == "exit")
                {
                    exit = true;
                    continue;
                } //чек на выход

                if (sqlCommand == "/?")
                {
                    Console.WriteLine(
                        "COMMANDS: \n SELECT \n DELETE \n UPDATE \n TRUNCATE \n CREATE TABLE \n INSERT INTO \n DROP");
                    continue;
                } //выводит список возможных команд на экран

                var sqlQuery = new List<string>(sqlCommand.Split(' '));

                if (sqlQuery.Contains(sqlQuery[0]))
                    commands[sqlQuery[0]].Run(sqlQuery);
                else
                    Console.WriteLine("SQL>Syntax Error");
            }
        }
    }
}