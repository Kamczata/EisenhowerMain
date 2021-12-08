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
                QuarterType quarterType = QuarterType.Matrix;
                if (option == 6)
                {
                    System.Environment.Exit(1);
                }
                else
                {
                    if (option < 5)
                    {
                        quarterType = ShowItems(option);
                    }
                    else
                    {
                        display.DisplayMatrix(this.matrix);
                    }
                    
                    display.PrintSpecificMenu(quarterType);
                    string userInput = input.UserInput();
                    int userAction = Int32.Parse(userInput);
                    
                    CarryAction(userAction, quarterType);
                    
                    
                }
            }
            
        }

        public QuarterType ShowItems(int option)
        {
            
            QuarterType quarterType;
            TodoQuarter quarter;
              
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
            return quarterType;
           
        }

        public int ItemPicker(QuarterType quarterType)
        {
            int howManyItems = matrix.GetQuarter(quarterType).HowManyItems();
            int itemIndex = 0;
            if (howManyItems > 1)
            {
                display.PickItem(howManyItems);
                string userInput = input.UserInput();
                itemIndex = Int32.Parse(userInput) - 1;
            }
            return itemIndex;
        }

        public void CarryAction(int action, QuarterType quarterType)
        {
            // All options should contain proper input and display needed to carry full operation
            //1 - add item
            if (action == 1)
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
            else if (action > 1 && action < 5)
            {
                if(quarterType == QuarterType.Matrix)
                {
                    //2 - archive done items
                    //3 - save to csv
                    //4 - load from csv
                }
                else 
                {
                    // DO AKCJI 2-4 PRZYDALABY SIE POMOCNICZA FUNKCJA ItemPicker 
                    int indexOfPickedItem = ItemPicker(quarterType);
                    //2 - remove item
                    //3 - mark item as done
                    //4 - unmark item
                }
            }
            else
            {
                // somehow back to main menu
            }
            
            
            
            
        }

    }
}
