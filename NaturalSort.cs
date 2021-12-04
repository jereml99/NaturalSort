using System;

namespace NaturalSort
{
    class NaturalSort
    {
        private MyBuffer mainBuffer;
        private MyBuffer buffer1;
        private MyBuffer buffer2;
        private bool debug = false;
        public NaturalSort(string path, int bufferSize)
        {
            mainBuffer = new MyBuffer(path, bufferSize);
            buffer1 = new MyBuffer("./tape1.bin", bufferSize);
            buffer2 = new MyBuffer("./tape2.bin", bufferSize);
        }

        public void SortTwoPlusOne()
        {
            buffer1.ClearTape();
            buffer2.ClearTape();

            int phase = 0;
            while(true)
            {
                phase++;
                if (debug) Console.WriteLine("\n\n\t\t START PHASE {0}",phase);

                if (debug) mainBuffer.tape.printTape();
                if (debug) Console.WriteLine();
                Distribution();
                if (debug) buffer1.tape.printTape();
                if (debug) buffer2.tape.printTape();

                if (merge()) break;
                int accessNumber = mainBuffer.DiscAccesNumber() + buffer1.DiscAccesNumber() + buffer2.DiscAccesNumber();
                if (debug) Console.WriteLine("\t AFTER merg, number of acces = {0}",accessNumber);
                if (debug) mainBuffer.tape.printTape();
            }
            int accesCount = mainBuffer.DiscAccesNumber() + buffer1.DiscAccesNumber() + buffer2.DiscAccesNumber();
            Console.WriteLine("#########STATS##########");
            Console.WriteLine("\n\n\t File SORTED AFTER {0} PHASES \n\t acces to file: {1}", phase, accesCount);
            Console.WriteLine("#########STATS##########");
            if (debug) mainBuffer.tape.printTape();
            mainBuffer.Close();
            buffer1.Close();
            buffer2.Close();
        }

       
        private void Distribution()
        {
            Record lastRecord, record;

            lastRecord = new Record(0, 0);
            bool toFirstBuffer = true;
            

            while ((record=mainBuffer.get()) != null)
            {
                if(record.modul < lastRecord.modul)
                {
                    toFirstBuffer = !toFirstBuffer;
                }

                if (toFirstBuffer) buffer1.add(record);
                else buffer2.add(record);

                lastRecord = record;
            }
            buffer1.flush();
            buffer2.flush();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>return true if file is allready sorted </returns>
        private bool merge()
        {
            bool isSorted = true;
            Record fromBuffer1 = buffer1.get(); 
            Record fromBuffer2 = buffer2.get();
            Record nextInLine;

            bool lastWriteFromBuffer1 = true;

            while(fromBuffer1 != null && fromBuffer2 != null)
            {
                if (fromBuffer1.modul < fromBuffer2.modul) // porowanie 2 elemetów z każdej taśmy
                {
                    //TODO łądniej można
                    lastWriteFromBuffer1 = true;
                    mainBuffer.add(fromBuffer1);
                    nextInLine = buffer1.get();
                }
                else
                {
                    lastWriteFromBuffer1 = false;
                    mainBuffer.add(fromBuffer2);
                    nextInLine = buffer2.get();
                }
               
                if (lastWriteFromBuffer1) // jak ostatnio pobrano z pierwszej taśmy
                {
                    HandleRuns(ref fromBuffer1,ref fromBuffer2, ref nextInLine, ref buffer2, ref lastWriteFromBuffer1, ref  isSorted);
                }
                else
                {
                    HandleRuns(ref fromBuffer2,ref fromBuffer1, ref nextInLine, ref buffer1, ref lastWriteFromBuffer1, ref  isSorted);    
                }
            }

            if (lastWriteFromBuffer1)
            {
                mainBuffer.add(fromBuffer2);
                while((nextInLine = buffer2.get()) != null)
                {
                    if(nextInLine.modul < fromBuffer2.modul)
                    {
                        isSorted = false;
                    }
                    mainBuffer.add(nextInLine);
                }
            }
            else
            {
                mainBuffer.add(fromBuffer1);
                while ((nextInLine = buffer1.get()) != null)
                {
                    if (nextInLine.modul < fromBuffer1.modul)
                    {
                        isSorted = false;
                    }
                    mainBuffer.add(nextInLine);
                }
            }
            mainBuffer.flush();

            return isSorted;
        }

        private void HandleRuns(ref Record fromBuffer, ref Record secondFrombuffer, ref Record nextInLine, ref MyBuffer buffer, ref bool lastWriteFromBuffer1, ref bool isSorted)
        {
            if (nextInLine != null && fromBuffer.modul > nextInLine.modul) // Patrzrymy czy nie ma końca serii
            {
                isSorted = false;
                fromBuffer = nextInLine;

                nextInLine = buffer.get(); // lecimy do końca serii w drugiej taśmie
                while (nextInLine != null && nextInLine.modul > secondFrombuffer.modul)
                {
                    mainBuffer.add(secondFrombuffer);
                    secondFrombuffer = nextInLine;
                    nextInLine = buffer.get();
                }
                mainBuffer.add(secondFrombuffer);
                secondFrombuffer = nextInLine;
                lastWriteFromBuffer1 = !lastWriteFromBuffer1;
            }
            else
            {
                fromBuffer = nextInLine;
            }
        }

    }
}
