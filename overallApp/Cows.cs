using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overallApp
{
    class Cows : Animal // child class of Animal
    {
        public string color;
        public double amtOfMilk;


        public Cows() { }
        public Cows(int id, double amtW, double dailyC, double weight, int age, string color, double amtM)
                    : base(id, amtW, dailyC, weight, age)
        {
            this.color = color;
            this.amtOfMilk = amtM;
        }

        public override double Profitability()
        {
            double water = 0, dailycost = 0, milk = 0, tax = 0, income = 0, weight = 0;
            foreach (KeyValuePair<int, Cows> cows in HashTable.cow)
            {
                water = water + cows.Value.AmtOfWater;
                dailycost = dailycost + cows.Value.DailyCost;
                milk = milk + cows.Value.amtOfMilk;
                weight = weight + cows.Value.Weight;
            }
            tax = (weight * Prices.govtTax);
            water = water * Prices.waterPrice;
            income = milk * Prices.cowMilkPrice;
            return income = income - (tax + dailycost + water);
        }
        public override void getprofit()
        {
            double water = 0, dailycost = 0, milk = 0, tax = 0, income = 0, weight = 0;
            foreach (KeyValuePair<int, Cows> cows in HashTable.cow)
            {
                water = cows.Value.AmtOfWater;
                water = water * Prices.waterPrice;
                dailycost = cows.Value.DailyCost;
                milk = milk + cows.Value.amtOfMilk;
                weight = weight + cows.Value.Weight;
                tax = (weight * Prices.govtTax);
                income = (milk * Prices.cowMilkPrice) - (tax + dailycost + water);
                HashTable.sort.Add(cows.Value.ID, income);
            }
        }
    }
}

