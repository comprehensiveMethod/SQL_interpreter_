using System;
using System.Collections.Generic;
using System.Text;

namespace SQL.Commands
{
    class InsertInto : ICommand{
        public string CommandName { get; }

        public InsertInto()
        {
            CommandName = "INSERT";
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
