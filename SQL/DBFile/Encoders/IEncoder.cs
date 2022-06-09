using System.Text;

namespace SQL.DBFile.Encoders
{
    public interface IEncoder
    {
        byte[] Encode(DbfField field, object data, Encoding encoding);

        object Decode(byte[] buffer, byte[] memoData, Encoding encoding);
    }
}