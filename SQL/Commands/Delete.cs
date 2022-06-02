using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQL.DBFile;

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
            try
            {
                if (!sqlQuery.Contains("FROM"))
                    throw new Exception("syntax err");

                this.Execute(sqlQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Execute(List<string> query)
        {
            string path = query[2] + ".dbf";
            if (!File.Exists(path))
                throw new Exception("FNF");
            
            Dbf dbf = new Dbf();
            dbf.Read(path);



            var condition ="";
            for (var j = 4; j < query.Count; j++)
            {
                condition += query[j]+" ";
            }
            List<DbfRecord> buff = new List<DbfRecord>();
            
            foreach (DbfRecord record in dbf.Records)
            {
                if (!Comparer.WhereCondition(dbf,record,condition))
                {
                    buff.Add(record);
                }
            }

            var num = dbf.Records.Count;
            dbf.Records = buff;

           
            dbf.Write(path, DbfVersion.dBase4WithMemo);
            Console.WriteLine("deleted {0}", num-buff.Count);
        }
        
    }
}
