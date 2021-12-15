using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerCore
{
    public class TodoItemDao : ITodoItemDao
    {
        private readonly string _connectionString;

        public TodoItemDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(TodoItem item)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string insertTodoItemSql =
                    @"
INSERT INTO item (title, deadline, is_important, matrix_id)
VALUES (@Title, @Deadline, @IsImportant, @MatrixId);
SELECT SCOPE_IDENTITY();
";

                command.CommandText = insertTodoItemSql;
                command.Parameters.AddWithValue("@Title", item.Title);
                command.Parameters.AddWithValue("@Deadline", item.Deadline);
                command.Parameters.AddWithValue("@IsImportant", Convert.ToByte(item.IsImportant));
                command.Parameters.AddWithValue("@MatrixId", item.MatrixId);


                int TodoItemitemId = Convert.ToInt32(command.ExecuteScalar());
                item.Id = TodoItemitemId;
            }
            catch (SqlException e)
            {
                throw;
            }

        }
            

        public TodoItem Get(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectItemSql = @"
                SELECT title, deadline, is_important, matrix_id
                FROM item
                WHWER id = @Id;
";
                command.Parameters.AddWithValue("@Id", id);
                command.CommandText = selectItemSql;

                using var dataReader = command.ExecuteReader();
                TodoItem newItem = null;

                if (dataReader.Read())
                {
                    string title = (string)dataReader["title"];
                    DateTime deadline = Convert.ToDateTime(dataReader["deadline"]);
                    bool isImportnat = Convert.ToBoolean((byte)dataReader["is_important"]);
                    int matrixId = Convert.ToInt32(dataReader["matrix_id"]);

                    newItem = new TodoItem(id, title, deadline, isImportnat, matrixId);
                }
                return newItem;


            }
            catch(SqlException e)
            {
                throw;
            }
        }

        public List<TodoItem> GetAll(int matrixId)
        {
            throw new NotImplementedException();
        }

        public void Mark(int id)
        {
            throw new NotImplementedException();
        }

        public void Unmark(int id)
        {
            throw new NotImplementedException();
        }
    }
}
