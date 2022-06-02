using System;
using System.Collections.Generic;
using System.Text;

namespace SQL.Commands
{
    class Delete : ICommand
    {
        public string CommandName { get; }

        public Delete()
        {
            CommandName = "DELETE";
        }

        public void Run(List<string> sqlQuery)
        {
            throw new NotImplementedException();
        }

        public void Execute(List<string> query)
        {
            throw new NotImplementedException();
            
        }
    }
}
