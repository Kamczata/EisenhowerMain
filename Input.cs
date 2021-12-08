using System;
using System.Collections.Generic;
using System.Text;

namespace EisenhowerCore
{
    class Input
    {
        public string UserInput() => Console.ReadLine();
        

        public ConsoleKeyInfo PressAnyKey() => Console.ReadKey();

        public DateTime ConvertDeadline(string deadline)
        {
            string[] dateList = deadline.Split('-');
            int year = Int32.Parse(dateList[0]);
            int month = Int32.Parse(dateList[1]);
            int day = Int32.Parse(dateList[2]);
            DateTime convDeadline = new DateTime(year, month, day);
            return convDeadline;
        }

        public bool ConvertImportance(string isImportant)
        {
            if (isImportant == "y")
            {
                return true;
            }
            return false;
        }

        public string WhichMenu(int option)
        {
            if (option <= 4)
            {
                return "quarter";
            }
            return "matrix";
        }
        
    }
}
