using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerCore
{
    public class Manager
    {
        private string ConnectionString => ConfigurationManager.AppSettings["connectionString"];
        private Display _display;
        private Input _input;

        public void Run()
        {
            bool running = true;

            while (running)
            {
                /*_ui.PrintTitle(GetName());
                _ui.PrintOption('l', "Create new Matrix");
                _ui.PrintOption('2', "Display saved Matrix");


                switch (_ui.Choice("laeq"))
                {
                    case 'l':
                        List();
                        break;
                    case 'a':
                        Add();
                        break;
                    case 'e':
                        Edit();
                        break;
                    case 'q':
                        running = false;
                        break;
                }*/
            }

            void Setup()
            {
                string connectionString = ConnectionString;
                /*_authorDao = new AuthorDao(connectionString);
                _bookDao = new BookDao(connectionString);*/
            }
        }
    } 
}
