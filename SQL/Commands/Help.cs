using System;
using System.Collections.Generic;

namespace SQL.Commands
{
    public class Help : ICommand
    {
        public Help()
        {
            CommandName = "HELP";
        }

        public string CommandName { get; }

        public void Run(List<string> sqlQuery)
        {
            try
            {
                Execute(sqlQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Execute(List<string> query)
        {
            Console.WriteLine("COMMANDS:");
            foreach (var commands in Program.commands) Console.WriteLine(commands.Value.CommandName);
        }
    }
}