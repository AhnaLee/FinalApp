using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overallApp
{
    class Sheep : Animal
    {

        public string color;

        public double amtOfWool;

        public Sheep() { }
        public Sheep(int id, double amtW, double dailyC, double weight, int age, string color, double amtWool)
                    : base(id, amtW, dailyC, weight, age)
        {
            this.color = color;
            this.amtOfWool = amtWool;
        }
        public override double Profitability()
        {
            double water = 0, dailycost = 0, wool = 0, tax = 0, income = 0, weight = 0;
            foreach (KeyValuePair<int, Sheep> sheep in HashTable.sheep)
            {
                water = water + sheep.Value.AmtOfWater;
                dailycost = dailycost + sheep.Value.DailyCost;
                wool = wool + sheep.Value.amtOfWool;
                weight = weight + sheep.Value.Weight;
            }
            tax = (weight * Prices.govtTax);
            water = water * Prices.waterPrice;
            income = wool * Prices.sheepWoolPrice;
            return income = income - (tax + dailycost + water);
        }
        public override void getprofit()
        {
            double water = 0, dailycost = 0, wool = 0, tax = 0, income = 0, weight = 0;
            foreach (KeyValuePair<int, Sheep> sheep in HashTable.sheep)
            {
                water = sheep.Value.AmtOfWater;
                water = water * Prices.waterPrice;
                dailycost = sheep.Value.DailyCost;
                wool = sheep.Value.amtOfWool;
                weight = weight + sheep.Value.Weight;
                tax = (weight * Prices.govtTax);
                income = (wool * Prices.sheepWoolPrice) - (tax + dailycost + water);
                HashTable.sort.Add(sheep.Value.ID, income);
            }
        }
    }
}
