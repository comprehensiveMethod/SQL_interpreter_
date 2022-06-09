using System;
using System.Collections.Generic;
using System.Text;
using SQL.DBFile;
namespace SQL.Commands
{
    class CreateTable : ICommand
    {
        public string CommandName { get; }

        public CreateTable()
        {
            CommandName = "CREATE TABLE";
        }

        public void Run(List<string> sqlQuery)
        {
            try
            {
                if (sqlQuery[1] != "TABLE")
                    throw new Exception("SQL>Syntax Error");
                    this.Execute(sqlQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
       
            }
            
        }

        public void Execute(List<string> query)
        {
            string tableName = query[2];
            query.RemoveAt(0); query.RemoveAt(0); query.RemoveAt(0); //убираем CREATE TABLE name
            query[0] = query[0].Substring(1); //убираем '('
            List<DbfField> fields = new List<DbfField>();
            try {
                for (int i = 0; i < query.Count; i += 2)   //для каждой пары значений name + Fieldtype пихаем в общий лист DbfFields  
                {
                    query[i + 1].Trim(',');
                    query[i + 1].Trim(')');
                    if (query[i + 1].Contains('(') == true)
                    {
                        if (query[i + 1].StartsWith('C') && query[i + 1][1] == '(')
                        {

                            query[i + 1] = query[i + 1].Substring(1);
                            query[i + 1] = query[i + 1].Trim(',');
                            query[i + 1] = query[i + 1].Trim('(', ')');
                            

                            DbfField field = new DbfField(query[i], DbfFieldType.Character, byte.Parse(query[i + 1]));
                            fields.Add(field);

                            continue;
                        }
                        if (query[i + 1].StartsWith('N') && query[i + 1][1] == '(')
                        {
                            query[i + 1] = query[i + 1].Substring(1);
                            query[i + 1] = query[i + 1].Trim('(', ')', ',');
                            List<string> nums = new List<string>(query[i + 1].Split(','));
                            DbfField field = new DbfField(query[i], DbfFieldType.Numeric, byte.Parse(nums[0]), byte.Parse(nums[1]));
                            fields.Add(field);
                            continue;
                        }
                    }
                    if (query[i + 1].StartsWith('D') && query[i + 1].Length == 2)
                    {
                        DbfField field = new DbfField(query[i], DbfFieldType.Date, 10);
                        fields.Add(field);
                        continue;
                    }
                    if (query[i + 1].StartsWith('L') && query[i + 1].Length == 2)
                    {
                        DbfField field = new DbfField(query[i], DbfFieldType.Logical, 1);
                        fields.Add(field);
                        continue;
                    }
                    if (query[i + 1].StartsWith('M') && query[i + 1].Length == 2)
                    {
                        DbfField field = new DbfField(query[i], DbfFieldType.Memo, 20);
                        fields.Add(field);
                        continue;
                    }
                    throw new Exception();



                }
                Dbf dbf = new Dbf();
                foreach (DbfField field1 in fields)
                {
                    dbf.Fields.Add(field1);
                }

                dbf.Write(tableName + ".dbf", DbfVersion.dBase4WithMemo);
                Console.WriteLine("Table created");
            }catch(Exception e)
            {
                Console.WriteLine("SQL>Wrong Syntax");
            }
        }
    }
}

