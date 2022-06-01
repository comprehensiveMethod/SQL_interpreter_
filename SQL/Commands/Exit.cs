using System.Collections.Generic;

namespace SQL.Commands
{
    public class Exit : ICommand
    {
        public string CommandName { get; }

        public Exit()
        {
            CommandName = "EXIT";
        }

        public void Run(List<string> sqlQuery)
        {
            if (sqlQuery[0] == "EXIT" || sqlQuery[0] == "exit")
            {
               Execute(sqlQuery);
            } 
        }
    
        public void Execute(List<string> query)
        {
            Program.exitFlag = true;
        }
    }
}