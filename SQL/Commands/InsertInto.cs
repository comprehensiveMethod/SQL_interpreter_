using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SQL.DBFile;

namespace SQL.Commands
{
    class InsertInto : ICommand
    {
        public string CommandName { get; }

        public InsertInto()
        {
            CommandName = "INSERT";
        }

        public void Run(List<string> sqlQuery)
        {
            if (sqlQuery.Contains("INTO"))
            {
                this.Execute(sqlQuery);
            }
        }

        public void Execute(List<string> query)
        {
            string path = query[2] + ".dbf";
            if (!File.Exists(path))
                throw new Exception("wrong sintax");

            foreach (var d in query)
            {
                Console.WriteLine(d);
            }
            if (!(query[4] == "VALUES" && query[0] == "INSERT" && query[1] == "INTO"))
                throw new Exception("wrong sintax");

            query[3] = query[3].Trim(')', '(');
            query[5] = query[5].Trim(')', '(');
            List<string> fields = new List<string>(query[3].Split(',')); //лист полей
            List<string> values = new List<string>(query[5].Split(',')); //лист значений
            if (fields.Count != values.Count) //если неравное кол-во значений  в полях - выходим
                throw new Exception("wrong num field");

            Dbf dbf = new Dbf();
            dbf.Read(path);
          

            DbfRecord dbfRecord = dbf.CreateRecord(); //создаём пустую запись

            for (var i = 0; i < fields.Count; i++)
            {
                dbfRecord[fields[i]] = values[i];
            }
            
            dbf.Write(path,DbfVersion.dBase4WithMemo);
        }
    }
}