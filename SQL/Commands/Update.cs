using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SQL.DBFile;

namespace SQL.Commands
{
    class Update : ICommand
    {
        public string CommandName { get; }

        public Update()
        {
            CommandName = "Update";
        }

        public void Run(List<string> sqlQuery)
        {
            try
            {
                this.Execute(sqlQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Execute(List<string> query)
        {
            if (!(query[0] == "UPDATE" && query[2] == "SET"))
                throw new Exception("WRONG SYNTAX");

            if (!File.Exists(query[1] + ".dbf"))
                throw new Exception("FILE NOT FOUND");
            
            Dbf dbf = new Dbf();
            dbf.Read(query[1] + ".dbf");
            List<DbfRecord> buff = new List<DbfRecord>();
            var fieldAndVal = query[3].Split('=');

            var updatedCounter = 0;
            if (query.Contains("WHERE")) //если есть where
            {
                var condition = "";
                for (var j = 5; j < query.Count; j++)
                    condition += query[j] + " ";

                foreach (DbfRecord record in dbf.Records)
                {
                    if (Comparer.WhereCondition(dbf, record, condition))
                    {
                        record[fieldAndVal[0]] = fieldAndVal[1];
                        updatedCounter++;
                    }

                    buff.Add(record);
                }
            }
            else
            {
                foreach (DbfRecord record in dbf.Records)
                {
                    record[fieldAndVal[0]] = fieldAndVal[1];
                    updatedCounter++;
                    buff.Add(record);
                }
            }
            
            var num = dbf.Records.Count;
            dbf.Records = buff;
            dbf.Write(query[1] + ".dbf", DbfVersion.dBase4WithMemo);
            Console.WriteLine("updated {0}",updatedCounter);
        }
    }
}