using System;

namespace NaturalSort
{
    class Program
    {
        static string mainFile = "./tape3.bin";
        static void Main(string[] args)
        {
            int bufferSize = 1000000;
            int recordInFile = 10000000;
            TapeBuilder.MakeRandom(mainFile, recordInFile);
            NaturalSort naturalSort = new NaturalSort(mainFile, bufferSize);
            naturalSort.SortTwoPlusOne();
        }
    }
}
