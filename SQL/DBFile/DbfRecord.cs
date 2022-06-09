using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQL.DBFile.Encoders;

namespace SQL.DBFile
{
    /// <summary>
    ///     DbfRecord encapsulates a record in a .dbf file. It contains an array with
    ///     data (as an Object) for each field.
    /// </summary>
    public class DbfRecord
    {
        private const string defaultSeparator = "|";
        public const string defaultMask = "{value}";

        private readonly List<DbfField> fields;
        public Dictionary<string, int> nameToNum;

        internal DbfRecord(BinaryReader reader, DbfHeader header, List<DbfField> fields, byte[] memoData,
            Encoding encoding)
        {
            this.fields = fields;
            Data = new List<object>();
            nameToNum = new Dictionary<string, int>(); //добавить инициализаци

            // Read record marker.
            var marker = reader.ReadByte();

            // Read entire record as sequence of bytes.
            // Note that record length includes marker.
            var row = reader.ReadBytes(header.RecordLength - 1);
            if (row.Length == 0)
                throw new EndOfStreamException();

            // Read data for each field.
            var offset = 0;
            var i = 0;
            foreach (var field in fields)
            {
                nameToNum[field.Name] = i++;
                // Copy bytes from record buffer into field buffer.
                var buffer = new byte[field.Length];
                Array.Copy(row, offset, buffer, 0, field.Length);
                offset += field.Length;

                var encoder = EncoderFactory.GetEncoder(field.Type);
                Data.Add(encoder.Decode(buffer, memoData, encoding));
            }
        }

        /// <summary>
        ///     Create an empty record.
        /// </summary>
        internal DbfRecord(List<DbfField> fields)
        {
            this.fields = fields;
            Data = new List<object>();
            foreach (var field in fields) Data.Add(null);
        }

        public List<object> Data { get; }

        public object this[int index] => Data[index];


        public object this[string name]
        {
            get
            {
                var index = fields.FindIndex(x => x.Name.Equals(name));
                if (index == -1) return null;
                return Data[index];
            }
            set
            {
                for (var i = 0; i < fields.Count; i++)
                    if (fields[i].Name == name)
                    {
                        Data[i] = value;
                        break;
                    }
            }
        }

        public object this[DbfField field]
        {
            get
            {
                var index = fields.IndexOf(field);
                if (index == -1) return null;
                return Data[index];
            }
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToString(defaultSeparator, defaultMask);
        }

        /// <summary>
        ///     Returns a string that represents the current object with custom separator.
        /// </summary>
        /// <param name="separator">Custom separator.</param>
        /// <returns>A string that represents the current object with custom separator.</returns>
        public string ToString(string separator)
        {
            return ToString(separator, defaultMask);
        }

        /// <summary>
        ///     Returns a string that represents the current object with custom separator and mask.
        /// </summary>
        /// <param name="separator">Custom separator.</param>
        /// <param name="mask">
        ///     Custom mask.
        ///     <para>e.g., "{name}={value}", where {name} is the mask for the field name, and {value} is the mask for the value.</para>
        /// </param>
        /// <returns>A string that represents the current object with custom separator and mask.</returns>
        public string ToString(string separator, string mask)
        {
            separator = separator ?? defaultSeparator;
            mask = (mask ?? defaultMask).Replace("{name}", "{0}").Replace("{value}", "{1}");

            return string.Join(separator, fields.Select(z => string.Format(mask, z.Name, this[z])));
        }

        internal void Write(BinaryWriter writer, Encoding encoding)
        {
            // Write marker (always "not deleted")
            writer.Write((byte) 0x20);

            var index = 0;
            foreach (var field in fields)
            {
                var encoder = EncoderFactory.GetEncoder(field.Type);
                var buffer = encoder.Encode(field, Data[index], encoding);
                if (buffer.Length > field.Length)
                    throw new ArgumentOutOfRangeException(nameof(buffer.Length), buffer.Length,
                        "Buffer length has exceeded length of the field.");

                writer.Write(buffer);
                index++;
            }
        }
    }
}