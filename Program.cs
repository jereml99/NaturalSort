using System;

namespace NaturalSort
{
    class Program
    {
        static string mainFile = "./tape3.bin";
        static void Main(string[] args)
        {


            TapeBuilder.MakeFromEntry(mainFile);

            Tape tape3 = new Tape(mainFile);
            Record[] buffer3 = new Record[8];
            while (tape3.Read(ref buffer3) > 0)
            {
                foreach (Record record in buffer3)
                {
                    Console.WriteLine(record);
                }
            }

        }
    }
}
