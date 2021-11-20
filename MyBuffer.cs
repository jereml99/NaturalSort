using System;

namespace NaturalSort
{
    internal class MyBuffer
    {
        public int Length { get => buffer.Length; }

        public Tape tape;
        private Record[] buffer;
        private int index = 0;

        public MyBuffer(string path, int bufferSize)
        {
            tape = new Tape(path);
            buffer = new Record[bufferSize];
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
            tape.Write(buffer);
            Array.Clear(buffer, 0, buffer.Length);
            index = 0;
        }
    }
}
