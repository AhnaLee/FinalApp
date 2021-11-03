using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace overallApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            createHashTable();
            PriceTable();
        }
        public void createHashTable()
        {
            try
            {   
                //Connection
                OleDbConnection OleConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ahnal\source\repos\overallApp\overallApp\FarmInfomation.accdb");
                OleConn.Open(); // open the Access DB

                String c1 = "SELECT * FROM Cows"; 
                String c2 = "SELECT * FROM Goats"; 
                String c3 = "SELECT * FROM Sheep";  
                String c4 = "SELECT * FROM Dogs";

                //Create commands for each table
                OleDbCommand OleCmd1 = new OleDbCommand(c1, OleConn);
                OleDbCommand OleCmd2 = new OleDbCommand(c2, OleConn);
                OleDbCommand OleCmd3 = new OleDbCommand(c3, OleConn);
                OleDbCommand OleCmd4 = new OleDbCommand(c4, OleConn);
               

                //Read Cow's table
                using (OleDbDataReader value = OleCmd1.ExecuteReader())
                    while (value.Read())
                    {
                        if (value[7].ToString() != "True")
                        {
                            HashTable.cow.Add(int.Parse(value[0].ToString()),
                                new Cows(int.Parse(value[0].ToString()),
                                double.Parse(value[1].ToString()),
                                double.Parse(value[2].ToString()),
                                double.Parse(value[3].ToString()),
                                int.Parse(value[4].ToString()),
                                value[5].ToString(),
                                double.Parse(value[6].ToString())));
                        }
                        else
                        {
                            HashTable.jersycow.Add(int.Parse(value[0].ToString()),
                                new JersyCow(int.Parse(value[0].ToString()),
                                double.Parse(value[1].ToString()),
                                double.Parse(value[2].ToString()),
                                double.Parse(value[3].ToString()),
                                int.Parse(value[4].ToString()),
                                value[5].ToString(),
                                double.Parse(value[6].ToString()),
                                bool.Parse(value[7].ToString())));
                        }
                    }
                //Read Goat table
                using (OleDbDataReader value = OleCmd2.ExecuteReader())
                    while (value.Read())
                    {
                        HashTable.goat.Add(int.Parse(value[0].ToString()),
                            new Goats(int.Parse(value[0].ToString()),
                            double.Parse(value[1].ToString()),
                            double.Parse(value[2].ToString()),
                            double.Parse(value[3].ToString()),
                            int.Parse(value[4].ToString()),
                            value[5].ToString(),
                            double.Parse(value[6].ToString())));
                    }
                //Read Sheep table
                using (OleDbDataReader value = OleCmd3.ExecuteReader())
                    while (value.Read())
                    {
                        HashTable.sheep.Add(int.Parse(value[0].ToString()),
                            new Sheep(int.Parse(value[0].ToString()),
                            double.Parse(value[1].ToString()),
                            double.Parse(value[2].ToString()),
                            double.Parse(value[3].ToString()),
                            int.Parse(value[4].ToString()),
                            value[5].ToString(),
                            double.Parse(value[6].ToString())));
                    }
                //Read Dogs table
                using (OleDbDataReader value = OleCmd4.ExecuteReader())
                    while (value.Read())
                    {
                        HashTable.dogs.Add(int.Parse(value[0].ToString()),
                            new Dogs(int.Parse(value[0].ToString()),
                            double.Parse(value[1].ToString()),
                            double.Parse(value[2].ToString()),
                            int.Parse(value[3].ToString()),
                            value[4].ToString(),
                            double.Parse(value[5].ToString())));
                    }
                OleConn.Close();// close the connection
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void PriceTable()
        {
            OleDbConnection OleConn2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ahnal\source\repos\overallApp\overallApp\FarmInfomation.accdb");
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Prices", OleConn2);
            OleDbDataAdapter dataAdp = new OleDbDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            dataAdp.Fill(dataTable);

            foreach (DataRow dataraw in dataTable.Rows)
            {
                switch (dataraw["Commodity"].ToString())
                {
                    case "Goat milk price":
                        {
                            Prices.goatMilkPrice = double.Parse(dataraw["Price"].ToString());
                            break;
                        }
                    case "Sheep Wool price":
                        {
                            Prices.sheepWoolPrice = double.Parse(dataraw["Price"].ToString());
                            break;
                        }
                    case "Water Price":
                        {
                            Prices.waterPrice = double.Parse(dataraw["Price"].ToString());
                            break;
                        }
                    case "Government tax per kg":
                        {
                            Prices.govtTax = double.Parse(dataraw["Price"].ToString());
                            break;
                        }
                    case "Jersycow tax":
                        {
                            Prices.jersyCowTax = double.Parse(dataraw["Price"].ToString());
                            break;
                        }
                    case "Cow milk price":
                        {
                            Prices.cowMilkPrice = double.Parse(dataraw["Price"].ToString());
                            break;
                        }
                }
            }
            OleConn2.Close();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public struct array
        {
            public int id;
            public double profit;
        }
        private void button20_Click(object sender, EventArgs e)//Report 8
        {
            HashTable.sort.Clear();
            Cows cow = new Cows();
            JersyCow jc = new JersyCow();
            Goats goat = new Goats();
            Sheep sheep = new Sheep();
            //fill sort table with getProfit function
            cow.getprofit();
            jc.getprofit();
            goat.getprofit();
            sheep.getprofit();

            double valTemp;
            int keyTemp;
            int count = 0;
            array[] sort = new array[HashTable.sort.Count];
            foreach (KeyValuePair<int, double> myFarm in HashTable.sort)
            {
                sort[count].id = myFarm.Key;
                sort[count].profit = myFarm.Value;
                count++;
            }
            for (int i = 0; i < HashTable.sort.Count; i++) //bubble sort
            {
                for (int j = 0; j < HashTable.sort.Count - i - 1; j++)
                {
                    if (sort[i].profit > sort[j].profit)
                    {
                        keyTemp = sort[i].id;
                        valTemp = sort[i].profit;
                        sort[i].id = sort[j].id;
                        sort[i].profit = sort[j].profit;
                        sort[j].id = keyTemp;
                        sort[j].profit = valTemp;
                       
                    }
                }
            }
            sortbox.Text = sortbox.Text + "Animal ID" + "                    " + "Profitability" + "\r\n";
            sortbox.Text = sortbox.Text + "----------------------------------------------------" + "\r\n";
            FileStream file = new FileStream("sorted.txt", FileMode.Create);
            StreamWriter write = new StreamWriter(file);
            for (int i = 0; i < HashTable.sort.Count; i++)//save that data into text file
            {
                sortbox.Text = sortbox.Text + sort[i].id.ToString() + "                                 " + sort[i].profit.ToString() + "\r\n";

                write.WriteLine(sort[i].id);
            }
            write.Close();
        }
        private void label21_Click(object sender, EventArgs e)
        {
        }
        private void label27_Click(object sender, EventArgs e)
        {
        }
        private void Info_btn_Click(object sender, EventArgs e) //Report 1 button 
        {
            MessageBox.Show("Please Enter an animal ID");
        }
        private void button14_Click(object sender, EventArgs e) //Report 1 search button
        {
            int count = 0;
            //search for the information about animal 
            foreach (KeyValuePair<int, Cows> cows in HashTable.cow) //if it matches to the user input 
            {
                if (cows.Value.ID.ToString() == enterID.Text)
                {
                    count++;
                    id.Text = cows.Value.ID.ToString();
                    amtofwater.Text = cows.Value.AmtOfWater.ToString();
                    daily1.Text = cows.Value.DailyCost.ToString();
                    colour1.Text = cows.Value.color.ToString();
                    weight1.Text = cows.Value.Weight.ToString();
                    age1.Text = cows.Value.Age.ToString();
                    amtofmilk.Text = cows.Value.amtOfMilk.ToString();
                    isJersey.Text = "";
                    break;
                }
            }

            foreach (KeyValuePair<int, JersyCow> jc in HashTable.jersycow)
            {
                if (jc.Value.ID.ToString() == enterID.Text)
                {
                    count++;
                    id.Text = jc.Value.ID.ToString();
                    amtofwater.Text = jc.Value.AmtOfWater.ToString();
                    daily1.Text = jc.Value.DailyCost.ToString();
                    colour1.Text = jc.Value.color.ToString();
                    weight1.Text = jc.Value.Weight.ToString();
                    age1.Text = jc.Value.Age.ToString();
                    amtofmilk.Text = jc.Value.amtOfMilk.ToString();
                    isJersey.Text = "JerseyCow";
                    break;
                }
            }

            foreach (KeyValuePair<int, Goats> goats in HashTable.goat)
            {
                if (goats.Value.ID.ToString() == enterID.Text)
                {
                    count++;
                    id.Text = goats.Value.ID.ToString();
                    amtofwater.Text = goats.Value.AmtOfWater.ToString();
                    daily1.Text = goats.Value.DailyCost.ToString();
                    colour1.Text = goats.Value.color.ToString();
                    weight1.Text = goats.Value.Weight.ToString();
                    age1.Text = goats.Value.Age.ToString();
                    goatMilk.Text = goats.Value.amtOfMilk.ToString();
                    isJersey.Text = "";
                    break;
                }
            }

            foreach (KeyValuePair<int, Sheep> sh in HashTable.sheep)
            {
                if (sh.Value.ID.ToString() == enterID.Text)
                {
                    count++;
                    id.Text = sh.Value.ID.ToString();
                    amtofwater.Text = sh.Value.AmtOfWater.ToString();
                    daily1.Text = sh.Value.DailyCost.ToString();
                    colour1.Text = sh.Value.color.ToString();
                    weight1.Text = sh.Value.Weight.ToString();
                    age1.Text = sh.Value.Age.ToString();
                    amtofwool.Text = sh.Value.amtOfWool.ToString();
                    isJersey.Text = "";
                    break;
                }
            }

            foreach (KeyValuePair<int, Dogs> dog in HashTable.dogs)
            {
                if (dog.Value.ID.ToString() == enterID.Text)
                {
                    count++;
                    id.Text = dog.Value.ID.ToString();
                    amtofwater.Text = dog.Value.AmtOfWater.ToString();
                    daily1.Text = dog.Value.DailyCost.ToString();
                    colour1.Text = dog.Value.color.ToString();
                    weight1.Text = dog.Value.Weight.ToString();
                    age1.Text = dog.Value.Age.ToString();
                    isJersey.Text = "";
                    break;
                }
            }
            if (count == 0) // if not show error meesage and empty the textboxes
            {
                MessageBox.Show("Data not found, Please enter valid ID");
                enterID.Text = "";
                id.Text = "";
                amtofwater.Text = "";
                daily1.Text = "";
                colour1.Text = "";
                weight1.Text = "";
                age1.Text = "";
                isJersey.Text = "";
                amtofmilk.Text = "";
                amtofwool.Text = "";
                goatMilk.Text = "";
            }
        }

        private void profit_Btn_Click(object sender, EventArgs e)
        {
            Cows cow = new Cows();
            JersyCow jc = new JersyCow();
            Goats goat = new Goats();
            Sheep sheep = new Sheep();
            // get profit from all different class and put it into textboxes
            cowProf.Text = cow.Profitability().ToString();
            jcProf.Text = jc.Profitability().ToString();
            goatProf.Text = goat.Profitability().ToString();
            sheepProf.Text = sheep.Profitability().ToString();
            totProf.Text = (cow.Profitability() + jc.Profitability() + goat.Profitability() + sheep.Profitability()).ToString();
        }//Report 2

        private void taxGov_btn_Click(object sender, EventArgs e)
        {   
            //get weights from the hashtable to calculate the tax
            double cow = 0, goat = 0, jc = 0, sheep =0;
            foreach (KeyValuePair<int, Goats> goats in HashTable.goat)
            {
                goat = goat + goats.Value.Weight;
            }
            foreach (KeyValuePair<int, JersyCow> jcs in HashTable.jersycow)
            {
                jc = jc + jcs.Value.Weight;
            }
            foreach (KeyValuePair<int, Cows> cows in HashTable.cow)
            {
                cow = cow + cows.Value.Weight;
            }
            foreach (KeyValuePair<int, Sheep> sh in HashTable.sheep)
            {
                sheep = sheep + sh.Value.Weight;
            }
            // calculate the monthly tax and put it into textboxes 
            cowTaxText.Text = ((cow * Prices.govtTax) * 30).ToString();
            jcTaxText.Text = ((jc * Prices.govtTax + Prices.jersyCowTax) * 30).ToString();
            sheepTaxText.Text = ((goat * Prices.govtTax) * 30).ToString();
            shTaxText.Text = ((sheep * Prices.govtTax) * 30).ToString();
            totTax.Text = (double.Parse(cowTaxText.Text) + double.Parse(jcTaxText.Text) + double.Parse(sheepTaxText.Text) + double.Parse(shTaxText.Text)).ToString();
        } //Report 3

        private void amtMilk_btn_Click(object sender, EventArgs e)
        {
            // get amount of milk from the hashtable
            double cow = 0, jc = 0, goat = 0;
            foreach (KeyValuePair<int, Cows> cows in HashTable.cow)
            {
                cow = cow + cows.Value.amtOfMilk;
            }
            foreach (KeyValuePair<int, JersyCow> jcs in HashTable.jersycow)
            {
                jc = jc + jcs.Value.amtOfMilk;
            }
            foreach (KeyValuePair<int, Goats> goats in HashTable.goat)
            {
                goat = goat + goats.Value.amtOfMilk;
            }
            // put it into textboxes
            cowMilk.Text = cow.ToString();
            jcMilk.Text = jc.ToString();
            goatMilk2.Text = goat.ToString();
            totMilk.Text = (cow + jc + goat).ToString();
        } //Report 4

        private void avgAge_btn_Click(object sender, EventArgs e)
        {
            int count = 0;
            int cow = 0, jc = 0, goat = 0, sheep = 0;
            foreach (KeyValuePair<int, Cows> cows in HashTable.cow)
            {
                cow = cow + cows.Value.Age;
                count++;
            }
            foreach (KeyValuePair<int, JersyCow> jcs in HashTable.jersycow)
            {
                jc = jc + jcs.Value.Age;
                count++;
            }
            foreach (KeyValuePair<int, Goats> goats in HashTable.goat)
            {
                goat = goat + goats.Value.Age;
                count++;
            }
            foreach (KeyValuePair<int, Sheep> sh in HashTable.sheep)
            {
                sheep = sheep + sh.Value.Age;
                count++;
            }
            cowAge.Text = cow.ToString();
            jcAge.Text = jc.ToString();
            goatAge.Text = goat.ToString();
            sheepAge.Text = sheep.ToString();
            avgAge.Text = ((cow + jc + goat + sheep) / count).ToString(); // calculate the average age of counted animal
        } //Report 5

        private void proVS_btn_Click(object sender, EventArgs e)
        {
            Cows cow = new Cows();
            Goats goat = new Goats();
            Sheep sheep = new Sheep();

            cowProf2.Text = cow.Profitability().ToString();
            goatProf2.Text = goat.Profitability().ToString();
            avgCowGoat.Text = ((cow.Profitability() + goat.Profitability()) / 2).ToString();
            sheepProf2.Text = sheep.Profitability().ToString();
        }//Report 6

        private void dogRatio_btn_Click(object sender, EventArgs e)
        {
            double cow = 0, jc = 0, goat = 0, sheep = 0, dog = 0;
            foreach (KeyValuePair<int, Cows> cows in HashTable.cow)
            {
                cow = cow + cows.Value.DailyCost;
            }
            foreach (KeyValuePair<int, JersyCow> jcs in HashTable.jersycow)
            {
                jc = jc + jcs.Value.DailyCost;
            }
            foreach (KeyValuePair<int, Goats> goats in HashTable.goat)
            {
                goat = goat + goats.Value.DailyCost;
            }
            foreach (KeyValuePair<int, Sheep> sh in HashTable.sheep)
            {
                sheep = sheep + sh.Value.DailyCost;
            }
            foreach (KeyValuePair<int, Dogs> dogs in HashTable.dogs)
            {
                dog = dog + dogs.Value.DailyCost;
            }
            //print the daily cost of animals
            cowCost.Text = cow.ToString();
            jcCost.Text = jc.ToString();
            gCost.Text = goat.ToString();
            sCost.Text = sheep.ToString();
            dCost.Text = dog.ToString();
            totCost.Text = (cow + jc + goat + sheep + dog).ToString();
            //print dog's cost as a ratio and compare
            double ratio = 0;
            ratio = (dog / double.Parse(totCost.Text)) * 100;
            dogVStot.Text = ratio.ToString("0.##");
        }//Report 7

        private void colourRatio_btn_Click(object sender, EventArgs e)
        {   //find animals with red colour in hashtable
            double cow = 0, jc = 0, goat = 0, sheep = 0, dog = 0, total = 0;
            foreach (KeyValuePair<int, Cows> cows in HashTable.cow)
            {
                if (cows.Value.color == "Red")
                {
                    cow++;
                }
                total++;
            }
            foreach (KeyValuePair<int, JersyCow> cows in HashTable.jersycow)
            {
                if (cows.Value.color == "Red")
                {
                    cow++;
                }
                total++;
            }
            foreach (KeyValuePair<int, Goats> goats in HashTable.goat)
            {
                if (goats.Value.color == "Red")
                {
                    goat++;
                }
                total++;
            }
            foreach (KeyValuePair<int, Sheep> sh in HashTable.sheep)
            {
                if (sh.Value.color == "Red")
                {
                    sheep++;
                }
                total++;
            }
            foreach (KeyValuePair<int, Dogs> dogs in HashTable.dogs)
            {
                if (dogs.Value.color == "Red")
                {
                    dog++;
                }
                total++;
            }
            //print the total number of animal and red coloured animal
            cowColour.Text = cow.ToString();
            jcColour.Text = jc.ToString();
            goatColour.Text = goat.ToString();
            sheepColour.Text = sheep.ToString();
            dogColour.Text = dog.ToString();
            totColour.Text = total.ToString();
            redAnimal.Text = (cow + jc + goat + sheep + dog).ToString();
        }//Report 9

        private void jcTax_btn_Click(object sender, EventArgs e)
        {
            double milk = 0, income = 0, tax = 0;
            foreach (KeyValuePair<int, JersyCow> jcs in HashTable.jersycow)
            {
                milk = milk + jcs.Value.amtOfMilk;
            }
            income = milk * Prices.cowMilkPrice;
            tax = (income * (Prices.govtTax + Prices.jersyCowTax));
            totMilkpd.Text = milk.ToString();
            totincompd.Text = income.ToString();
            totTaxpd.Text = tax.ToString();
            totTaxpy.Text = (tax * 365).ToString();
        }// Report10

        private void report11_TextChanged(object sender, EventArgs e)
        {

        }
        private void year_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please Enter age of the animal");
        } //Report 11
        private void button13_Click(object sender, EventArgs e)//search threshold Report 11
        {
            try
            {
                if (int.Parse(enterYear.Text) > 0 || enterYear.Text == "")
                {
                    int cow = 0, jc = 0, goat = 0, sheep = 0, dog = 0, total = 0;
                    foreach (KeyValuePair<int, Cows> cows in HashTable.cow)
                    {
                        if (cows.Value.Age > int.Parse(enterYear.Text))
                        {
                            cow++;
                        }
                        total++;
                    }
                    foreach (KeyValuePair<int, JersyCow> jcs in HashTable.jersycow)
                    {
                        if (jcs.Value.Age > int.Parse(enterYear.Text))
                        {
                            jc++;
                        }
                        total++;
                    }
                    foreach (KeyValuePair<int, Goats> goats in HashTable.goat)
                    {
                        if (goats.Value.Age > int.Parse(enterYear.Text))
                        {
                            goat++;
                        }
                        total++;
                    }
                    foreach (KeyValuePair<int, Sheep> sh in HashTable.sheep)
                    {
                        if (sh.Value.Age > int.Parse(enterYear.Text))
                        {
                            sheep++;
                        }
                        total++;
                    }
                    foreach (KeyValuePair<int, Dogs> dogs in HashTable.dogs)
                    {
                        if (dogs.Value.Age > int.Parse(enterYear.Text))
                        {
                            dog++;
                        }
                        total++;
                    }
                    cowYear.Text = cow.ToString();
                    jcYear.Text = jc.ToString();
                    goatYear.Text = goat.ToString();
                    sheepYear.Text = sheep.ToString();
                    dogYear.Text = dog.ToString();
                    totYear.Text = (cow + jc + goat + sheep + dog).ToString();
                }
                else
                {
                    MessageBox.Show("Please enter valid threshold.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void proJC_btn_Click(object sender, EventArgs e) //Report 12
        {
            JersyCow jc = new JersyCow();
            jcTotMilk.Text = jc.totMilk().ToString();
            jcTotIncome.Text = (jc.totMilk() * Prices.cowMilkPrice).ToString();
            waterConsume.Text = jc.totWater().ToString();
            jcDaily.Text = jc.daily().ToString();

            double tax = (Prices.jersyCowTax) * jc.totMilk();
            totExpense.Text = ((jc.totWater() * Prices.waterPrice) + (tax) + jc.daily()).ToString();
            jcTotProf.Text = jc.Profitability().ToString();
        }

        
    }
}
