using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;

namespace EisenhowerCore
{


    public class TodoMatrix
    {

        private Dictionary<QuarterType, TodoQuarter> TodoQuarters = new Dictionary<QuarterType, TodoQuarter>();

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


        public void AddItem(String title, DateTime deadline, bool isImportant)
        {
            bool isUrgent = IsUrgent(deadline);
            if (isUrgent && isImportant)
            {
                TodoQuarters[QuarterType.IU].AddItem(title, deadline);
            }
            else if (isUrgent && !isImportant)
            {
                TodoQuarters[QuarterType.NU].AddItem(title, deadline);
            }
            else if (!isUrgent && isImportant)
            {
                TodoQuarters[QuarterType.IN].AddItem(title, deadline);
            }
            else if (!isUrgent && !isImportant)
            {
                TodoQuarters[QuarterType.NN].AddItem(title, deadline);
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

        public void AddItemsFromFile(string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] item = line.Split('|');
                string title = item[0];
                string[] date = item[1].Split('-');
                bool isImportant;
                DateTime deadline = new DateTime(DateTime.Today.Year, Int32.Parse(date[1]), Int32.Parse(date[0]));
                if (item[2] == " ")
                {
                    isImportant = false;
                }
                else
                {
                    isImportant = true;
                }

                this.AddItem(title, deadline, isImportant);

            }
        }


        public void SaveItemsToFile(string fileName) //https://www.youtube.com/watch?v=vDpww7HsdnM
        {
            foreach (KeyValuePair<QuarterType, TodoQuarter> quarter in TodoQuarters)
            {
                var list = quarter.Value.GetItems();
                foreach (var item in list)
                {
                    try
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@fileName, true))
                        {
                            if (item.IsDone && quarter.Key == QuarterType.IN || quarter.Key == QuarterType.IU)
                            {
                                file.WriteLine($"{item.Title}|{item.Deadline}|is_important");
                            }
                            else
                            {
                                file.WriteLine($"{item.Title}|{item.Deadline}| ");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("This program made an error: ", ex);
                    }

                }
            }

        }

        public void ArchiveItems()
        {
            foreach (TodoQuarter quarter in TodoQuarters.Values)
            {
                // List<TodoItem> list = quarter.GetItems();
                quarter.GetItems().RemoveAll(item => item.IsDone);
            }

        }

        public override string ToString()
        {
            string matrix = $"";
            foreach (KeyValuePair<QuarterType, TodoQuarter> quarter in TodoQuarters)
            {
                matrix += $"{multiplySign("-", 50)}{Environment.NewLine}";
                matrix += $"{Display.headers[quarter.Key]}{Environment.NewLine}";
                matrix += $"{multiplySign("-", 50)}{Environment.NewLine}";
                matrix += $"{quarter.Value}{Environment.NewLine}";

            }

            return matrix;
            // var emptyMatrix =  GenerateEmptyMatrix();
            // return replaceFieldsInMatrixWithItems(emptyMatrix);

        }

        //     private string GenerateEmptyMatrix()
        //     {
        //         string board = @"";
        //         
        //         
        //         
        //         string important = CreatePartWithName("IMPORTANT");
        //         string notImportant = CreatePartWithName("NOT IMPORTANT");
        //         string rightSideSeparator = multiplySign("-", 50);
        //         string emptySpace = multiplySign(" ", 50);
        //         string Urgent = multiplySign(" ", 22);
        //         string NotUrgent = multiplySign(" ", 20);
        //         string horizontalSeparator = $"--|{rightSideSeparator}-{rightSideSeparator}|--{Environment.NewLine}";
        //         string urgentNotUrgent = $"  |{Urgent}URGENT{Urgent}|{NotUrgent}NOT URGENT{NotUrgent}|  {Environment.NewLine}";
        //         board += horizontalSeparator;
        //         board += urgentNotUrgent;
        //         board += horizontalSeparator;
        //         board += important;
        //         board += horizontalSeparator;
        //         board += notImportant;
        //         board += horizontalSeparator;
        //         return board;
        //     }
        //
        //     string CreatePartWithName(string text) {
        //         string emptySpace = multiplySign(" ", 50);
        //         string emptyLine = $"  |{emptySpace}|{emptySpace}|  {Environment.NewLine}";
        //         string field = "";
        //         field += emptyLine;
        //         field += emptyLine;
        //         for (int i = 0; i < text.Length; i++)
        //         {
        //             field += $"{text[i]} |{emptySpace}|{emptySpace}|  {Environment.NewLine}";
        //         }
        //         field += emptyLine;
        //         field += emptyLine;
        //
        //         return field;
        //     }
        //
        string multiplySign(string sign, int multiplier)
        {
            return String.Concat(Enumerable.Repeat(sign, multiplier));
        }
        //
        //     string replaceFieldsInMatrixWithItems(string board)
        //     {
        //         TodoMatrix matrix = new TodoMatrix();
        //         int counter = 0;
        //         var allQuarters = matrix.GetQuarters();
        //         foreach (TodoQuarter toDoQuarter in allQuarters.Values)
        //         {
        //             if (toDoQuarter.GetItems().Any() == false)
        //             {
        //                 counter++;
        //             }
        //             // split replace
        //         }
        //
        //         if (counter == 4)
        //         {
        //             return board;
        //         }
        //         else
        //         {
        //             foreach (KeyValuePair<QuarterType, TodoQuarter> quarter in allQuarters)
        //             {
        //                 if (quarter.Value.GetItems().Count >= 1)
        //                 {
        //                     if (quarter.Key == QuarterType.IN)
        //                     {
        //                         using (StringReader reader = new StringReader(board))
        //                         {
        //                             string line;
        //                             line = reader.ReadLine();
        //                             for (int i = 3; i < 15; i++)
        //                             {
        //                                 while ((line = reader.ReadLine()) != null)
        //                                 {
        //                                 
        //                                 }
        //                             }
        //                             
        //                            
        //                             
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     
        // }
        //
        //
        // foreach (string line in new LineReader(() => new StringReader(text)))
        // {
        // Console.WriteLine(line);
        // }



    }
}

