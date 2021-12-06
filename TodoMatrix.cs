using System;
using System.IO;

namespace EisenhowerCore
{
    public class TodoMatrix {

        private Dictionary<string, TodoQuarter> TodoQuarters;

        public TodoMatrix()
        {
            TodoQuarters = new Dictionary<string, TodoQuarter>;
            List<TodoItem> todoItemts = new();
            TodoQuarters.Add("IU", todoItemts);
            TodoQuarters.Add("IN", todoItemts);
            TodoQuarters.Add("NU", todoItemts);
            TodoQuarters.Add("NN", todoItemts);
        }

        public Dictionary<string, TodoQuarter> GetQuarters() => TodoQuarters;


        public TodoQuarter GetQuarter(String status)
        {
            return TodoQuarters[status];
        }
  
    
        public void AddItem(String title, DateTime deadline, bool isImportant)
        {
            bool isUrgent = IsUrgent(deadline);
            if (isUrgent && isImportant)
            {
                TodoQuarters["IU"].ToDoItems.AddItem(title, deadline);
            }
            else if(isUrgent && !isImportant)
            {
                TodoQuarters["NU"].ToDoItems.AddItem(title, deadline);
            }
            else if(!isUrgent && isImportant)
            {
                TodoQuarters["IN"].ToDoItems.AddItem(title, deadline);
            }
            else if(!isUrgent && !isImportant)
            {
                TodoQuarters["NN"].ToDoItems.AddItem(title, deadline);
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
                bool isImportant = new();
                DateTime deadline = new DateTime(DateTime.Today.Year, Int32.Parse(date[1]), Int32.Parse(date[0]));
                if (item[2]="")
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
    }

}