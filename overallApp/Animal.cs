using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overallApp
{
    class Animal // parent class
    {

        public int ID;
        public double AmtOfWater;
        public double DailyCost;
        public double Weight;
        public int Age;
  
        public Animal() { }

        public Animal(int id, double amtW, double dailyC, double weight, int age)
        {
            this.ID = id;
            this.AmtOfWater = amtW;
            this.DailyCost = dailyC;
            this.Weight = weight;
            this.Age = age;
        }
        //virtual functions
        public virtual double Profitability() { return 1; }//calcualtes profitability of all animals
                                                           
        public virtual void getprofit() { }//calcualtes profitability of individual animal , for sorting 
                                                          
    }
}
