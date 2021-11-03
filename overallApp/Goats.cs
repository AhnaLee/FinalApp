using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overallApp
{
    class Goats : Animal
    {
        public string color;
        public double amtOfMilk;

        public Goats() { }
        public Goats(int id, double amtW, double dailyC, double weight, int age, string color, double amtM)
                    : base(id, amtW, dailyC, weight, age)
        {
            this.color = color;
            this.amtOfMilk = amtM;
        }

        public override double Profitability()
        {
            double water = 0, dailycost = 0, milk = 0, tax = 0, income = 0, weight = 0;
            foreach (KeyValuePair<int, Goats> goat in HashTable.goat)
            {
                water = water + goat.Value.AmtOfWater;
                dailycost = dailycost + goat.Value.DailyCost;
                milk = milk + goat.Value.amtOfMilk;
                weight = weight + goat.Value.Weight;
            }
            tax = (weight * Prices.govtTax);
            water = water * Prices.waterPrice;
            income = milk * Prices.goatMilkPrice;
            return income = income - (tax + dailycost + water);
        }
        public override void getprofit()
        {
            double water = 0, dailycost = 0, milk = 0, tax = 0, income = 0, weight = 0;
            foreach (KeyValuePair<int, Goats> goat in HashTable.goat)
            {
                water = goat.Value.AmtOfWater;
                water = water * Prices.waterPrice;
                dailycost = goat.Value.DailyCost;
                milk = milk + goat.Value.amtOfMilk;
                weight = weight + goat.Value.Weight;
                tax = (weight * Prices.govtTax);
                income = (milk * Prices.goatMilkPrice) - (tax + dailycost + water);
                HashTable.sort.Add(goat.Value.ID, income);
            }
        }

    }
}
