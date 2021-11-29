using System;

namespace NaturalSort
{
    class Record
    {
        public double realPart { get; }
        public double imaginaryPart { get; }
        public double modul;
        public static int ByteForRecord = sizeof(double) * 2;
        public Record(double realPart, double imaginaryPart)
        {
            this.realPart = Math.Round(realPart,2);
            this.imaginaryPart = Math.Round(imaginaryPart, 2);
            this.modul = Math.Round(Math.Sqrt(Math.Pow(realPart, 2) + Math.Pow(imaginaryPart, 2)),2);
        }


        public override string ToString()
        {
            return "{" + String.Format("m:{2} r:{0} i:{1}", realPart, imaginaryPart, modul) + "}";
        }

        public string ToShort()
        {
            return modul.ToString();
        }
    }
}
