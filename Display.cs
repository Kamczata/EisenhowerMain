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
";

        private string MatrixMenu = @"
5. Archive done items
6. Save items to csv
7. Load items from csv";


        private Dictionary<QuarterType, string> headers = new Dictionary<QuarterType, string>() {
            { QuarterType.IU, "Urgent & Important" },
            { QuarterType.NU, "Urgent & Not Important" },
            { QuarterType.IN, "Not Urgent & Important" },
            { QuarterType.NN, "Not Urgent & Not Important" },
        };

        public void ClearScreen() => Console.Clear();
        

        public void PrintMainMenu() => Console.WriteLine(this.MainMenu);
        

        public void DisplayQuarter(TodoQuarter quarter, QuarterType quarterType)
        {
            string header = headers[quarterType];
            ClearScreen();
            Console.WriteLine(header);
            Console.WriteLine(quarter.ToString());
            Console.WriteLine(QarterMenu);
        }

        public void DisplayMatrix(TodoMatrix matrix)
        {
            ClearScreen();
            Console.WriteLine(matrix);
            Console.WriteLine(QarterMenu);
            Console.WriteLine(MatrixMenu);
        }
    }
}
