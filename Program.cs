using System;

namespace NaturalSort
{
    class Program
    {
        static string mainFile = "./tape3.bin";
        static void Main(string[] args)
        {
            bool debug = false;
            if(args.Length > 0)
            {
                if (args[0] == "-d") debug = true;
            }
            int recordInFile = 0;
            string input;
            Console.Write("Chose option\n" +
                "1 Random records\n" +
                "2 Worst case records\n" +
                "3 records from input\n" +
                "4 from file\n" +
                "5 print main tape\n");
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
                    return;
                default:
                    throw new Exception("Bad command");
                    break;
            }


            Console.Write("Buffer size: ");
            int bufferSize = int.Parse(Console.ReadLine());


            NaturalSort naturalSort = new NaturalSort(mainFile, bufferSize, debug);

            Console.WriteLine("\n\nN = {0} b= {1}",recordInFile,bufferSize);
        /*    Console.WriteLine("Teroetical max reads/writes acces: {0}", Math.Ceiling(4 * recordInFile * Math.Log2(recordInFile) / bufferSize) );
            Console.WriteLine("Teroetical max phase: {0}", Math.Ceiling(Math.Log2(recordInFile)));
*/
            int runsNumber = countRuns(mainFile);
            Console.WriteLine("Number of runs in file: {0}", runsNumber);
            Console.WriteLine("Teroetical phase: {0}", Math.Round(Math.Log2(runsNumber),2));
            Console.WriteLine("Teroetical reads/writes access: {0}", Math.Round(4 * recordInFile * Math.Log2(runsNumber) / bufferSize,2));
            naturalSort.SortTwoPlusOne();
            
        }

        static public int countRuns(string path)
        {
            MyBuffer buffer = new MyBuffer(path, 100);
            Record actual;
            Record last = new Record(float.MinValue, float.MinValue);
            int result = 0;

            while((actual = buffer.get()) != null)
            {
                if(actual.modul < last.modul)
                {
                    result++;
                }
                last = actual;
            }
            buffer.Close();
            return result;
        }
    }
}
