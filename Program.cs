using System;

namespace NaturalSort
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            Tape tape3 = new Tape("./tape3.bin");
            Record[] buffer3 = new Record[4] { new Record(1, 1), new Record(2, 2), null, null };
            tape3.Write(buffer3);
            for (int i = 0; i < 1; i++)
            {
                tape3.Read(buffer3);
                foreach (Record record in buffer3)
                {
                    Console.WriteLine(record);
                }
            }

        }
    }
}
