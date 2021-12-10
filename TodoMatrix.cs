using System;
using System.Collections.Generic;
using System.IO;

namespace EisenhowerCore
{

    
    public class TodoMatrix {

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
            else if(isUrgent && !isImportant)
            {
                TodoQuarters[QuarterType.NU].AddItem(title, deadline);
            }
            else if(!isUrgent && isImportant)
            {
                TodoQuarters[QuarterType.IN].AddItem(title, deadline);
            }
            else if(!isUrgent && !isImportant)
            {
                TodoQuarters[QuarterType.NN].AddItem(title, deadline);
            }
        }
        
        private bool IsUrgent(DateTime deadline)
        {
            DateTime today = DateTime.Today;
            if ((deadline - today).TotalDays >= 3 )
            {
                return false;
            }
            return true;
        }

        public void AddItemsFromFile(string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);
            foreach(string line in lines)
            {
                string[] item = line.Split('|');
                string title = item[0];
                string[] date = item[1].Split('-');
                bool isImportant;
                DateTime deadline = new DateTime(DateTime.Today.Year, Int32.Parse(date[1]), Int32.Parse(date[0]));
                if (item[2]==" ")
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
            foreach(TodoQuarter quarter in TodoQuarters.Values)
            {
                // List<TodoItem> list = quarter.GetItems();
                quarter.GetItems().RemoveAll(item => item.IsDone);
            }
            
        }

        public override string ToString()
        {
            return "-----------";
        }
    }


        
              


}

