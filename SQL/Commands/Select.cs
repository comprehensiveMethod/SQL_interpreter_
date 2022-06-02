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
                if (ForWhere(dbf,record,condition))
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

        public Dictionary<string, Func<Dbf,DbfRecord,object, object, bool>> cond =
            new Dictionary<string, Func<Dbf,DbfRecord,object, object, bool>>();

        void SetCond()
        {
            cond["<"] = (file, record, o1, o2) =>
            {
                var type = file.Fields.Where(x => x.Name == o1.ToString()).First().Type;
                    var val = record[o1.ToString()].ToString();

                    if (type == DbfFieldType.Character)
                        return string.Compare(val, o2.ToString()) == -1;

                    if (type == DbfFieldType.Numeric)
                        return double.Parse(val) < double.Parse(o2.ToString());

                    if (type == DbfFieldType.Date)
                        return DateTime.Parse(val) < DateTime.Parse(o1.ToString());

                    if (type == DbfFieldType.Memo)
                        return string.Compare(val, o2.ToString()) == -1;
                
                   // Logical = 'L',
                  
  
                    throw new Exception("wrong type");
                };
            cond["<="] = (file, record, o1, o2) =>
            {
                var type = file.Fields.Where(x => x.Name == o1.ToString()).First().Type;
                var val = record[o1.ToString()].ToString();

                if (type == DbfFieldType.Character)
                    return string.Compare(val, o2.ToString()) <= 0;

                if (type == DbfFieldType.Numeric)
                    return double.Parse(val) <= double.Parse(o2.ToString());

                if (type == DbfFieldType.Date)
                    return DateTime.Parse(val) <= DateTime.Parse(o1.ToString());

                if (type == DbfFieldType.Memo)
                    return string.Compare(val, o2.ToString()) <= 0;
                
                // Logical = 'L',
                
                throw new Exception("wrong type");
            };
            cond[">"] = (file, record, o1, o2) =>
            {
                var type = file.Fields.Where(x => x.Name == o1.ToString()).First().Type;
                var val = record[o1.ToString()].ToString();

                if (type == DbfFieldType.Character)
                    return string.Compare(val, o2.ToString()) == 1;

                if (type == DbfFieldType.Numeric)
                    return double.Parse(val) > double.Parse(o2.ToString());

                if (type == DbfFieldType.Date)
                    return DateTime.Parse(val) > DateTime.Parse(o1.ToString());

                if (type == DbfFieldType.Memo)
                    return string.Compare(val, o2.ToString()) == 1;
                
                // Logical = 'L',
                  
  
                throw new Exception("wrong type");
            };
            cond[">="] = (file, record, o1, o2) =>
            {
                var type = file.Fields.Where(x => x.Name == o1.ToString()).First().Type;
                var val = record[o1.ToString()].ToString();

                if (type == DbfFieldType.Character)
                    return string.Compare(val, o2.ToString()) >= 0;

                if (type == DbfFieldType.Numeric)
                    return double.Parse(val) >= double.Parse(o2.ToString());

                if (type == DbfFieldType.Date)
                    return DateTime.Parse(val) >= DateTime.Parse(o1.ToString());

                if (type == DbfFieldType.Memo)
                    return string.Compare(val, o2.ToString()) >= 0;
                
                // Logical = 'L',
                  
  
                throw new Exception("wrong type");
            };
            cond["=="] = (file,record, o1, o2) =>
            {
                var type = file.Fields.Where(x => x.Name == o1.ToString()).First().Type;
                var val = record[o1.ToString()].ToString();

                if (type == DbfFieldType.Character)
                    return string.Compare(val, o2.ToString()) == 0;

                if (type == DbfFieldType.Numeric)
                    return double.Parse(val) == double.Parse(o2.ToString());

                if (type == DbfFieldType.Date)
                    return DateTime.Parse(val) == DateTime.Parse(o1.ToString());

                if (type == DbfFieldType.Memo)
                    return string.Compare(val, o2.ToString()) == 0;
                
                if (type == DbfFieldType.Logical)
                    return bool.Parse(o1.ToString()) == bool.Parse(o2.ToString());
   
  
                throw new Exception("wrong type");
            };
            cond["!="] = (file,record, o1, o2) =>
            {
                var type = file.Fields.Where(x => x.Name == o1.ToString()).First().Type;
                var val = record[o1.ToString()].ToString();

                if (type == DbfFieldType.Character)
                    return string.Compare(val, o2.ToString()) != 0;

                if (type == DbfFieldType.Numeric)
                    return double.Parse(val) != double.Parse(o2.ToString());

                if (type == DbfFieldType.Date)
                    return DateTime.Parse(val) != DateTime.Parse(o1.ToString());

                if (type == DbfFieldType.Memo)
                    return string.Compare(val, o2.ToString()) != 0;
                
                if (type == DbfFieldType.Logical)
                    return bool.Parse(o1.ToString()) != bool.Parse(o2.ToString());
   
  
                throw new Exception("wrong type");
            };
            
            cond["OR"] = (file,record, o1, o2) =>
            {
                return bool.Parse(o1.ToString()) || bool.Parse(o2.ToString());
            };
            cond["AND"] = (file,record, o1, o2) =>
            {
                return bool.Parse(o1.ToString()) && bool.Parse(o2.ToString());
            };
        }

        public bool ForWhere(Dbf file,DbfRecord record, string condition)
        {
            if (condition == "")
                return true;
          
            SetCond();
            var buff=
                System.Text.RegularExpressions.Regex
                    .Replace(condition, @"\s+", " ")
                    .Split(" ").ToList();

         
        if(buff[0]=="")
           buff.RemoveAt(0);
        if(buff.Last()=="")
            buff.RemoveAt(buff.Count-1);

        
            if (buff.Count == 7)
                return cond[buff[3]](file,record,
                    cond[buff[1]](file,record,buff[0], buff[2]),
                    cond[buff[5]](file,record,buff[4], buff[6]));
            
            if (buff.Count == 3)
                return cond[buff[1]](file,record,
                    buff[0], buff[2]);

            throw new Exception("wrong WHERE");
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