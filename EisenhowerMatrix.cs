using EisenhowerCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EisenhowerCore
{
    class EisenhowerMatrix
    {
        // implement all programm logic
        public TodoMatrix matrix = new TodoMatrix();
        private Display display = new Display();
        private Input input = new Input();

        public void MatrixLogic()
        {
            
            while(true)
            {
                display.ClearScreen();
                display.PrintMainMenu();
                string option = input.ChooseOption();
                if (option == "6")
                {
                    System.Environment.Exit(1);
                }
                else
                {
                    ShowItems(option);
                    input.PressAnyKey();
                }
            }
            
        }

        public void ShowItems(string userChoice)
        {
            int option = Int32.Parse(userChoice);
            QuarterType quarterType;
            TodoQuarter quarter;
            if ( option < 5)
            {
                
                if (option == 1)
                {
                    quarterType = QuarterType.IU;
                    quarter = matrix.GetQuarter(quarterType);
                }
                else if (option == 2)
                {
                    quarterType = QuarterType.NU;
                    quarter = matrix.GetQuarter(quarterType);
                }
                else if (option == 3)
                {
                    quarterType = QuarterType.IN;
                    quarter = matrix.GetQuarter(quarterType);
                }
                else
                {
                    quarterType = QuarterType.NN;
                    quarter = matrix.GetQuarter(quarterType);
                }

                display.DisplayQuarter(quarter, quarterType);
            }
            else
            {
                Console.WriteLine("THERE WILL BE MATRIX");
            }

        }

    }
}
