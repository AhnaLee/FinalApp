using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overallApp
{
    class JersyCow:Cows
    {

        public bool isJersy;


        public JersyCow() { }
        public JersyCow(int id, double amtW, double dailyC, double weight, int age, string color, double amtM, bool isJersy)
                        : base(id, amtW, dailyC, weight, age, color, amtM)
        {
            this.isJersy = isJersy;
        }
        public double totMilk()//this returns the total milk of all jersy cows
        {
            double milk = 0;
            foreach (KeyValuePair<int, JersyCow> cow in HashTable.jersycow)
            {
                milk = milk + cow.Value.amtOfMilk;
            }
            return milk;
        }
        public double totWater()
        {
            double water = 0;
            foreach (KeyValuePair<int, JersyCow> cow in HashTable.jersycow)
            {
                water = water + cow.Value.AmtOfWater;
            }
            return water;
        }
        public double daily()
        {
            double dailyC = 0;
            foreach (KeyValuePair<int, JersyCow> cow in HashTable.jersycow)
            {
                dailyC = dailyC + cow.Value.DailyCost;
            }
            return dailyC;
        }

        public override double Profitability()
        {
            double water = 0, dailycost = 0, milk = 0, tax = 0, income = 0, weight = 0;
            foreach (KeyValuePair<int, JersyCow> jc in HashTable.jersycow)
            {
                water = water + jc.Value.AmtOfWater;
                dailycost = dailycost + jc.Value.DailyCost;
                milk = milk + jc.Value.amtOfMilk;
                weight = weight + jc.Value.Weight;
            }
            tax = (weight * (Prices.jersyCowTax));
            water = water * Prices.waterPrice;
            income = milk * Prices.cowMilkPrice;
            return income = income - (tax + dailycost + water);
        }
        public override void getprofit()
        {
            double water = 0, dailycost = 0, milk = 0, tax = 0, income = 0, weight = 0;
            foreach (KeyValuePair<int, JersyCow> jc in HashTable.jersycow)
            {
                water = jc.Value.AmtOfWater;
                water = water * Prices.waterPrice;
                dailycost = jc.Value.DailyCost;
                milk = milk + jc.Value.amtOfMilk;
                weight = weight + jc.Value.Weight;
                tax = (weight * (Prices.jersyCowTax));
                income = (milk * Prices.cowMilkPrice) - (tax + dailycost + water);
                HashTable.sort.Add(jc.Value.ID, income);
            }
        }
    }
}
