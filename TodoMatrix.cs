using System;
using System.Collections.Generic;

namespace EisenhowerCore
{
    public class TodoMatrix {

        private Dictionary<QuarterType, TodoQuarter> todoQuarters = new Dictionary<QuarterType, TodoQuarter>();

        public TodoMatrix()
        {
            todoQuarters[QuarterType.URGENT] = new TodoQuarter();
            todoQuarters[QuarterType.NOT_URGENT] = new TodoQuarter();
            todoQuarters[QuarterType.IMPORTANT] = new TodoQuarter();
            todoQuarters[QuarterType.NOT_IMPORTANT] = new TodoQuarter();    
            //todoQuarters[5] = new TodoQuarter();    
        }

        private TodoQuarter GetQuarter(QuarterType quarterType)
        {
            return todoQuarters[quarterType];
        }

       


    }

}