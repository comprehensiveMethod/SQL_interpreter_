using System;
using System.Collections.Generic;
using System.IO;

namespace SQL.Commands
{
    internal class Drop : ICommand
    {
        public Drop()
        {
            CommandName = "DROP";
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
            query.RemoveAt(0);
            if (query.Count > 1)
            {
                Console.WriteLine("SQL>Wrong syntax");
            }
            else
            {
                var path = query[0] + ".dbf";
                if (File.Exists(path))
                    File.Delete(path);
                else
                    Console.WriteLine("SQL>Table does not exist");
            }
        }
    }
}