using System;
using System.Collections.Generic;
using SQL.Commands;

namespace SQL
{
    internal class Program
    {
        public static bool exitFlag = false;
        public static readonly Dictionary<string, ICommand> commands = registerCommands();

        private static Dictionary<string, ICommand> registerCommands()
        {
            var commands = new Dictionary<string, ICommand>();

            commands["CREATE"] = new CreateTable();
            commands["DROP"] = new Drop();
            commands["SELECT"] = new Select();
            commands["DELETE"] = new Delete();
            commands["EXIT"] = new Exit();
            commands["exit"] = commands["EXIT"];
            commands["/?"] = new Help();
            commands["INSERT"] = new InsertInto();
            commands["UPDATE"] = new Update();
            return commands;
        }

        private static void Main(string[] args)
        {
            while (exitFlag != true)
            {
                Console.Write("SQL>");
                var sqlCommand = Console.ReadLine();
                var sqlQuery = new List<string>(sqlCommand.Split(' '));

                if (commands.ContainsKey(sqlQuery[0]))
                    commands[sqlQuery[0]].Run(sqlQuery);
                else
                    Console.WriteLine("SQL>Syntax Error");
            }
        }
    }
}