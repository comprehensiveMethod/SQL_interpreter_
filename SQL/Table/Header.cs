using System;

namespace SQL
{
    public class Header
    {
         private static int headerSize = 32;
         byte[] data = new byte[headerSize];

         //примеры свойств
         public byte zeroHeaderByte
         {
             get
             {
                 return data[0];
             }
             set
             {
                 data[0] = value;
             }
         }
         public DateTime LastUpdate
         {
             get
             {
                 byte[] buff = new byte[4];
                 Array.Copy(data, 1, buff, 0, 4);
                 long longVar = BitConverter.ToInt64(buff, 0);
                 return new DateTime(longVar);
             }
             set
             {
                 Array.Copy(BitConverter.GetBytes(value.ToBinary()), 0, data, 1, 4);
             }
         }

         public int recordCounter { get; set; }
         public int headerLenth { get; set; }
         public int recordLenth { get; set; }
         
    }
}