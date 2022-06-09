using System.Collections.Generic;

namespace SQL.Commands
{
    internal interface ICommand
    {
        public string CommandName { get; }

        void Run(List<string> sqlQuery);

        void Execute(List<string> query);
    }
}