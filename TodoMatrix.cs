using System;
using System.Collections.Generic;

namespace EisenhowerCore
{
    public class TodoMatrix
    {
        TodoQuarter quarter = new TodoQuarter();
        public void SaveItemsToFile(string fileName) //https://www.youtube.com/watch?v=vDpww7HsdnM
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
                            file.WriteLine($"{item.Title}|{item.Deadline}|is_important"); //is important not implemented
                        }
                        else
                        {
                            file.WriteLine($"{item.Title}|{item.Deadline}| ");
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw new ApplicationException("This program made an error: ", ex);
                }
                
            }
            
        }
        
        public void ArchiveItems()
        {
            foreach(TodoQuarter quarter in myDictionary.Value)
            {
                // List<TodoItem> list = quarter.GetItems();
                quarter.GetItems().RemoveAll(item => item.IsDone);
            }
            
        }
    }

}