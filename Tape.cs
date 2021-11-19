using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalSort
{
    class Tape
    {
        private BinaryReader tapeReader;
        private BinaryWriter tapeWriter;
        private string path;
        private int writeAcces;
        private int readAcces;

        public Tape(string path)
        {
            this.path = path;
          
        }

        List<Record> read()
        {
            using (tapeReader = new BinaryReader(File.Open(path, FileMode.OpenOrCreate)))
            {
                Console.WriteLine(String.Format("Created binary writer: {0}", path));
            }
            return new List<Record>();
        }
    }
}
