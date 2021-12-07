using System;
using System.Collections.Generic;
using System.Text;

namespace EisenhowerCore
{
    class Display
    {
        private string menu = @" 
SHOW:
1. urgent & important items
2. not urgent & important items
3. urgent & not important items
4. not urgent & not important items
5. all items
6. QUIT
";

        private Dictionary<QuarterType, string> headers = new Dictionary<QuarterType, string>() {
            { QuarterType.IU, "Important & Urgent" },
            { QuarterType.NU, "Not Important & Urgent" },
            { QuarterType.IN, "Important & Not Urgent" },
            { QuarterType.NN, "Not Important & Not Urgent" },
        };
        

        public void PrintMenu()
        {
            Console.WriteLine(this.menu);
        }

        public void DisplayQuarter(TodoQuarter quarter, QuarterType quarterType)
        {
            string header = headers[quarterType];
            Console.WriteLine(header);
        }
    }
}
