using System;
using System.Text;

namespace SQL.DBFile.Encoders
{
    internal class MemoEncoder : IEncoder
    {
        private static MemoEncoder instance;

        private MemoEncoder()
        {
        }

        public static MemoEncoder Instance => instance ?? (instance = new MemoEncoder());

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            return null;
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            var index = 0;

            if (buffer.Length > 4)
            {
                var text = encoding.GetString(buffer).Trim();
                if (text.Length == 0) return null;
                index = Convert.ToInt32(text);
            }
            else
            {
                index = BitConverter.ToInt32(buffer, 0);
                if (index == 0) return null;
            }

            return findMemo(index, memoData, encoding);
        }

        private static string findMemo(int index, byte[] memoData, Encoding encoding)
        {
            var blockSize = BitConverter.ToUInt16(new[] {memoData[7], memoData[6]}, 0);
            var length = (int) BitConverter.ToUInt32(
                new[]
                {
                    memoData[index * blockSize + 4 + 3],
                    memoData[index * blockSize + 4 + 2],
                    memoData[index * blockSize + 4 + 1],
                    memoData[index * blockSize + 4 + 0]
                },
                0);

            var memoBytes = new byte[length];
            var lengthToSkip = index * blockSize + 8;

            for (var i = lengthToSkip; i < lengthToSkip + length; ++i) memoBytes[i - lengthToSkip] = memoData[i];

            return encoding.GetString(memoBytes).Trim();
        }
    }
}