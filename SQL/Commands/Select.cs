using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Linq;

using SQL.DBFile;

namespace SQL.Commands
{
    
    class Select : ICommand
    {
        public string CommandName { get; }
        public Select()
        {
            CommandName = "SELECT";
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

            if (!(query[0] == "SELECT" && query[2] == "FROM")) // по идее не надо
                throw new Exception("syntax err");

            string path = query[3] + ".dbf";
            if (!File.Exists(path))
                throw new Exception("FNF");
            
            Dbf dbf = new Dbf();
            dbf.Read(path);
            List<string> fields = GetQueryFileds(query[1],dbf);

            
            Console.Write("   ");
            foreach (var field in fields)
            {
                Console.Write(field + '\t' );
            }
            Console.Write("\n");

            var condition ="";
            for (var j = 5; j < query.Count; j++)
            {
                condition += query[j]+" ";
            }

    
            int i = 1;
            foreach (DbfRecord record in dbf.Records)
            {
                if (Comparer.WhereCondition(dbf,record,condition))
                {
                    Console.Write("[" + i + "]");
                    foreach (var field in fields)
                    {
                        Console.Write(record[field].ToString() + '\t');
                    }

                    Console.Write("\n");
                    i++;
                }
            }
            Console.WriteLine(--i + " rows");
     
        }
        
     

        List<string> GetQueryFileds(string fields,Dbf dbf)
        {
            if (fields == "*")
                return dbf.Fields.Select(x => x.Name).ToList();
            
            var buff = new List<string>();
            foreach (var field in fields.Split(','))
            {
                if(dbf.Fields.Select(x=>x.Name).Contains(field))
                    buff.Add(field);
                else
                {
                    throw new Exception("field not found");
                }
            }
            return buff;
        }
    }
}