using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerCore
{
    public interface ITodoMatrixDao
    {
        public void Add();

        public void Update(int id);

        public TodoItem Get(int id);

        List<TodoItem> GetAllTitles();
    }
}
