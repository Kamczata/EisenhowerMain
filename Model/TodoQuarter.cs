using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EisenhowerCore { 

    public class TodoQuarter
    {
        

        private List<TodoItem> ToDoItems { get; }  = new List<TodoItem>();

        public TodoQuarter()
        {
        }

        public void AddItem(int id, string title, DateTime deadline, bool isImportant, int matrixId)
        {
            TodoItem newItem = new TodoItem(id, title, deadline, isImportant, matrixId);
            ToDoItems.Add(newItem);
        }

        public void RemoveItem(int index) => ToDoItems.RemoveAt(index);
    
        // public void ArchiveItems() => ToDoItems.RemoveAll(item => item.IsDone);
        public void ArchiveItems()
        {
            foreach (TodoItem item in ToDoItems)
            {
                if (item.IsDone)
                {
                    ToDoItems.Remove(item);
                }
            }
        }

        public TodoItem GetItem(int index) => ToDoItems[index];

        public List<TodoItem> GetItems() => ToDoItems;

        public override string ToString()
        {
            string allItems = $"";
            foreach (TodoItem item in ToDoItems)
            {
                allItems += item.ToString() + $"{Environment.NewLine}";
            }

            return allItems;
        }

        public int HowManyItems() => ToDoItems.Count();
        

    }

}