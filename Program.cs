using System;

namespace NaturalSort
{
    class Program
    {
        static string mainFile = "./tape3.bin";
        static void Main(string[] args)
        {
            int recordInFile = 0;
            string input;
            Console.Write("Chose option\n" +
                "1 Random records\n" +
                "2 Worst case records\n" +
                "3 records from input\n" +
                "4 from file\n");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("How many records?: ");
                    input = Console.ReadLine();
                    recordInFile = int.Parse(input);
                    TapeBuilder.MakeRandom(mainFile, recordInFile);
                    break;
                case "2":
                    Console.Write("How many records?: ");
                    input = Console.ReadLine();
                    recordInFile = int.Parse(input);
                    TapeBuilder.MakeWorstCase(mainFile, recordInFile);
                    break;
                case "3":
                    recordInFile = TapeBuilder.MakeFromEntry(mainFile);
                    break;
                case "4":
                    Console.Write("Path: ");
                    string path = Console.ReadLine();
                    recordInFile = TapeBuilder.MakeFromFile(path,mainFile);
                    break;
                case "5":
                    new Tape(mainFile).printTape();
                    break;
                default:
                    throw new Exception("Bed command");
                    break;
            }


            Console.Write("Buffer size: ");
            int bufferSize = int.Parse(Console.ReadLine());


            NaturalSort naturalSort = new NaturalSort(mainFile, bufferSize);

            Console.WriteLine("N = {0} b= {1}",recordInFile,bufferSize);
            Console.WriteLine("Teroetical max reads/writes acces: {0}", Math.Ceiling(4 * recordInFile * Math.Log2(recordInFile) / bufferSize) );
            Console.WriteLine("Teroetical max phase: {0}", Math.Ceiling(Math.Log2(recordInFile)));
            naturalSort.SortTwoPlusOne();
            
        }
    }
}
