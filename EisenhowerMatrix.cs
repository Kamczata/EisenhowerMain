using EisenhowerCore;
using System;
using System.Collections.Generic;
using System.Net;
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
                    if (option <5)
                    {
                        display.DisplayQuarter(matrix.GetQuarter(quarterType), quarterType);
                    }
                    else
                    {
                        display.DisplayMatrix(this.matrix);
                    }
                    display.PrintSpecificMenu(quarterType);
                    string userInput2 = input.UserInput();
                    int userAction2 = Int32.Parse(userInput2);
                    CarryAction(userAction2, quarterType);
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

        private bool HasDoneOrUndoneItems(string doneOrUndone, QuarterType quarterType)
        {
            bool hasDoneOrUndoneItems = false;
            foreach (TodoItem item in matrix.GetQuarter(quarterType).GetItems())
            {
                
                if (doneOrUndone == "done")
                {
                    if (item.IsDone)
                    {
                        return true;
                    }
                }
                else if (doneOrUndone == "undone")
                {
                    if(!item.IsDone)
                    {
                        return true;
                    }
                }
            }
            return hasDoneOrUndoneItems;
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
                    if (action == 2)
                    {
                        display.PrintMessage(display.askForConfirmation);
                        string userChoice = input.UserInput();
                        if (userChoice == "y")
                        {   
                            matrix.ArchiveItems();
                        }
                        else if (userChoice == "n")
                        {
                            //Puste wraca do głownego menu :p
                        }
                    }

                    //3 - save to csv
                    else if (action == 3)
                    {
                        display.PrintMessage(display.askForFilename);
                        string filename = input.UserInput();
                        matrix.SaveItemsToFile(filename);
                        display.PrintMessage(display.confirmationFilesSaved);
                        input.PressAnyKey();
                    }
                    //4 - load from csv
                    else if (action == 4)
                    {
                        display.PrintMessage(display.provideFilepath);
                        string filepath = input.UserInput();
                        matrix.AddItemsFromFile(filepath);
                    }
                    else if (action == 5)
                    {
                        input.PressAnyKey();
                    }

                }
                else 
                {
                    // DO AKCJI 2-4 PRZYDALABY SIE POMOCNICZA FUNKCJA ItemPicker 
                    bool haveDoneItems = HasDoneOrUndoneItems("done", quarterType);
                    bool haveUndoneItems = HasDoneOrUndoneItems("undone", quarterType);
                    if (action == 2)
                    {
                        int indexOfPickedItem = ItemPicker(quarterType);
                        matrix.GetQuarter(quarterType).RemoveItem(indexOfPickedItem);
                    }
                    else if (action == 3)
                    {
                        if (haveUndoneItems) 
                        {
                            int indexOfPickedItem = ItemPicker(quarterType);
                            while (matrix.GetQuarter(quarterType).GetItem(indexOfPickedItem).IsDone && indexOfPickedItem>0)
                            {
                                display.PrintMessage(display.itemAlreadyDone);
                                indexOfPickedItem = ItemPicker(quarterType);
                            }
                            matrix.GetQuarter(quarterType).GetItem(indexOfPickedItem).Mark(); //TO ZAZNACZA ALE CZY W DOBRYM MIEJSCU?
                        }
                        else
                        {
                            display.PrintMessage(display.noItemsToPick);
                            input.PressAnyKey();
                        }
                        
                    }
                    else if (action == 4)
                    {
                        if (haveDoneItems)
                        {
                            int indexOfPickedItem = ItemPicker(quarterType);
                            while (!matrix.GetQuarter(quarterType).GetItem(indexOfPickedItem).IsDone && indexOfPickedItem > 0)
                            {
                                display.PrintMessage(display.itemAlreadyNotDone);
                                indexOfPickedItem = ItemPicker(quarterType);
                            }
                            matrix.GetQuarter(quarterType).GetItem(indexOfPickedItem).UnMark();
                        }
                        else
                        {
                            display.PrintMessage(display.noItemsToPick);
                            input.PressAnyKey();
                        }
                    }
                    else if (action == 5)
                    {
                        input.PressAnyKey();
                    }
                    

                }
            }
            else if (action == 5)
            {
                //Najlepsze rozwiązanie to zostawić to puste i wtedy samo wróci do głównego menu :p
                input.PressAnyKey();
            }
            
        }

    }
}
