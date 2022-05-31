using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SQL.DBFile;
namespace SQL.Commands
{
    class Select : ICommand
    {
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
                                
                                Console.Write(field.Name + "|");
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
                            if(query.Count == 6)
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
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); }



                    }



                }Console.WriteLine("SQL>Table does not exist");
                
                
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
