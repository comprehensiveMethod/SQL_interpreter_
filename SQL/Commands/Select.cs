using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
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
            if (sqlQuery.Contains("FROM"))
            {
                Select select = new Select();
                select.Execute(sqlQuery);
            }
            //что-то сделать если ошибки 
        }

        public void Execute(List<string> query)
        {
            if(query.Count < 4) { Console.WriteLine("SQL>Wrong syntax"); }
            if(query[0] == "SELECT" && query[2] == "FROM")
            {
                string path = query[3] + ".dbf";
                
                if (File.Exists(path))
                {
                    if (query[1] == "*")
                    {
                        Dbf dbf = new Dbf();
                        dbf.Read(path);
                        
                        if (query.Count == 4)
                        {
                            Console.Write("   ");
                            foreach (DbfField field in dbf.Fields)
                            {
                                
                                Console.Write(field.Name + "|" );
                                
                            }
                            Console.Write("\n");
                            int i = 1;
                            foreach (DbfRecord record in dbf.Records)
                            {
                                
                                Console.Write("[" + i + "]");
                                Console.Write(record);
                                Console.Write("\n");
                                i++;
                            }
                            Console.WriteLine(--i + " rows");
                            return;
                        }
                        if(query[4] == "WHERE")
                        {
                            if(query.Count == 6) //select * from table where column=smth
                            {
                                if (query[5].Contains(">="))
                                {
                                    List<string> fieldAndValue = new List<string>(query[5].Split(">="));
                                    if (fieldAndValue.Count != 2)
                                    {
                                        Console.WriteLine("SQL>Wrong syntax");
                                        return;
                                    }
                                    bool fieldExists = false;
                                    string fieldForSwitch = "";
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if(field.Name == fieldAndValue[0])
                                        {
                                            fieldExists = true;
                                            fieldForSwitch = field.Type.ToString();
                                        }
                                        Console.Write(field.Name + "|");
                                    }
                                    if (!fieldExists)
                                    {
                                        Console.WriteLine("SQL>Wrong syntax");
                                    }
                                    switch (fieldForSwitch)
                                    {
                                        case "Numeric":
                                            double first = double.Parse(fieldAndValue[1]);
                                            foreach (DbfField field in dbf.Fields)
                                            {
                                                Console.Write(field.Name + "|");
                                            }
                                            Console.Write("\n");
                                            int b = 1;
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                double second = double.Parse(record[fieldAndValue[1]].ToString());
                                                if (first == second)
                                                {
                                                    Console.Write("[" + b + "]");
                                                    Console.Write(record);
                                                    Console.Write("\n");
                                                    b++;
                                                }

                                            }
                                            Console.WriteLine(--b + " rows");
                                            return;
                                            break;
                                        case "Logical":
                                            if(fieldAndValue[1] != "Y" || fieldAndValue[1] != "N")
                                            {
                                                Console.WriteLine("SQL>Wrong syntax");
                                                return;
                                            }
                                            foreach(DbfField field in dbf.Fields)
                                            {
                                                Console.Write(field.Name + "|");
                                            }
                                            Console.Write("\n");
                                            int i = 1;
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                if(fieldAndValue[1] == "Y")
                                                {
                                                    if (record[fieldAndValue[0]].ToString() == "Y")
                                                    {
                                                        Console.Write("[" + i + "]");
                                                        Console.Write(record);
                                                        Console.Write("\n");
                                                        i++;
                                                    }
                                                }
                                                if (fieldAndValue[1] == "N")
                                                {
                                                    if (record[fieldAndValue[0]].ToString() == "N")
                                                    {
                                                        Console.Write("[" + i + "]");
                                                        Console.Write(record);
                                                        Console.Write("\n");
                                                        i++;
                                                    }
                                                }

                                            }
                                            Console.WriteLine(--i + " rows");
                                            return;
                                            break;
                                        case "Character":
                                            foreach (DbfField field in dbf.Fields)
                                            {
                                                Console.Write(field.Name + "|");
                                            }
                                            Console.Write("\n");
                                            int j = 1;
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                if (fieldAndValue[1] == record[fieldAndValue[0]].ToString())
                                                {
                                                    Console.Write("[" + j + "]");
                                                    Console.Write(record);
                                                    Console.Write("\n");
                                                    j++;
                                                }

                                            }
                                            Console.WriteLine(--j + " rows");
                                            return;
                                            break;
                                        case "Date":
                                            Console.WriteLine("I cant compare Dates");
                                            return;                                            
                                            break;
                                        case "Memo":
                                            foreach (DbfField field in dbf.Fields)
                                            {
                                                Console.Write(field.Name + "|");
                                            }
                                            Console.Write("\n");
                                            int m = 1;
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                if (fieldAndValue[1] == record[fieldAndValue[0]].ToString())
                                                {
                                                    Console.Write("[" + m + "]");
                                                    Console.Write(record);
                                                    Console.Write("\n");
                                                    m++;
                                                }

                                            }
                                            Console.WriteLine(--m + " rows");
                                            return;
                                            break;
                                        default:
                                            break;
                                    }
                                   

                                }

                                
                                if (query[5].Contains("<="))
                                {

                                }
                                if (query[5].Contains('='))
                                {

                                }
                                if (query[5].Contains('<'))
                                {

                                }
                                if (query[5].Contains('>'))
                                {

                                }
                            }
                            if(query.Count == 8)
                            {

                            }
                        }

                    }
                    else
                    {
                        List<string> fields = new List<string>(query[1].Split(','));
                       
                        Dbf dbf = new Dbf();
                        dbf.Read(path);
                        try
                        {
                            foreach (DbfField field in dbf.Fields) //проверка на наличие полей
                            {
                                bool equals = false;
                                
                                foreach (string listField in fields)
                                {
                                    if (field.Name == listField)
                                    {
                                        equals = true;
                                    }
                                }
                                if (equals == false) throw new Exception("SQL>No such field in table");   //если не нашлось за цикл по листу то кидаем ошибку
                            }
                            
                            foreach (var field in fields)
                            {
                                Console.Write(field+"\t");
                            }
                            Console.WriteLine();
                            foreach (var record in dbf.Records)
                            {
                                foreach (var field in fields)
                                {
                                    Console.WriteLine(record[field]+"\t");
                                }
                                Console.WriteLine();
                            }
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); }
                        
                    }
                       Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("SQL>Table does not exist");
                }
                
                
                
            }
        }
    }

    //Dbf dbf = new Dbf();
    //dbf.Read("test.dbf");
    //foreach(DbfField field in dbf.Fields)
    //{
    //    Console.Write(field.Name+ "|");
    //}
    //string fieldname = "TEST"; //select * from testTable where TEST = HELLO
    //string whereWord = "HELLO";
    //foreach (DbfField field in dbf.Fields)
    //{
    //    if (fieldname == field.Name)
    //    {
    //        foreach (DbfRecord record in dbf.Records)
    //        {
    //            if (record[field].ToString() == whereWord)
    //            {
    //                Console.Write("\n" + record);
    //            }

    //        }
    //    }
    //}
    //string fieldname1 = "TEST"; //select TEST,Lox from testTable where TEST = HELLO or Lox = LOL
    //string fieldname2 = "Lox";
    //string whereWord1 = "HELLO";
    //string whereWord2 = "lol";
    //foreach (DbfField field2 in dbf.Fields)
    //{
    //    if (fieldname1 == field2.Name)
    //    {
    //        foreach (DbfField field1 in dbf.Fields)
    //        {
    //            if (fieldname2 == field1.Name)
    //            {
    //                foreach(DbfRecord dbfRecord in dbf.Records)
    //                {
    //                    if(dbfRecord[field2].ToString() == whereWord1 || dbfRecord[field1].ToString() == whereWord2)
    //                    {
    //                        Console.WriteLine(dbfRecord);
    //                    }
    //                }
    //            }

    //        }
    //    }
    //}

}
