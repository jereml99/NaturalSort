using System;

namespace NaturalSort
{
    class Program
    {
        static string mainFile = "./tape3.bin";
        static void Main(string[] args)
        {
            int bufferSize = 4;
            int recordInFile = 8;
            TapeBuilder.MakeRandom(mainFile, recordInFile);
            NaturalSort naturalSort = new NaturalSort(mainFile, bufferSize);
            Console.WriteLine("N = {0} b= {1}",recordInFile,bufferSize);
            Console.WriteLine("Teroetical max reads/writes acces: {0}", (int)(4 * recordInFile * Math.Log2(recordInFile) / bufferSize));
            naturalSort.SortTwoPlusOne();
            
        }
    }
}
