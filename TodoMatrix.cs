using System;
using System.Collections.Generic;
using System.IO;

namespace EisenhowerCore
{
    public class TodoMatrix {

        private Dictionary<string, TodoQuarter> TodoQuarters;

        /*public TodoMatrix()
        {
            TodoQuarters = new Dictionary<string, TodoQuarter>();
            List<TodoItem> todoItems = new List<TodoItem>();
            TodoQuarter quarter = new TodoQuarter(todoItems);
            TodoQuarters.Add("IU", quarter);
            TodoQuarters.Add("IN", quarter);
            TodoQuarters.Add("NU", quarter);
            TodoQuarters.Add("NN", quarter);
        }*/
  
    
        public void AddItem(String title, DateTime deadline, bool isImportant)
        {
            bool isUrgent = IsUrgent(deadline);
            if (isUrgent && isImportant)
            {
                TodoQuarters["IU"].AddItem(title, deadline);
            }
            else if(isUrgent && !isImportant)
            {
                TodoQuarters["NU"].AddItem(title, deadline);
            }
            else if(!isUrgent && isImportant)
            {
                TodoQuarters["IN"].AddItem(title, deadline);
            }
            else if(!isUrgent && !isImportant)
            {
                TodoQuarters["NN"].AddItem(title, deadline);
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
                if (item[2]=="")
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
            foreach (TodoQuarter quarter in TodoQuarters.Values)
            {
                var list = quarter.GetItems();
                foreach (var item in list)
                {
                    try
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@fileName, true))
                        {
                            if (item.IsDone)
                            {
                                file.WriteLine(
                                    $"{item.Title}|{item.Deadline}|is_important"); //is important not implemented
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
            return base.ToString();
        }
    }



}