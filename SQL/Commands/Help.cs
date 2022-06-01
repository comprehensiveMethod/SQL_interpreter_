using System;
using System.Collections.Generic;

namespace SQL.Commands
{
    public class Help : ICommand
    {
        public string CommandName { get; }
        
        public Help()
        {
            CommandName = "HELP";
        }
        public void Run(List<string> sqlQuery)
        {
            Console.WriteLine("COMMANDS:");
            foreach (var commands in Program.commands)
            {
                Console.WriteLine(commands.Value.CommandName);
            }
        }

        public void Execute(List<string> query)
        {
            throw new System.NotImplementedException();
        }
    }
}