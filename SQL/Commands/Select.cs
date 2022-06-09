using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQL.DBFile;

namespace SQL.Commands
{
    internal class Select : ICommand
    {
        public Select()
        {
            CommandName = "SELECT";
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
            if (!(query[0] == "SELECT" && query[2] == "FROM")) // по идее не надо
                throw new Exception("syntax err");

            var path = query[3] + ".dbf";
            if (!File.Exists(path))
                throw new Exception("FNF");

            var dbf = new Dbf();
            dbf.Read(path);
            var fields = GetQueryFileds(query[1], dbf);
            
            Console.Write("   ");
            foreach (var field in fields) Console.Write(field + '\t');
            Console.Write("\n");

            var condition = "";
            for (var j = 5; j < query.Count; j++) condition += query[j] + " ";


            var i = 1;
            foreach (var record in dbf.Records)
                if (Comparer.WhereCondition(dbf, record, condition))
                {
                    Console.Write("[" + i + "]");
                    foreach (var field in fields) Console.Write(record[field].ToString() + '\t');

                    Console.Write("\n");
                    i++;
                }

            Console.WriteLine(--i + " rows");
        }


        private List<string> GetQueryFileds(string fields, Dbf dbf)
        {
            if (fields == "*")
                return dbf.Fields.Select(x => x.Name).ToList();

            var buff = new List<string>();
            foreach (var field in fields.Split(','))
                if (dbf.Fields.Select(x => x.Name).Contains(field))
                    buff.Add(field);
                else
                    throw new Exception("field not found");
            return buff;
        }
    }
}