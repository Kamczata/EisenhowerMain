using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerCore
{
    public interface ITodoItemDao
    {
        public void Add(TodoItem item);

        public void Mark(int id);

        public void Unmark(int id);

        public TodoItem Get(int id);

        List<TodoItem> GetAll(int matrixId);
    }
}
