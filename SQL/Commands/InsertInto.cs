using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SQL.DBFile;

namespace SQL.Commands
{
    class InsertInto : ICommand{
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
            if (File.Exists(path)) //если существует такой файл
            {
                if (query[4] == "VALUES" && query[0] == "INSERT" && query[1] == "INTO")
                {
                    query[3] = query[3].Trim(')', '(');
                    query[5] = query[5].Trim(')', '(');
                    List<string> fields = new List<string>(query[3].Split(',')); //лист полей
                    List<string> values = new List<string>(query[5].Split(',')); //лист значений
                    if (fields.Count != values.Count) //если неравное кол-во значений  в полях - выходим
                    {
                        Console.WriteLine("SQL>Fields are not equals to values");
                        return;
                    }
                    Dbf dbf = new Dbf();
                    dbf.Read(path);
                    foreach(string enteredField in fields) //проверка полей на существование в файле
                    {  
                        bool check = false;
                        foreach(DbfField fileField in dbf.Fields)
                        {
                            if (fileField.Name== enteredField) check = true; //если имя совпало то класс
                        }
                        if (!check) //если нет то ретерн
                        {
                            Console.WriteLine("SQL> No such field");
                            return;
                        }
                    }
                    DbfRecord dbfRecord = dbf.CreateRecord(); //создаём пустую запись
                    for(int i = 0; i <= fields.Count; i++) //напихиваем записи
                    {
                        
                    }



                }
                else //если нет INSERT/INTO/VALUES
                {
                    Console.WriteLine("SQL> Wrong syntax");
                    return;
                }
            }
            else //file not exists
            {
                Console.WriteLine("SQL> Table does not exist");
                return;
            }
        }
    }
}
