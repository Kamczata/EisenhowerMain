using System;
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
5. Back to main menu
";

        private string MatrixMenu = @"
1. Add item
2. Archive done items
3. Save items to csv
4. Load items from csv
5. Back to main menu";

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

        public void PrintSpecificMenu(string whichMenu)
        {
            // 1-4 show quarter; 5 is full matrix
            if (whichMenu == "quarter")
            {
                Console.WriteLine(QarterMenu);
            }
            else
            {
                Console.WriteLine(MatrixMenu);
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

        public void PickItem(int howManyItems)
        {
            string message = $"Pick item from 1 to {howManyItems}";
            PrintMessage(message);
                  
        }
    }
}
