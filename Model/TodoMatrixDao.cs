using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerCore
{
    public class TodoMatrixDao : ITodoMatrixDao
    {
        private readonly string _connectionString;

        public TodoMatrixDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public TodoItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<TodoItem> GetAllTitles()
        {
            throw new NotImplementedException();
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
