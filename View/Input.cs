using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace EisenhowerCore
{
    class Input
    {
        public string numberOfItemsInQuarter(string number) => number; 
        private Display display = new Display();
        public readonly List<string> userInputMainMenu = new List<string>() { "1", "2", "3", "4", "5", "6" };
        public readonly List<string> choiceInsideQuarter = new List<string>() { "1", "2", "3", "4", "5" };
        public readonly List<string> isItemImportant = new List<string>() { "y", "n", "yes", "no" };
        public readonly List<string> QuarterTypes = new List<string>() { "IU", "NU", "IN", "NN" };

        bool checkIfInputIsAlphaOrNumbers(string input) => Regex.IsMatch(input, @"^[a-zA-Z0-9_]+$");
        
        public string UserInput(List<string> inputValidationFromList)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (inputValidationFromList.Contains(input.ToLower()))
                {
                    return input;
                }
                display.DisplayInfoAboutWrongInput();
                        
            }
        }

        public string UserInputInQuarterItemChoice(int quantityOfItems)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (Convert.ToInt32(input) < quantityOfItems)
                {
                    return input;
                }
                display.DisplayInfoAboutWrongInput();
            }
        }

        public string UserInputNewItemTitle()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input.Length >= 1 && checkIfInputIsAlphaOrNumbers(input))
                {
                    return input;
                }
                display.DisplayInfoAboutWrongInput();
            }
        }
        
        public string UserInputNewItemDate()
        {
            while (true)
            {
                string format = "yyyy-mm-dd";
                string input = Console.ReadLine();
                DateTime dateTime;
                if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    return input;
                }
                display.DisplayInfoAboutWrongInput();
            }
        }
        
        
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

        public bool CheckImportance(string isImportant)
        {
            return isImportant == "y" || isImportant == "yes";
        }

        public string UserInputSaveToFile()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input.Length >= 5 && input.Contains("."))
                {
                    string firstPart = input.Split('.')[0];
                    string secondPart = input.Split('.')[1];
                    if (checkIfInputIsAlphaOrNumbers(firstPart) && secondPart == "csv" || secondPart == "txt")
                    {
                        return input;
                    }         
                    display.DisplayInfoAboutWrongInput();
                }
                
                display.DisplayInfoAboutWrongInput();
            }
        }


        
    }
}
