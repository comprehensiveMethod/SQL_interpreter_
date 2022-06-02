using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SQL.DBFile;
namespace SQL.Commands
{
    class Update : ICommand
    {
        public string CommandName => throw new NotImplementedException();
        public void Run(List<string> sqlQuery)
        {
            this.Execute(sqlQuery);
        }
        public void Execute(List<string> query)
        {
            if (query[0] == "UPDATE" && query[2] == "SET")
            {
                if (File.Exists(query[1] + ".dbf"))
                {
                    if (query.Contains("WHERE")) //если есть where
                    {

                    }
                    if (query.Count == 4) //если UPDATE table SET column=smth
                    {
                        List<string> fieldAndValue = new List<string>(query[3].Split('='));
                        Dbf dbf = new Dbf();
                        dbf.Read(query[1] + ".dbf");
                        bool fieldExists = false;
                        DbfFieldType fieldType = DbfFieldType.Character;
                        foreach (DbfField field in dbf.Fields)
                        {
                            if (fieldAndValue[0] == field.Name)
                            {
                                fieldExists = true;
                                fieldType = field.Type;
                            }
                        }
                        if (!fieldExists)
                        {
                            Console.WriteLine("SQL> No such field");
                        }
                        switch (fieldType) //находка по полю и установка значений
                        {
                            case DbfFieldType.Character:
                                
                                foreach (DbfRecord record in dbf.Records)
                                {
                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                }
                                Console.WriteLine("Updated");
                                dbf.Write(query[1] + ".dbf", DbfVersion.dBase4WithMemo);
                                return;
                                
                            case DbfFieldType.Date:
                                foreach (DbfRecord record in dbf.Records)
                                {
                                    try
                                    {
                                        record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                    }catch(Exception e)
                                    {
                                        Console.WriteLine("Cant parse this");
                                        return;
                                    }
                                }
                                Console.WriteLine("Updated");
                                dbf.Write(query[1] + ".dbf", DbfVersion.dBase4WithMemo);
                                return;
                                
                            case DbfFieldType.Logical:
                                foreach (DbfRecord record in dbf.Records)
                                {
                                    try
                                    {
                                        record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Cant parse this");
                                        return;
                                    }
                                }
                                Console.WriteLine("Updated");
                                dbf.Write(query[1] + ".dbf", DbfVersion.dBase4WithMemo);
                                return;
                                
                            case DbfFieldType.Memo:
                                foreach (DbfRecord record in dbf.Records)
                                {
                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                }
                                Console.WriteLine("Updated");
                                dbf.Write(query[1] + ".dbf", DbfVersion.dBase4WithMemo);
                                return;
                                
                            case DbfFieldType.Numeric:
                                foreach (DbfRecord record in dbf.Records)
                                {
                                    try
                                    {
                                        record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Cant parse this");
                                        return;
                                    }
                                }
                                Console.WriteLine("Updated");
                                dbf.Write(query[1] + ".dbf", DbfVersion.dBase4WithMemo);
                                return;
                                
                        }
                        

                    }
                }
            }
        }


    }
}
