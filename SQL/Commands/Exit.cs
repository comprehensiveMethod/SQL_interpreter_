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
                Program.exitFlag = true;
            } 
        }
    
        public void Execute(List<string> query)
        {
            throw new System.NotImplementedException();
        }
    }
}