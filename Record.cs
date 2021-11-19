using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalSort
{
    [Serializable()]
    class Record
    {
        private double realPart, imaginaryPart;
        public double modul; 
        public Record(double realPart, double imaginaryPart)
        {
            this.realPart = realPart;
            this.imaginaryPart = imaginaryPart;
            this.modul = Math.Sqrt(Math.Pow(realPart, 2) + Math.Pow(imaginaryPart, 2));
        }


        public override string ToString()
        {
            return "{"+String.Format("\n r:{0}\n i:{1}\n m:{2}\n", realPart, imaginaryPart,modul)+"}";
        }
    }
}
