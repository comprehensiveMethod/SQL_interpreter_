using System.Collections.Generic;
using System.IO;

namespace SQL
{
    public class Table
    {
        private string filename;
        private Header header;
        private List<Descriptor> descriptors;
        private Body body;
        
        public Table(string filename)
        {
            //сделать провекри на имя файла
            this.filename = filename;
            File.Create(filename + ".dbf");
        }
    }
}