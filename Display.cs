﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EisenhowerCore
{
    class Display
    {
        private string MainMenu = @" 
SHOW:
1. urgent & important items
2. urgent & not important items
3. not urgent & important items
4. not urgent & not important items
5. all items
6. QUIT
";

        private string QarterMenu = @"
1. Add item
2. Remove item
3. Mark item as done
4. Mark item as not done 
";

        private string MatrixMenu = @"5. Archive done items
6. Save items to csv
7. Load items from csv";

        public readonly string askForTitle = "Write new item title";
        public readonly string askForDeadline = "Provide the deadline YYYY-MM-DD";
        public readonly string isItImportant = "Is it important? y - for yes; n - for no";

        private Dictionary<QuarterType, string> headers = new Dictionary<QuarterType, string>() {
            { QuarterType.IU, "Urgent & Important" },
            { QuarterType.NU, "Urgent & Not Important" },
            { QuarterType.IN, "Not Urgent & Important" },
            { QuarterType.NN, "Not Urgent & Not Important" },
        };

        public void ClearScreen() => Console.Clear();

        public void PrintMessage(string message) => Console.WriteLine(message);
        
        public void PrintMainMenu() => Console.WriteLine(this.MainMenu);

        public void PrintSpecificMenu(int option)
        {
            // 1-4 show quarter; 5 is full matrix
            if (option <= 4)
            {
                Console.WriteLine(QarterMenu);
            }
            else
            {
                Console.WriteLine(QarterMenu + MatrixMenu);
            }
        } 


        public void DisplayQuarter(TodoQuarter quarter, QuarterType quarterType)
        {
            string header = headers[quarterType];
            ClearScreen();
            Console.WriteLine(header);
            Console.WriteLine(quarter.ToString());
        }

        public void DisplayMatrix(TodoMatrix matrix)
        {
            ClearScreen();
            Console.WriteLine(matrix);
        }
    }
}
