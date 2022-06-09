using System;
using System.Collections.Generic;
using System.IO;
using SQL.DBFile;

namespace SQL.Commands
{
    internal class InsertInto : ICommand
    {
        public InsertInto()
        {
            CommandName = "INSERT";
        }

        public string CommandName { get; }

        public void Run(List<string> sqlQuery)
        {
            try
            {
                if (sqlQuery.Contains("INTO")) Execute(sqlQuery);
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
                throw new Exception("wrong sintax");

            if (!(query[4] == "VALUES" && query[0] == "INSERT" && query[1] == "INTO"))
                throw new Exception("wrong sintax");

            query[3] = query[3].Trim(')', '(');
            query[5] = query[5].Trim(')', '(');
            var fields = new List<string>(query[3].Split(',')); //лист полей
            var values = new List<string>(query[5].Split(',')); //лист значений
            if (fields.Count != values.Count) //если неравное кол-во значений  в полях - выходим
                throw new Exception("wrong num field");

            var dbf = new Dbf();
            dbf.Read(path);


            var dbfRecord = dbf.CreateRecord(); //создаём пустую запись

            for (var i = 0; i < fields.Count; i++) dbfRecord[fields[i]] = values[i];

            dbf.Write(path, DbfVersion.dBase4WithMemo);
        }
    }
}