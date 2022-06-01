using System;
using System.Collections.Generic;
using System.Text;

namespace SQL.Commands
{
    interface ICommand
    {
        public string CommandName { get;  }

        void Run(List<string> sqlQuery);

        void Execute(List<string> query);
    }
}
