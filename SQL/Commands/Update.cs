﻿using System;
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
                        if (query.Contains("AND"))
                        {
                            Console.WriteLine("Here AND");
                            if(query[5].Contains(">") && query[7].Contains(">"))
                            {
                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('>'));
                                List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('>'));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                bool fieldExistsWhere2 = false;
                                DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere2[0] == field.Name)
                                    {
                                        fieldExistsWhere2 = true;
                                        fieldTypeWhere2 = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere2)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }

                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }



                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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
                                    case DbfFieldType.Date:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere,record,fieldTypeWhere,'>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            if (query[5].Contains(">") && query[7].Contains("<"))
                            {
                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('>'));
                                List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('<'));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                bool fieldExistsWhere2 = false;
                                DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere2[0] == field.Name)
                                    {
                                        fieldExistsWhere2 = true;
                                        fieldTypeWhere2 = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere2)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }

                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }



                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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
                                    case DbfFieldType.Date:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            if (query[5].Contains(">") && query[7].Contains("=")){
                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('>'));
                                List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('='));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                bool fieldExistsWhere2 = false;
                                DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere2[0] == field.Name)
                                    {
                                        fieldExistsWhere2 = true;
                                        fieldTypeWhere2 = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere2)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }

                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }



                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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
                                    case DbfFieldType.Date:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            if (query[5].Contains("<") && query[7].Contains(">"))
                            {
                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('<'));
                                List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('>'));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                bool fieldExistsWhere2 = false;
                                DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere2[0] == field.Name)
                                    {
                                        fieldExistsWhere2 = true;
                                        fieldTypeWhere2 = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere2)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }

                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }



                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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
                                    case DbfFieldType.Date:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            if (query[5].Contains("<") && query[7].Contains("<"))
                            {
                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('<'));
                                List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('<'));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                bool fieldExistsWhere2 = false;
                                DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere2[0] == field.Name)
                                    {
                                        fieldExistsWhere2 = true;
                                        fieldTypeWhere2 = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere2)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }

                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }



                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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
                                    case DbfFieldType.Date:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            if (query[5].Contains("<") && query[7].Contains("="))
                            {
                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('<'));
                                List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('='));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                bool fieldExistsWhere2 = false;
                                DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere2[0] == field.Name)
                                    {
                                        fieldExistsWhere2 = true;
                                        fieldTypeWhere2 = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere2)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }

                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }



                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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
                                    case DbfFieldType.Date:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            if (query[5].Contains("=") && query[7].Contains(">"))
                            {
                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('='));
                                List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('>'));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                bool fieldExistsWhere2 = false;
                                DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere2[0] == field.Name)
                                    {
                                        fieldExistsWhere2 = true;
                                        fieldTypeWhere2 = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere2)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }

                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }



                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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
                                    case DbfFieldType.Date:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            if (query[5].Contains("=") && query[7].Contains("<"))
                            {
                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('='));
                                List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('<'));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                bool fieldExistsWhere2 = false;
                                DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere2[0] == field.Name)
                                    {
                                        fieldExistsWhere2 = true;
                                        fieldTypeWhere2 = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere2)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }

                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }



                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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
                                    case DbfFieldType.Date:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            if (query[5].Contains("=") && query[7].Contains("="))
                            {
                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('='));
                                List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('='));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                bool fieldExistsWhere2 = false;
                                DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere2[0] == field.Name)
                                    {
                                        fieldExistsWhere2 = true;
                                        fieldTypeWhere2 = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere2)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }

                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }



                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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
                                    case DbfFieldType.Date:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') && compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                        if (query.Contains("OR"))
                        {
                            Console.WriteLine("Here OR");
                            {
                                if (query[5].Contains(">") || query[7].Contains(">"))
                                {
                                    List<string> fieldAndValueWhere = new List<string>(query[5].Split('>'));
                                    List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('>'));
                                    Dbf dbf = new Dbf();
                                    dbf.Read(query[1] + ".dbf");
                                    bool fieldExistsWhere = false;
                                    DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere[0] == field.Name)
                                        {
                                            fieldExistsWhere = true;
                                            fieldTypeWhere = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }
                                    bool fieldExistsWhere2 = false;
                                    DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere2[0] == field.Name)
                                        {
                                            fieldExistsWhere2 = true;
                                            fieldTypeWhere2 = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere2)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }

                                    List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                        return;
                                    }



                                    switch (fieldType) //находка по полю и установка значений
                                    {
                                        case DbfFieldType.Character:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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
                                        case DbfFieldType.Date:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                    }
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

                                        case DbfFieldType.Logical:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                    }
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
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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

                                        case DbfFieldType.Numeric:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                    }
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
                                if (query[5].Contains(">") || query[7].Contains("<"))
                                {
                                    List<string> fieldAndValueWhere = new List<string>(query[5].Split('>'));
                                    List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('<'));
                                    Dbf dbf = new Dbf();
                                    dbf.Read(query[1] + ".dbf");
                                    bool fieldExistsWhere = false;
                                    DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere[0] == field.Name)
                                        {
                                            fieldExistsWhere = true;
                                            fieldTypeWhere = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }
                                    bool fieldExistsWhere2 = false;
                                    DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere2[0] == field.Name)
                                        {
                                            fieldExistsWhere2 = true;
                                            fieldTypeWhere2 = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere2)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }

                                    List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                        return;
                                    }



                                    switch (fieldType) //находка по полю и установка значений
                                    {
                                        case DbfFieldType.Character:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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
                                        case DbfFieldType.Date:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                    }
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

                                        case DbfFieldType.Logical:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                    }
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
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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

                                        case DbfFieldType.Numeric:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                    }
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
                                if (query[5].Contains(">") || query[7].Contains("="))
                                {
                                    List<string> fieldAndValueWhere = new List<string>(query[5].Split('>'));
                                    List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('='));
                                    Dbf dbf = new Dbf();
                                    dbf.Read(query[1] + ".dbf");
                                    bool fieldExistsWhere = false;
                                    DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere[0] == field.Name)
                                        {
                                            fieldExistsWhere = true;
                                            fieldTypeWhere = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }
                                    bool fieldExistsWhere2 = false;
                                    DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere2[0] == field.Name)
                                        {
                                            fieldExistsWhere2 = true;
                                            fieldTypeWhere2 = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere2)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }

                                    List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                        return;
                                    }



                                    switch (fieldType) //находка по полю и установка значений
                                    {
                                        case DbfFieldType.Character:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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
                                        case DbfFieldType.Date:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                    }
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

                                        case DbfFieldType.Logical:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                    }
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
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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

                                        case DbfFieldType.Numeric:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                    }
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
                                if (query[5].Contains("<") || query[7].Contains(">"))
                                {
                                    List<string> fieldAndValueWhere = new List<string>(query[5].Split('<'));
                                    List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('>'));
                                    Dbf dbf = new Dbf();
                                    dbf.Read(query[1] + ".dbf");
                                    bool fieldExistsWhere = false;
                                    DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere[0] == field.Name)
                                        {
                                            fieldExistsWhere = true;
                                            fieldTypeWhere = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }
                                    bool fieldExistsWhere2 = false;
                                    DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere2[0] == field.Name)
                                        {
                                            fieldExistsWhere2 = true;
                                            fieldTypeWhere2 = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere2)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }

                                    List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                        return;
                                    }



                                    switch (fieldType) //находка по полю и установка значений
                                    {
                                        case DbfFieldType.Character:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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
                                        case DbfFieldType.Date:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                    }
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

                                        case DbfFieldType.Logical:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                    }
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
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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

                                        case DbfFieldType.Numeric:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                    }
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
                                if (query[5].Contains("<") || query[7].Contains("<"))
                                {
                                    List<string> fieldAndValueWhere = new List<string>(query[5].Split('<'));
                                    List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('<'));
                                    Dbf dbf = new Dbf();
                                    dbf.Read(query[1] + ".dbf");
                                    bool fieldExistsWhere = false;
                                    DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere[0] == field.Name)
                                        {
                                            fieldExistsWhere = true;
                                            fieldTypeWhere = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }
                                    bool fieldExistsWhere2 = false;
                                    DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere2[0] == field.Name)
                                        {
                                            fieldExistsWhere2 = true;
                                            fieldTypeWhere2 = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere2)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }

                                    List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                        return;
                                    }



                                    switch (fieldType) //находка по полю и установка значений
                                    {
                                        case DbfFieldType.Character:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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
                                        case DbfFieldType.Date:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                    }
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

                                        case DbfFieldType.Logical:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                    }
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
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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

                                        case DbfFieldType.Numeric:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                    }
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
                                if (query[5].Contains("<") || query[7].Contains("="))
                                {
                                    List<string> fieldAndValueWhere = new List<string>(query[5].Split('<'));
                                    List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('='));
                                    Dbf dbf = new Dbf();
                                    dbf.Read(query[1] + ".dbf");
                                    bool fieldExistsWhere = false;
                                    DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere[0] == field.Name)
                                        {
                                            fieldExistsWhere = true;
                                            fieldTypeWhere = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }
                                    bool fieldExistsWhere2 = false;
                                    DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere2[0] == field.Name)
                                        {
                                            fieldExistsWhere2 = true;
                                            fieldTypeWhere2 = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere2)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }

                                    List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                        return;
                                    }



                                    switch (fieldType) //находка по полю и установка значений
                                    {
                                        case DbfFieldType.Character:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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
                                        case DbfFieldType.Date:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                    }
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

                                        case DbfFieldType.Logical:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                    }
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
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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

                                        case DbfFieldType.Numeric:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                    }
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
                                if (query[5].Contains("=") || query[7].Contains(">"))
                                {
                                    List<string> fieldAndValueWhere = new List<string>(query[5].Split('='));
                                    List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('>'));
                                    Dbf dbf = new Dbf();
                                    dbf.Read(query[1] + ".dbf");
                                    bool fieldExistsWhere = false;
                                    DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere[0] == field.Name)
                                        {
                                            fieldExistsWhere = true;
                                            fieldTypeWhere = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }
                                    bool fieldExistsWhere2 = false;
                                    DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere2[0] == field.Name)
                                        {
                                            fieldExistsWhere2 = true;
                                            fieldTypeWhere2 = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere2)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }

                                    List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                        return;
                                    }



                                    switch (fieldType) //находка по полю и установка значений
                                    {
                                        case DbfFieldType.Character:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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
                                        case DbfFieldType.Date:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                    }
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

                                        case DbfFieldType.Logical:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                    }
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
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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

                                        case DbfFieldType.Numeric:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '>'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                    }
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
                                if (query[5].Contains("=") || query[7].Contains("<"))
                                {
                                    List<string> fieldAndValueWhere = new List<string>(query[5].Split('='));
                                    List<string> fieldAndValueWhere2 = new List<string>(query[7].Split('<'));
                                    Dbf dbf = new Dbf();
                                    dbf.Read(query[1] + ".dbf");
                                    bool fieldExistsWhere = false;
                                    DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere[0] == field.Name)
                                        {
                                            fieldExistsWhere = true;
                                            fieldTypeWhere = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }
                                    bool fieldExistsWhere2 = false;
                                    DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere2[0] == field.Name)
                                        {
                                            fieldExistsWhere2 = true;
                                            fieldTypeWhere2 = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere2)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }

                                    List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                        return;
                                    }



                                    switch (fieldType) //находка по полю и установка значений
                                    {
                                        case DbfFieldType.Character:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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
                                        case DbfFieldType.Date:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                    }
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

                                        case DbfFieldType.Logical:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                    }
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
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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

                                        case DbfFieldType.Numeric:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '<'))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                    }
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
                                if (query[5].Contains("=") || query[7].Contains("="))
                                {
                                    List<string> fieldAndValueWhere = new List<string>(query[5].Split('='));
                                    List<string> fieldAndValueWhere2 = new List<string>(query[5].Split('='));
                                    Dbf dbf = new Dbf();
                                    dbf.Read(query[1] + ".dbf");
                                    bool fieldExistsWhere = false;
                                    DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere[0] == field.Name)
                                        {
                                            fieldExistsWhere = true;
                                            fieldTypeWhere = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere)
                                    {
                                        Console.WriteLine("SQL>No such field");
                                        return;
                                    }
                                    bool fieldExistsWhere2 = false;
                                    DbfFieldType fieldTypeWhere2 = DbfFieldType.Character;
                                    foreach (DbfField field in dbf.Fields)
                                    {
                                        if (fieldAndValueWhere2[0] == field.Name)
                                        {
                                            fieldExistsWhere2 = true;
                                            fieldTypeWhere2 = field.Type;
                                        }
                                    }
                                    if (!fieldExistsWhere2)
                                    {
                                        Console.WriteLine("SQL> No such field");
                                        return;
                                    }

                                    List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                        return;
                                    }



                                    switch (fieldType) //находка по полю и установка значений
                                    {
                                        case DbfFieldType.Character:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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
                                        case DbfFieldType.Date:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                    }
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

                                        case DbfFieldType.Logical:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                    }
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
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                    }
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

                                        case DbfFieldType.Numeric:
                                            foreach (DbfRecord record in dbf.Records)
                                            {
                                                try
                                                {
                                                    if (compare(fieldAndValueWhere, record, fieldTypeWhere, '=') || compare(fieldAndValueWhere2, record, fieldTypeWhere2, '='))
                                                    {
                                                        record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                    }
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
                        if(query.Count == 6)
                        {
                            if (query[5].Contains(">"))
                            {
                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('>'));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }
                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:


                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Date:

                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '>'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            if (query[5].Contains("<"))
                            {

                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('<'));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }
                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:


                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Date:

                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '<'))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            if (query[5].Contains("="))
                            {

                                List<string> fieldAndValueWhere = new List<string>(query[5].Split('='));
                                Dbf dbf = new Dbf();
                                dbf.Read(query[1] + ".dbf");
                                bool fieldExistsWhere = false;
                                DbfFieldType fieldTypeWhere = DbfFieldType.Character;
                                foreach (DbfField field in dbf.Fields)
                                {
                                    if (fieldAndValueWhere[0] == field.Name)
                                    {
                                        fieldExistsWhere = true;
                                        fieldTypeWhere = field.Type;
                                    }
                                }
                                if (!fieldExistsWhere)
                                {
                                    Console.WriteLine("SQL> No such field");
                                    return;
                                }
                                List<string> fieldAndValue = new List<string>(query[3].Split('='));
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
                                    return;
                                }
                                switch (fieldType) //находка по полю и установка значений
                                {
                                    case DbfFieldType.Character:


                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Date:

                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = DateTime.Parse(fieldAndValue[1]);
                                                }
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

                                    case DbfFieldType.Logical:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = bool.Parse(fieldAndValue[1]);
                                                }
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
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = fieldAndValue[1];
                                                }
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

                                    case DbfFieldType.Numeric:
                                        foreach (DbfRecord record in dbf.Records)
                                        {
                                            try
                                            {
                                                if (compare(fieldAndValueWhere, record, fieldTypeWhere, '='))
                                                {
                                                    record.Data[record.nameToNum[fieldAndValue[0]]] = double.Parse(fieldAndValue[1]);
                                                }
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
                            return;
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

        public bool compare(List<string> fieldAndValue, DbfRecord record, DbfFieldType dbfFieldType, char znak)
        {
            switch (znak)
            {
                case '>':
                    switch (dbfFieldType)
                    {
                        case DbfFieldType.Character:
                            throw new Exception("Cant compare > to Character");
                        case DbfFieldType.Memo:
                            throw new Exception("Cant compare > to Memo");
                        case DbfFieldType.Logical:
                            throw new Exception("Cant compare > to Logical");
                        case DbfFieldType.Date:
                                if (DateTime.Parse(record.Data[record.nameToNum[fieldAndValue[0]]].ToString()) > DateTime.Parse(fieldAndValue[1])) return true;
                                else return false;  
                        case DbfFieldType.Numeric:
                            if (double.Parse(record.Data[record.nameToNum[fieldAndValue[0]]].ToString()) > double.Parse(fieldAndValue[1])) return true;
                            else return false;
                    }
                    break;
                case '<':
                    switch (dbfFieldType)
                    {
                        case DbfFieldType.Character:
                            throw new Exception("Cant compare > to Character");
                        case DbfFieldType.Memo:
                            throw new Exception("Cant compare > to Memo");
                        case DbfFieldType.Logical:
                            throw new Exception("Cant compare > to Logical");
                        case DbfFieldType.Date:
                            if (DateTime.Parse(record.Data[record.nameToNum[fieldAndValue[0]]].ToString()) < DateTime.Parse(fieldAndValue[1])) return true;
                            else return false;
                        case DbfFieldType.Numeric:
                            if (double.Parse(record.Data[record.nameToNum[fieldAndValue[0]]].ToString()) < double.Parse(fieldAndValue[1])) return true;
                            else return false;
                    }
                    break;
                case '=':
                    switch (dbfFieldType)
                    {
                        case DbfFieldType.Character:
                            if (record.Data[record.nameToNum[fieldAndValue[0]]].ToString() == fieldAndValue[1]) return true;
                            else return false;
                        case DbfFieldType.Memo:
                            if (record.Data[record.nameToNum[fieldAndValue[0]]].ToString() == fieldAndValue[1]) return true;
                            else return false;
                        case DbfFieldType.Logical:
                            if (bool.Parse(record.Data[record.nameToNum[fieldAndValue[0]]].ToString()) == bool.Parse(fieldAndValue[1])) return true;
                            else return false;
                        case DbfFieldType.Date:
                            if (DateTime.Parse(record.Data[record.nameToNum[fieldAndValue[0]]].ToString()) == DateTime.Parse(fieldAndValue[1])) return true;
                            else return false;
                        case DbfFieldType.Numeric:
                            if (double.Parse(record.Data[record.nameToNum[fieldAndValue[0]]].ToString()) == double.Parse(fieldAndValue[1])) return true;
                            else return false;
                    }break;

            }
            return false;
        }
    }
}
