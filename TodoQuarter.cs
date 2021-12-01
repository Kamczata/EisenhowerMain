using System;
using System.Collections.Generic;
using System.Text;

namespace EisenhowerCore { 

    public class TodoQuarter 
    {
        private List<TodoItem> ToDoItems { get; }

        public TodoQuarter(List<TodoItem> listOfItems)
        {
            ToDoItems = listOfItems;
        }

        public void AddItem(string title, DateTime deadline)
        {
            TodoItem newItem = new TodoItem(title,deadline);
            ToDoItems.Add(newItem);
        }

        public void RemoveItem(int index)
        {
            ToDoItems.RemoveAt(index);
        }

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
                allItems += item.ToString() + "\n";
            }

            return allItems;
        }

    }

}