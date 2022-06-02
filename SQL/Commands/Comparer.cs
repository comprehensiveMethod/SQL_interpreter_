using System;
using System.Collections.Generic;
using System.Linq;
using SQL.DBFile;

namespace SQL.Commands
{
    static public class Comparer
    {
        public static Dictionary<string, Func<Dbf,DbfRecord,object, object, bool>> cond =
            new Dictionary<string, Func<Dbf,DbfRecord,object, object, bool>>();

        static  Comparer()
        {
            cond = new Dictionary<string, Func<Dbf, DbfRecord, object, object, bool>>();
            SetCond();
        }
        static void SetCond()
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
        static public bool WhereCondition(Dbf file,DbfRecord record, string condition)
        {
            if (condition == "")
                return true;
          
     
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
    }
}