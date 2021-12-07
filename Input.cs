using System;
using System.Collections.Generic;
using System.Text;

namespace EisenhowerCore
{
    class Input
    {
        public string ChooseOption() => Console.ReadLine();
        

        public ConsoleKeyInfo PressAnyKey() => Console.ReadKey();
        
    }
}
