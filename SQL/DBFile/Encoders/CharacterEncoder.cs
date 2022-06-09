using System;
using System.Collections.Generic;
using System.Text;

namespace SQL.DBFile.Encoders
{
    internal class CharacterEncoder : IEncoder
    {
        private static CharacterEncoder instance;

        // cach different length bytes (for performance)
        private readonly Dictionary<int, byte[]> buffers = new Dictionary<int, byte[]>();

        private CharacterEncoder()
        {
        }

        public static CharacterEncoder Instance => instance ?? (instance = new CharacterEncoder());

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            // Input data maybe various: int, string, whatever.
            var res = data?.ToString();
            if (string.IsNullOrEmpty(res)) res = field.DefaultValue;

            // consider multibyte should truncate or padding after GetBytes (11 bytes)
            var buffer = GetBuffer(field.Length);
            var bytes = encoding.GetBytes(res);
            Array.Copy(bytes, buffer, Math.Min(bytes.Length, field.Length));

            return buffer;
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            var text = encoding.GetString(buffer).Trim();
            if (text.Length == 0) return null;
            return text;
        }

        private byte[] GetBuffer(int length)
        {
            if (!buffers.TryGetValue(length, out var bytes))
            {
                var s = new string(' ', length);
                bytes = Encoding.ASCII.GetBytes(s);
                buffers.Add(length, bytes);
            }

            return (byte[]) bytes.Clone();
        }
    }
}