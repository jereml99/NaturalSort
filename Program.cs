using System;

namespace NaturalSort
{
    class Program
    {
        static string mainFile = "./tape3.bin";
        static void Main(string[] args)
        {


            //TapeBuilder.MakeRandom(mainFile,10);
            NaturalSort naturalSort = new NaturalSort(mainFile, 3);
            naturalSort.SortTwoPlusOne();
        }
    }
}
