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
                string userChoice = input.UserInput();
                int option = Int32.Parse(userChoice);
                if (option == 6)
                {
                    System.Environment.Exit(1);
                }
                else
                {
                    ShowItems(option);
                    display.PrintSpecificMenu(option);
                    string userInput = input.UserInput();
                    int userAction = Int32.Parse(userInput);
                    // while(true)??
                    CarryAction(userAction);
                }
            }
            
        }

        public void ShowItems(int option)
        {
            
            QuarterType quarterType;
            TodoQuarter quarter;
            if ( option < 5)
            {
                
                if (option == 1)
                {
                    quarterType = QuarterType.IU;
                }
                else if (option == 2)
                {
                    quarterType = QuarterType.NU;
                }
                else if (option == 3)
                {
                    quarterType = QuarterType.IN;
                }
                else
                {
                    quarterType = QuarterType.NN;   
                }
                quarter = matrix.GetQuarter(quarterType);
                display.DisplayQuarter(quarter, quarterType);
            }
            else
            {
                display.DisplayMatrix(this.matrix);
            }

            
        }
        public void CarryAction(int option)
        {
            // All options should contain proper input and display needed to carry full operation
            //1 - add item
            if (option == 1)
            {
                display.PrintMessage(display.askForTitle);
                string title = input.UserInput();

                display.PrintMessage(display.askForDeadline);
                string deadline = input.UserInput();
                DateTime convDeadline = input.ConvertDeadline(deadline);

                display.PrintMessage(display.isItImportant);
                string isImportant = input.UserInput();
                bool convIsImportant = input.ConvertImportance(isImportant);

                matrix.AddItem(title, convDeadline, convIsImportant);
            }
            //2 - remove item
            //3 - mark item as done
            //4 - unmark item
            //5 - archive done items
            //6 - save to csv
            //7 - load from csv
        }

    }
}
