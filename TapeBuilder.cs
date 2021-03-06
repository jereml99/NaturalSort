using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalSort
{
    static class TapeBuilder
    {
        static int rangeForRandom = 100;
        static public void MakeRandom(string path, int numberOfRecord)
        {
            Tape tape = new Tape(path);
            Record[] records = new Record[numberOfRecord];
            Random random = new Random(1);

            for (int i = 0; i < numberOfRecord; i++)
            {
                records[i] = new Record(random.NextDouble() * rangeForRandom, random.NextDouble() * rangeForRandom);
            }
            tape.Write(records);
            tape.Close();
        }

        static public int MakeFromFile(string srcPath, string outPath)
        {
            int numberOfRecords = -1;
            using (StreamReader sr = File.OpenText(srcPath))
            {

                numberOfRecords = 0;
                string s;
                Tape tape = new Tape(outPath);
                while ((s = sr.ReadLine()) != null)
                {
                    numberOfRecords++;
                    s = s.Trim();
                    string[]  entry =  s.Split(" ");
                    Record[] records = {
                        new Record(Double.Parse(entry[0]), Double.Parse(entry[1]))
                    };
                    tape.Write(records);
                }
                tape.Close();
            }
            return numberOfRecords;
        }

        static public int MakeFromEntry(string path)
        {
            string input;
            double realPart, imaginaryPart;
            LinkedList<Record> records = new LinkedList<Record>();
            Tape tape = new Tape(path);

            Console.WriteLine("Enter Complex number in format:\n" +
                "realPart imaginaryPart \\newline \n" +
                "use ','\n" +
                "enter q for end");
          
            while (true)
            {
                input = Console.ReadLine();
                if (input == "q") break;
                if (!Double.TryParse(input.Split(' ')[0].Trim(), out realPart)) { Console.WriteLine(" ^ Parse error try again"); continue; }
                if (!Double.TryParse(input.Split(' ')[1].Trim(), out imaginaryPart)) { Console.WriteLine("\t ^ Parse error try again"); continue; }
                records.AddLast(new Record(realPart, imaginaryPart));
            }
            tape.Write(records.ToArray());
            tape.Close();
            return records.Count;
        }

        static public void MakeWorstCase(string path, int numberOfRecord)
        {
            Tape tape = new Tape(path);
            Record[] records = new Record[numberOfRecord];
          
            for (int i = 0; i < numberOfRecord; i++)
            {
                records[i] = new Record(numberOfRecord-i,0);
            }
            tape.Write(records);
            tape.Close();
        }
    }
}
