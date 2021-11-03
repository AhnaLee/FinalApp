using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overallApp
{
    class HashTable
    {
        public static Dictionary<int, Cows> cow = new Dictionary<int, Cows>();
        public static Dictionary<int, JersyCow> jersycow = new Dictionary<int, JersyCow>();
        public static Dictionary<int, Goats> goat = new Dictionary<int, Goats>();
        public static Dictionary<int, Sheep> sheep = new Dictionary<int, Sheep>();
        public static Dictionary<int, Dogs> dogs = new Dictionary<int, Dogs>();
        public static Dictionary<int, double> sort = new Dictionary<int, double>();
    }
}
