using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace EisenhowerCore
{


    public class TodoMatrix
    {

        private Dictionary<QuarterType, TodoQuarter> TodoQuarters = new Dictionary<QuarterType, TodoQuarter>();
        private Display display = new Display();

        public TodoMatrix()
        {
            TodoQuarters[QuarterType.IU] = new TodoQuarter();
            TodoQuarters[QuarterType.IN] = new TodoQuarter();
            TodoQuarters[QuarterType.NU] = new TodoQuarter();
            TodoQuarters[QuarterType.NN] = new TodoQuarter();
            //TodoQuarters[5] = new TodoQuarter();    
        }

        public Dictionary<QuarterType, TodoQuarter> GetQuarters() => TodoQuarters;

        public TodoQuarter GetQuarter(QuarterType status)
        {
            return TodoQuarters[status];
        }


        public void AddItem(int id, String title, DateTime deadline, bool isImportant, int matrixId)
        {
            bool isUrgent = IsUrgent(deadline);
            if (isUrgent && isImportant)
            {
                TodoQuarters[QuarterType.IU].AddItem(id, title, deadline, isImportant, matrixId);
            }
            else if (isUrgent && !isImportant)
            {
                TodoQuarters[QuarterType.NU].AddItem(id, title, deadline, isImportant, matrixId);
            }
            else if (!isUrgent && isImportant)
            {
                TodoQuarters[QuarterType.IN].AddItem(id, title, deadline, isImportant, matrixId);
            }
            else if (!isUrgent && !isImportant)
            {
                TodoQuarters[QuarterType.NN].AddItem(id, title, deadline, isImportant, matrixId);
            }
        }

        private bool IsUrgent(DateTime deadline)
        {
            DateTime today = DateTime.Today;
            if ((deadline - today).TotalDays >= 3)
            {
                return false;
            }

            return true;
        }

       

        public void ArchiveItems()
        {
            foreach (TodoQuarter quarter in TodoQuarters.Values)
            {
                // List<TodoItem> list = quarter.GetItems();
                quarter.GetItems().RemoveAll(item => item.IsDone);
            }

        }

        private string GenerateHalfMatrix(QuarterType quaterType1, QuarterType quaterType2)
        {
            string halfMatrix = $"";
            int quarterWidth = 40;
            int lines = 8;
            string wall = "|";
            string dash = "-";
            string space = " ";
            string emptyHalfLine = multiplySign(space, quarterWidth);

            int Amount1 = TodoQuarters[quaterType1].HowManyItems();
            int Amount2 = TodoQuarters[quaterType2].HowManyItems();
            int max = Math.Max(Amount1, Amount2);
            if (max > lines)
            {
                lines = max;
            }

            int refilHeader1 = quarterWidth - display.headers[quaterType1].Length;
            int refilHeader2 = quarterWidth - display.headers[quaterType2].Length;

            halfMatrix += display.headers[quaterType1] + multiplySign(space, refilHeader1) + wall + display.headers[quaterType2] + multiplySign(space, refilHeader2) + "\n";
            halfMatrix += multiplySign(dash, quarterWidth * 2 + 1) + "\n";
            for (int i = 0; i < lines; i++)
            {
                if (Amount1 == 0 || i + 1 > Amount1)
                {
                    if (Amount2 == 0 || i + 1 > Amount2)
                    {
                        halfMatrix += emptyHalfLine + wall + emptyHalfLine + "\n";
                    }
                    else
                    {
                        int refill = quarterWidth - TodoQuarters[quaterType2].GetItem(i).ToString().Length;
                        halfMatrix += emptyHalfLine + wall + TodoQuarters[quaterType2].GetItem(i) + multiplySign(space, refill) + "\n";
                    }
                }
                else
                {
                    if (Amount2 == 0 || i + 1 > Amount2)
                    {
                        int refill = quarterWidth - TodoQuarters[quaterType1].GetItem(i).ToString().Length;
                        halfMatrix += TodoQuarters[quaterType1].GetItem(i) + multiplySign(space, refill) + wall + emptyHalfLine + "\n";
                    }
                    else
                    {
                        int refill = quarterWidth - TodoQuarters[quaterType1].GetItem(i).ToString().Length;
                        int refill2 = quarterWidth - TodoQuarters[quaterType2].GetItem(i).ToString().Length;
                        halfMatrix += TodoQuarters[quaterType1].GetItem(i) + multiplySign(space, refill) + wall + TodoQuarters[quaterType2].GetItem(i) + multiplySign(space, refill2) + "\n";
                    }
                }
            }
            halfMatrix += multiplySign(dash, quarterWidth * 2 + 1) + "\n";

            return halfMatrix;


        }

        public override string ToString()
        {
            string matrix = $"";
            matrix += GenerateHalfMatrix(QuarterType.IU, QuarterType.IN);
            matrix += GenerateHalfMatrix(QuarterType.NU, QuarterType.NN);

            return matrix;
        }

        public string multiplySign(string sign, int multiplier)
        {
            if (multiplier < 0)
            {
                multiplier = 1;
            }
            return String.Concat(Enumerable.Repeat(sign, multiplier));
        }
    }
    






}

