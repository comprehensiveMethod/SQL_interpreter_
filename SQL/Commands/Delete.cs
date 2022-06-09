using System;
using System.Collections.Generic;
using System.IO;
using SQL.DBFile;

namespace SQL.Commands
{
    internal class Delete : ICommand
    {
        public Delete()
        {
            CommandName = "DELETE";
        }

        public string CommandName { get; }

        public void Run(List<string> sqlQuery)
        {
            try
            {
                if (!sqlQuery.Contains("FROM"))
                    throw new Exception("syntax err");

                Execute(sqlQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Execute(List<string> query)
        {
            var path = query[2] + ".dbf";
            if (!File.Exists(path))
                throw new Exception("FNF");

            var dbf = new Dbf();
            dbf.Read(path);
            
            var condition = "";
            for (var j = 4; j < query.Count; j++) condition += query[j] + " ";
            var buff = new List<DbfRecord>();

            foreach (var record in dbf.Records)
                if (!Comparer.WhereCondition(dbf, record, condition))
                    buff.Add(record);

            var num = dbf.Records.Count;
            dbf.Records = buff;


            dbf.Write(path, DbfVersion.dBase4WithMemo);
            Console.WriteLine("deleted {0}", num - buff.Count);
        }
    }
}