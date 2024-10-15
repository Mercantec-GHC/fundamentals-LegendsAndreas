using System;

class Program
{ 
    public class BankoPlade
    {
        string name;
        Row Row1 = new Row();
        Row Row2 = new Row();
        Row Row3 = new Row();
    }

    public class Row
    {
        int[] numbers = new int[9];
        bool lineStatus = false;

        static public int[] CreateRow()
        {
            int[] row = new int[9];
            int[] indexes = getIndexies();
            row = getNumbers(indexes);


            return row;
        }

        static public int[] getNumbers(int[] indexes)
        {

            //List<int> bingoNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            //30, 31, 32, 33, 34, 35, 36, 37,38, 39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,
            //76,77,78,79,80,81,822,83,84,85,86,87,88,89,90};
            int number;
            Random r = new Random();

            int[] row = new int[9];

            foreach (int index in indexes)
            {
                // For the last column of a Bingo Board, we use different code, because we also need to include 90.
                if (index == 8)
                {
                    int adder = 10 * index;

                    number = r.Next(0, 11) + adder;
                    row[index] = index;
                }
                else
                {
                    int adder = 10 * index;

                    number = r.Next(0, 10) + adder;
                    row[index] = index;
                }
            }

            return row;
        }

        static public int[] getIndexies()
        {
            int[] indexes = new int[5]; // Indexes where our values will be placed.
            List<int> availabileIndexes = new List<int>{0,1,2,3,4,5,6,7,8}; // List, so that we can change the size.

            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                int j = r.Next(0, availabileIndexes.Count);
                indexes[i] = availabileIndexes[j];
                availabileIndexes.RemoveAt(j);
            }

            return indexes;
        }
    }

    static void Main()
    {
        BankoPlade bankoPlade = new BankoPlade();
        bankoPlade

    }
}