using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerCore
{
    public class TodoMatrixManager
    {
        private Display _display = new Display();
        private string _allMatrixMenuName = "Matrixes in database:";
        private TodoQuarterManager _quarterManager = new TodoQuarterManager();


        public void CreateNewMatrix(TodoMatrixDao matrixDao, TodoItemDao itemDao)
        {
            _display.ClearScreen();
            _display.PrintMessage(_display.ProvideMatrixTitle);
            string matrixName = _display.UserInput();
            TodoMatrix newMatrix = matrixDao.Add(matrixName);
            _display.PrintMatrixName(newMatrix);
            _display.DisplayMatrix(newMatrix);
            Run(matrixDao, itemDao, newMatrix);

        }

        public void DisplayAllMatrixes(TodoMatrixDao matrixDao, TodoItemDao itemDao)
        {
            _display.ClearScreen();
            List <TodoMatrix> allMatrix = matrixDao.GetAllTitles();
            string backToMenuIndex = (allMatrix.Count() + 1).ToString();
            _display.PrintMenuName(_allMatrixMenuName);
            foreach(TodoMatrix matrix in allMatrix)
            {
                _display.PrintMatrixTitleWithId(matrix);
            }

            _display.PrintOption(backToMenuIndex, _display.Back);
            _display.PrintMessage(_display.ChooseOption);
            int userChoice = Convert.ToInt32(_display.UserInput());
            var exists = allMatrix.ElementAtOrDefault(userChoice) != null;

            if (exists)
            {
                DisplayMatrix(allMatrix[userChoice-1], matrixDao, itemDao);
            }
            else
            {
                _display.PressAnyKey();
            }

        }

        private void DisplayMatrix(TodoMatrix matrix, TodoMatrixDao matrixDao, TodoItemDao itemDao)
        {
            _display.ClearScreen();
            _display.PrintMatrixName(matrix);
            List<TodoItem> items = itemDao.GetAll(matrix.Id);
            matrix.PlaceItems(items);
            //_display.DisplayMatrix(matrix);
            Run(matrixDao, itemDao, matrix);

        }

        private void Run(TodoMatrixDao matrixDao, TodoItemDao itemDao, TodoMatrix matrix)
        {

                _display.ClearScreen();
                _display.DisplayMatrix(matrix);
                _display.PrintOption("1", "Add item");
                _display.PrintOption("2", "Archive done items");
                _display.PrintOption("3", "Urgent & Important items");
                _display.PrintOption("4", "Urgent & Not important items");
                _display.PrintOption("5", "Not urgent & Important items");
                _display.PrintOption("6", "Not urgent & Not important items");
                _display.PrintOption("7", "Back");

                _display.PrintMessage(_display.ChooseOption);
                string userChoice = _display.UserInput();

                switch (userChoice)
                {
                    case "1":
                        _quarterManager.AddItem(itemDao, matrix);
                        break;

                    case "2":
                        matrixDao.ArchiveDoneItems(matrix.Id);
                        break;

                    case "3":
                        _quarterManager.DisplayQuarter(QuarterType.IU, matrix.GetQuarter(QuarterType.IU), itemDao, matrix);
                        break;

                    case "4":
                        _quarterManager.DisplayQuarter(QuarterType.NU, matrix.GetQuarter(QuarterType.NU), itemDao, matrix);
                        break;

                    case "5":
                        _quarterManager.DisplayQuarter(QuarterType.IN, matrix.GetQuarter(QuarterType.IN), itemDao, matrix);
                        break;

                    case "6":
                        _quarterManager.DisplayQuarter(QuarterType.NN, matrix.GetQuarter(QuarterType.NN), itemDao, matrix);
                        break;

                    case "7":
                        break;
                
            }

           
        }

    }
}
