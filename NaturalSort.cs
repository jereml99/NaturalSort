namespace NaturalSort
{
    class NaturalSort
    {
        private MyBuffer mainBuffer;
        private MyBuffer buffer1;
        private MyBuffer buffer2;

        public NaturalSort(string path, int bufferSize)
        {
            mainBuffer = new MyBuffer(path, bufferSize);
            buffer1 = new MyBuffer("./tape1.bin", bufferSize);
            buffer2 = new MyBuffer("./tape2.bin", bufferSize);
        }

        public void SortTwoPlusOne()
        {
            mainBuffer.tape.printTape();
            Distribution();
            buffer1.tape.printTape();
            buffer2.tape.printTape();
            merge();
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

        private void merge()
        {
            Record fromBuffer1 = buffer1.get(); 
            Record fromBuffer2 = buffer2.get();
            Record nextInLine;

            bool lastWriteFromBuffer1 = true;

            if(fromBuffer1.modul < fromBuffer2.modul) // porowanie 2 elemetów z każdej taśmy
            {
                //TODO łądniej można
                lastWriteFromBuffer1 = true ;
                mainBuffer.add(fromBuffer1);
                nextInLine = buffer1.get();
            }
            else
            {
                lastWriteFromBuffer1 = false;
                mainBuffer.add(fromBuffer2);
                nextInLine = buffer2.get();
            }

            if(lastWriteFromBuffer1) // jak ostatnio pobrano z pierwszej taśmy, może inna nazwa?
            {
                if(fromBuffer1.modul > nextInLine.modul) // Patrzrymy czy nie ma końca serii
                {
                    fromBuffer1 = nextInLine;

                    nextInLine = buffer2.get(); // lecimy do końca serii w drugiej taśmie
                    while (nextInLine.modul > fromBuffer2.modul)
                    {
                        mainBuffer.add(fromBuffer2);
                        fromBuffer2 = nextInLine;
                        nextInLine = buffer2.get() ;
                    }
                }
                else
                {
                    fromBuffer1 = nextInLine;
                }
            }
        }

    }
}
