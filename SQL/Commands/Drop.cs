﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SQL.Commands
{
    class Drop : ICommand
    {
        public void Execute(List<string> query)
        {
            query.RemoveAt(0);
            if (query.Count > 1)
            {
                Console.WriteLine("SQL>Wrong syntax");
            }
            else
            {
                string path = query[0] + ".dbf";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                else
                {
                    Console.WriteLine("SQL>Table does not exist");
                }
            }
            
        }
    }
}