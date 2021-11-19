using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NaturalSort
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
            BinaryWriter SaveFileStream = new BinaryWriter(File.Create("./tape3.bin"));
            for (int i = 0; i < 10; i++)
            {
                SaveFileStream.Write(1.2);
            }
            SaveFileStream.Close();
            BinaryReader FileStream = new BinaryReader(File.OpenRead("./tape3.bin"));

            byte[] buffor = new byte[16];
            double[] values = new double[buffor.Length / 8];

            for (int i = 0; i < 5; i++)
            {
                FileStream.Read(buffor);
                Buffer.BlockCopy(buffor, 0, values, 0, buffor.Length);
                Console.WriteLine(new Record(values[0], values[1]));
            }


        }
    }
}
