using System;
using System.IO;

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

            tapeWriter = new BinaryWriter(File.OpenWrite(path));
        }

        public void Read(Record[] buffer)
        {
            if (tapeWriter != null)
            {
                tapeWriter.Close();
                tapeWriter = null;
                Console.WriteLine("Closed BinaryWriter {0}", path);
            }
            if (tapeReader == null)
            {
                tapeReader = new BinaryReader(File.Open(path, FileMode.OpenOrCreate));
                Console.WriteLine("Opened BinaryReader {0}", path);
            }
            Console.WriteLine("read from file {0}", path);
            int numberOfByte = buffer.Length * sizeof(double) * 2;
            byte[] byteBuffer = new byte[numberOfByte];
            tapeReader.Read(byteBuffer);
            ConvertToRecord(byteBuffer).CopyTo(buffer, 0);
        }

        public void Write(Record[] buffer)
        {
            if (tapeReader != null)
            {
                tapeReader.Close();
                tapeReader = null;
                Console.WriteLine("Closed BinaryReader {0}", path);
            }
            if (tapeWriter == null)
            {
                tapeWriter = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate));
                Console.WriteLine("Opened BinaryWriter {0}", path);
            }

            Console.WriteLine("write to file {0}", path);
            tapeWriter.Write(ConvertToBytes(buffer));
        }

        private Record[] ConvertToRecord(byte[] byteBuffer)
        {
            double[] doubleBuffer = new double[byteBuffer.Length / 8];
            Buffer.BlockCopy(byteBuffer, 0, doubleBuffer, 0, byteBuffer.Length);
            Record[] recordBuffer = new Record[doubleBuffer.Length / 2];
            for (int i = 0; i < recordBuffer.Length; i++)
            {
                recordBuffer[i] = new Record(doubleBuffer[2 * i], doubleBuffer[2 * i + 1]);
            }
            return recordBuffer;
        }

        private byte[] ConvertToBytes(Record[] recordBuffer)
        {
            int numberOfByte = recordBuffer.Length * sizeof(double) * 2;
            byte[] byteBuffer = new byte[numberOfByte];
            double[] doubleBuffer = new double[numberOfByte / sizeof(double)];
            int i = 0;
            foreach (Record record in recordBuffer)
            {
                doubleBuffer[2 * i] = record.realPart;
                doubleBuffer[2 * i + 1] = record.imaginaryPart;
            }

            Buffer.BlockCopy(doubleBuffer, 0, byteBuffer, 0, byteBuffer.Length);
            return byteBuffer;
        }

    }
}
