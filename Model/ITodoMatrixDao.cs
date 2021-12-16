using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerCore
{
    public interface ITodoMatrixDao
    {
        public void Add(TodoMatrix matrix);

        public TodoMatrix Get(int id);

        List<TodoMatrix> GetAllTitles();

        public void ArchiveDoneItems(int matrixId);
    }
}
