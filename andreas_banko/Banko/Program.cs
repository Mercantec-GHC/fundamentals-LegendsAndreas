using System;

class Program
{
    public class BingoBankoSpil
    {
        BankoPlade[] bankoPlader;
        List<int> usedBingoNumbers = new();

        /// <summary>
        /// Constructor for BingoBankoSpil.
        /// Takes the amount of bingo plates as parameter.
        /// </summary>
        /// <param name="initSize"></param>
        public BingoBankoSpil(int initSize)
        {
            bankoPlader = new BankoPlade[initSize];

            for (int i = 0; i < bankoPlader.Length; i++)
            {
                bankoPlader[i] = new BankoPlade();
                bankoPlader[i].SetName("" + i);
                bankoPlader[i].CreateRows();
            }
        }

        public void ErrorCheckingPlates()
        {
            foreach (var plade in bankoPlader)
            {
                plade.ErrorCheckingRows();
            }
        }

        public void ResolvePlates()
        {
            for (int i = 0;i < 90;i++)
            {
                int bingoNumber = GetBingoNumber();
                foreach (BankoPlade plade in bankoPlader)
                {
                    plade.CheckPlateNumber(bingoNumber);
                }
            }
        }

        public int GetBingoNumber()
        {
            Random randCaller = new Random();
            while (true)
            {
                int bingoNumber = randCaller.Next(1,91);
                if (usedBingoNumbers.Contains(bingoNumber))
                {
                    continue;
                }
                else
                {
                    usedBingoNumbers.Add(bingoNumber);
                    return bingoNumber;
                }
            }
        }

        public void PrintGame()
        {
            foreach (BankoPlade plade in bankoPlader)
            {
                plade.PrintPlade();
            }
        }
    }
    public class BankoPlade
    {
        string? name;
        List<int> usedBinogNumber = new();
        int earnedRows = 0;
         Row Row1 = new();
         Row Row2 = new();
         Row Row3 = new();

        public void BeginBanko()
        {

            // Pre-game setup
            int bingoNumber = 0;
            if (name == null)
            {
                Console.Write("Please insert name>");
                name = Console.ReadLine();
            }

            CreateRows();

            PrintPlade();

            while (!Row1.lineStatus || !Row2.lineStatus || !Row3.lineStatus)
            {
                bingoNumber = GetBingoNumber();
                CheckPlateNumber(bingoNumber);
            }

            if (Row1.lineStatus || Row2.lineStatus || Row3.lineStatus)
            {
                Console.WriteLine("You won the whole thang!");
            }

        }

        public void ErrorCheckingRows()
        {
            int rowOneCounter = Row1.ExamineRow();
            int rowTwoCounter = Row2.ExamineRow();
            int rowThreeCounter = Row3.ExamineRow();

            if (rowOneCounter != 4)
            {
                Console.WriteLine("Error at row 1");
            }
            else if (rowTwoCounter != 4)
            {
                Console.WriteLine("Error at row 2");
            }
            else if (rowThreeCounter != 4 )
            {
                Console.WriteLine("Error at row 3");
            }
            else
            {
                Console.WriteLine("We good");
            }
        }

        public void CheckPlateNumber(int bingoNumber)
        {
            int bingoNumberIndex = bingoNumber / 10;
            if (bingoNumberIndex == 9)
            {
                bingoNumberIndex--;
            }

            if (!Row1.lineStatus && Row1.numbers[bingoNumberIndex] == bingoNumber)
            {
                Row1.numbers[bingoNumberIndex] = 99;
                Row1.points++;
                if (Row1.points == 5)
                {
                    Row1.lineStatus = true;
                    Row1.points++;
                    earnedRows++;
                    if (earnedRows < 3)
                    {
                        Console.WriteLine("You got row 1!");
                    }
                }

                Console.WriteLine("Row1" + Row1.points);
                Console.WriteLine(bingoNumber);
                PrintPlade();
            }
            else if (!Row2.lineStatus && Row2.numbers[bingoNumberIndex] == bingoNumber)
            {
                Row2.numbers[bingoNumberIndex] = 99;
                Row2.points++;
                if (Row2.points == 5)
                {
                    Row2.lineStatus = true;
                    Row2.points++;
                    earnedRows++;
                    if (earnedRows < 3)
                    {
                        Console.WriteLine("You got row 2!");
                    }
                }

                Console.WriteLine("Row2" + Row2.points);
                Console.WriteLine(bingoNumber);
                PrintPlade();
            }
            else if (!Row3.lineStatus && Row3.numbers[bingoNumberIndex] == bingoNumber)
            {
                Row3.numbers[bingoNumberIndex] = 99;
                Row3.points++;
                if (Row3.points == 5)
                {
                    Row3.lineStatus = true;
                    Row3.points++;
                    earnedRows++;
                    if (earnedRows < 3)
                    {
                        Console.WriteLine("You got row 3!");
                    }
                }

                Console.WriteLine("Row3" + Row3.points);
                Console.WriteLine(bingoNumber);
                PrintPlade();
            }
        }

        public int GetBingoNumber()
        {
            Random r = new();
            return r.Next(1,91);
        }

        public void CreateRows()
        {
            List<int> usedNumbers = new();
            Row1.CreateRow(usedNumbers);
            Row2.CreateRow(usedNumbers);
            Row3.CreateRow(usedNumbers);
        }

        public void PrintPlade()
        {
            Console.WriteLine($"{name}:");
            PrintRow(Row1);
            PrintRow(Row2);
            PrintRow(Row3);
        }

        private void PrintRow(Row row)
        {
            foreach (int elm in row.numbers)
            {
                string elementStr = elm.ToString();

                Console.Write(elementStr.PadRight(3));
            }
            Console.WriteLine();
        }

        public void SetName(string passedName)
        {
            name = passedName;
        }
    }

    private class Row
    {
        public int[] numbers = new int[9];
        public int points = 0;
        public bool lineStatus = false;

        // The full creation of a row.
        public void CreateRow(List<int> usedNumbers)
        {
            int[] indexes = getIndexies(); // Where the numbers will be placed.
            numbers = GetNumbers(indexes, usedNumbers); // What numbers will be placed.
        }
        public int ExamineRow()
        {
            int zeroCounter = 0;
            foreach (int number in numbers)
            {
                if (number == 0)
                {
                    zeroCounter++;
                }
            }
            return zeroCounter;
        }

        public int[] GetNumbers(int[] indexes, List<int> usedNumbers)
        {
            int number;
            Random r = new();

            int[] row = new int[9];

            foreach (int index in indexes)
            {
                do
                {
                // For the last column of a Bingo Board, we use different code, because we also need to include 90.
                    if (index == 8)
                    {
                        int adder = 10 * index;

                        number = r.Next(1, 11) + adder;
                    }
                    else
                    {
                        int adder = 10 * index;

                        number = r.Next(1, 10) + adder;
                    }

                }
                while (CheckDuplicateNumber(usedNumbers, number));

                usedNumbers.Add(number);
                row[index] = number;
            }

            return row;
        }

        public bool CheckDuplicateNumber(List<int> usedNumbers, int number)
        {
            if (usedNumbers.Contains(number))
            {
                Console.WriteLine("Duplicate number " + number + " found");
                return true;
            }
            else
            {
                return false;
            }
        }

        public int[] getIndexies()
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
        BingoBankoSpil spil = new(30);
        spil.ErrorCheckingPlates();
        
    }
}

//List<int> bingoNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
//30, 31, 32, 33, 34, 35, 36, 37,38, 39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,
//76,77,78,79,80,81,822,83,84,85,86,87,88,89,90};