using System;
using System.Collections.Generic;

namespace EisenhowerCore
{
    public class TodoMatrix {

        private Dictionary<QuarterType, TodoQuarter> TodoQuarters = new Dictionary<QuarterType, TodoQuarter>();

        public TodoMatrix()
        {
            TodoQuarters[QuarterType.IU] = new TodoQuarter();
            TodoQuarters[QuarterType.IN] = new TodoQuarter();
            TodoQuarters[QuarterType.NU] = new TodoQuarter();
            TodoQuarters[QuarterType.NN] = new TodoQuarter();    
            //TodoQuarters[5] = new TodoQuarter();    
        }

        private Dictionary<QuarterType, TodoQuarter> GetQuarters() => TodoQuarters;

        private TodoQuarter GetQuarter(QuarterType status)
        {
            return TodoQuarters[status];
        }
              


    }

}