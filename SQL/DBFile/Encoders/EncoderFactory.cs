using System;

namespace SQL.DBFile.Encoders
{
    internal class EncoderFactory
    {
        public static IEncoder GetEncoder(DbfFieldType type)
        {
            switch (type)
            {
                case DbfFieldType.Character:
                    return CharacterEncoder.Instance;
                case DbfFieldType.Date:
                    return DateEncoder.Instance;
                case DbfFieldType.Logical:
                    return LogicalEncoder.Instance;
                case DbfFieldType.Memo:
                    return MemoEncoder.Instance;
                case DbfFieldType.Numeric:
                    return NumericEncoder.Instance;
                default:
                    throw new ArgumentException("No encoder found for dBASE type " + type);
            }
        }
    }
}