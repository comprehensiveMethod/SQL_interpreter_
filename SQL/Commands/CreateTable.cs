using System;
using System.Collections.Generic;
using SQL.DBFile;

namespace SQL.Commands
{
    internal class CreateTable : ICommand
    {
        public CreateTable()
        {
            CommandName = "CREATE TABLE";
        }

        public string CommandName { get; }

        public void Run(List<string> sqlQuery)
        {
            try
            {
                if (sqlQuery[1] != "TABLE")
                    throw new Exception("SQL>Syntax Error");
                Execute(sqlQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Execute(List<string> query)
        {
            var tableName = query[2];
            query.RemoveAt(0);
            query.RemoveAt(0);
            query.RemoveAt(0); //убираем CREATE TABLE name
            query[0] = query[0].Substring(1); //убираем '('
            var fields = new List<DbfField>();
            try
            {
                for (var i = 0;
                     i < query.Count;
                     i += 2) //для каждой пары значений name + Fieldtype пихаем в общий лист DbfFields  
                {
                    query[i + 1].Trim(',');
                    query[i + 1].Trim(')');
                    if (query[i + 1].Contains('('))
                    {
                        if (query[i + 1].StartsWith('C') && query[i + 1][1] == '(')
                        {
                            query[i + 1] = query[i + 1].Substring(1);
                            query[i + 1] = query[i + 1].Trim(',');
                            query[i + 1] = query[i + 1].Trim('(', ')');


                            var field = new DbfField(query[i], DbfFieldType.Character, byte.Parse(query[i + 1]));
                            fields.Add(field);

                            continue;
                        }

                        if (query[i + 1].StartsWith('N') && query[i + 1][1] == '(')
                        {
                            query[i + 1] = query[i + 1].Substring(1);
                            query[i + 1] = query[i + 1].Trim('(', ')', ',');
                            var nums = new List<string>(query[i + 1].Split(','));
                            var field = new DbfField(query[i], DbfFieldType.Numeric, byte.Parse(nums[0]),
                                byte.Parse(nums[1]));
                            fields.Add(field);
                            continue;
                        }
                    }

                    if (query[i + 1].StartsWith('D') && query[i + 1].Length == 2)
                    {
                        var field = new DbfField(query[i], DbfFieldType.Date, 10);
                        fields.Add(field);
                        continue;
                    }

                    if (query[i + 1].StartsWith('L') && query[i + 1].Length == 2)
                    {
                        var field = new DbfField(query[i], DbfFieldType.Logical, 1);
                        fields.Add(field);
                        continue;
                    }

                    if (query[i + 1].StartsWith('M') && query[i + 1].Length == 2)
                    {
                        var field = new DbfField(query[i], DbfFieldType.Memo, 20);
                        fields.Add(field);
                        continue;
                    }

                    throw new Exception();
                }

                var dbf = new Dbf();
                foreach (var field1 in fields) dbf.Fields.Add(field1);

                dbf.Write(tableName + ".dbf", DbfVersion.dBase4WithMemo);
                Console.WriteLine("Table created");
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL>Wrong Syntax");
            }
        }
    }
}