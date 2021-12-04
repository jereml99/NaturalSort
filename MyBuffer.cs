using System;

namespace NaturalSort
{
    internal class MyBuffer
    {
        public int Length { get => buffer.Length; }
        private int writeAcces;
        private int readAcces;
        public Tape tape;
        private Record[] buffer;
        private int index = 0;

        public MyBuffer(string path, int bufferSize)
        {
            tape = new Tape(path);
            buffer = new Record[bufferSize];
            writeAcces = 0;
            readAcces = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>return null when whole tape read</returns>
        public Record get()
        {
            if (index >= buffer.Length || buffer[index] == null)
            {
                if (tape.Read(ref buffer) > 0)
                {
                    readAcces++;
                    index = 0;
                    return buffer[index++];
                }
                else
                {
                    index = 0;
                    tape.Close();
                    return null;
                }
            }
            else
            {
                return buffer[index++];
            }
        }

        public void add(Record record)
        {
            if(index >= buffer.Length)
            {
                flush();
            }
            buffer[index++] = record;
        }

        public void flush()
        {
            if( buffer[0] != null )
            {
                // nie ma sensu pisać jak jest pusty buffor 
                writeAcces++;
                tape.Write(buffer);
            }
            Array.Clear(buffer, 0, buffer.Length);
            index = 0;
        }

        public int DiscAccesNumber() => writeAcces + readAcces;

        public void ClearTape() => tape.Write(new Record[0]);
        public void Close()
        {
            tape.Close();
        }
    }
}
