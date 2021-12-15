using EisenhowerCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                string userChoice = input.UserInput(input.userInputMainMenu);
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
                    string userInput = input.UserInput(input.choiceInsideQuarter);
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
                string userInput = input.UserInputInQuarterItemChoice(howManyItems);
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

        private bool HaveItemsToRemove(QuarterType quarterType) => matrix.GetQuarter(quarterType).GetItems().Any();

        public void CarryAction(int action, QuarterType quarterType)
        {
            
            // All options should contain proper input and display needed to carry full operation
            //1 - add item
            

            if (action == 1)
            {
                // if (quarterType == QuarterType.Matrix)
                // {
                //     display.PrintMessage(display.chooseQuarterType);
                //     QuarterType quarter = input.PickQuarterType();
                //     quarterType = quarter;
                // }
                var NewItemData = CreateTitleDateImportanceForItem(); 
                // So basically I started to organise this mess ^^, but still, there is a lot of to do. :D 
                matrix.AddItem(NewItemData.Item1, NewItemData.Item2, NewItemData.Item3);
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
                        if (userChoice.ToLower() == "y" || userChoice.ToLower() == "yes")
                        {   
                            matrix.ArchiveItems();
                        }
                        else
                        {
                            //Ok, not the best option to leave this field empty, but it works. :p
                        }
                    }

                    //3 - save to csv
                    else if (action == 3)
                    {
                        display.PrintMessage(display.askForFilename);
                        string filename = input.UserInputSaveToFile();
                        matrix.SaveItemsToFile(filename);
                        display.PrintMessage(display.confirmationFilesSaved);
                        input.PressAnyKey();
                    }
                    //4 - load from csv
                    else if (action == 4)
                    {
                        display.PrintMessage(display.provideFilepath);
                        string filepath = input.UserInput(); //How to check if user input is the same as filepath?
                        matrix.AddItemsFromFile(filepath); //There is no escape if we dont have any file at this moment. ;)
                    }
                    else if (action == 5)
                    {
                        input.PressAnyKey();
                    }

                }
                else 
                {
                    // DO AKCJI 2-4 PRZYDALABY SIE POMOCNICZA FUNKCJA ItemPicker 
                    //bool haveDoneItems = HasDoneOrUndoneItems("done", quarterType);
                    //bool haveUndoneItems = HasDoneOrUndoneItems("undone", quarterType);
                    if (action == 2)
                    {
                        bool haveItemsToRemove = HaveItemsToRemove(quarterType);
                        if (haveItemsToRemove)
                        {
                            int indexOfPickedItem = ItemPicker(quarterType);
                            matrix.GetQuarter(quarterType).RemoveItem(indexOfPickedItem);
                        }
                        else
                        {
                            display.PrintMessage(display.noItemsToRemove);
                            input.PressAnyKey();
                        }
                    }
                    else if (action == 3)
                    {
                        bool haveUndoneItems = HasDoneOrUndoneItems("undone", quarterType);
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
                        bool haveDoneItems = HasDoneOrUndoneItems("done", quarterType);
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
                input.PressAnyKey();
            }
            
        }
        (string, DateTime, bool) CreateTitleDateImportanceForItem()
        {
            display.PrintMessage(display.askForTitle);
            string title = input.UserInputNewItemTitle();

            display.PrintMessage(display.askForDeadline);
            string deadline = input.UserInputNewItemDate();
            DateTime convDeadline = input.ConvertDeadline(deadline);

            display.PrintMessage(display.isItImportant);
            string isImportant = input.UserInput(input.isItemImportant);
            bool IsImportantOrNot = input.CheckImportance(isImportant);

            return (title, convDeadline, IsImportantOrNot);
        }
    }
}
