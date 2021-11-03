using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overallApp
{
    class Dogs : Animal
    {
        public string color;

        public Dogs() { }
        public Dogs(int id, double amtW, double weight, int age, string color, double dailyC)
                    : base(id, amtW, dailyC, weight, age)
        {
            this.color = color;
        }
    }
}
