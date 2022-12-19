using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace LR2
{

    public partial class Form1 : Form
    {
        public CarShop GCarShop;
        public static List<string> shopData = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InactiveButtons();
            comboBox1.Items.Add("Mexico Motors");
            comboBox1.Items.Add("Tokyo Auto");
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        GCarShop = new CarShop(0, "Mexico Motors", "м. Мехіко", 0);
                        labelShopName.Text = GCarShop.Property_ShopName.Trim();
                        ActiveButtons();
                        listBoxRefresh();
                        break;
                    }
                case 1:
                    {
                        GCarShop = new CarShop(0, "Tokyo Auto", "м. Токіо",0);
                        labelShopName.Text = GCarShop.Property_ShopName.Trim();
                        ActiveButtons();
                        listBoxRefresh();
                        break;
                    }
                case 2:
                    {
                        GCarShop = Form2.carShop;
                        labelShopName.Text = GCarShop.Property_ShopName.Trim();
                        ActiveButtons();
                        listBoxRefresh();
                        break;
                    }
                default:
                    break;
            }
        }
        private void InactiveButtons()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;

            listBox1.Enabled = false;
            listBox2.Enabled = false;

            checkedListBox1.Enabled = false;
            checkedListBox2.Enabled = false;
        }
        private void ActiveButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;

            listBox1.Enabled = true;
            listBox2.Enabled = true;

            checkedListBox1.Enabled = true;
            checkedListBox2.Enabled = true;
        }
        
        public void listBoxRefresh()
        {
            GCarShop.calculation();
            listBox1.Items.Clear();
            listBox1.Items.Add("Кількість моделей автомобілів:  " + GCarShop.Property_NumberOfCars);
            listBox1.Items.Add("Кількість працівників:  " + GCarShop.Property_NumberOfEmployers);
            listBox1.Items.Add("Назва магазину:  " + GCarShop.Property_ShopName);
            listBox1.Items.Add("Адреса магазину:  " + GCarShop.Property_StoreAddres);
            listBox1.Items.Add("Середній дохід магазину на місяць:  " + GCarShop.Property_AvgIncome);
            listBox1.Items.Add("Загальна заробітна плата співробітників:  " + GCarShop.Property_Salary);
            listBox1.Items.Add("Загальні витрати на кіпівлю товарів:  " + GCarShop.Property_Expenses);
            listBox1.Items.Add("Кількість найменувань товарів:  " + GCarShop.Property_ProductsQuantity);
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            GCarShop.sellCar((string)checkedListBox2.SelectedItem, textBox4.Text);

            shopData.Remove((string)checkedListBox2.SelectedItem);

            checkedListBox2.SetItemChecked(checkedListBox2.SelectedIndex, false);
            textBox4.Clear();
            listBoxRefresh();

            GCarShop.writeCarsToFile(shopData);
            readCars();
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            GCarShop.writeWorkersToFile();
            readWorkers();
        }

        private void button5_Click(object sender, EventArgs e) 
        {      
            GCarShop.addABatchOfCar((string) checkedListBox1.SelectedItem, textBox2.Text, Int32.Parse(textBox3.Text));

            for (int i = 0; i < Int32.Parse(textBox3.Text); i++)
            {
                shopData.Add((string)checkedListBox1.SelectedItem);
                Console.WriteLine(shopData.Count);
            }

            checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, false);
            textBox2.Clear();
            textBox3.Clear();
            listBoxRefresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GCarShop.Add_Worker(new Worker());
            listBoxRefresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GCarShop.Delete_Worker(textBox1.Text);
            listBoxRefresh();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ActiveButtons();
            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.ShowDialog();
            GCarShop = Form2.carShop;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i = GCarShop.CompareTo(new CarShop(12, "Mexico Motors", "м. Мехіко", 150));

            if (i == 0) MessageBox.Show(GCarShop.Property_ShopName + " приносить менші доходи.");
            if (i == 1) MessageBox.Show(GCarShop.Property_ShopName + " приносить більші доходи.");
            if (i == 2) MessageBox.Show("Магазини приносять однакові доходи.");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GCarShop.Delete_AllWorkers();
            listBoxRefresh();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GCarShop.writeCarsToFile(shopData);
            readCars();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            writeShopDataToFileWithout();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            readShopDataFromFile();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            writeShopDataToFile();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void readWorkers()
        {
            string[] strs = GCarShop.readWorkersFromFile();
            listBox2.Items.Clear();
            listBox2.Items.Add("Список робітників");
            foreach (string str in strs)
            {
                listBox2.Items.Add(str);
            }
        }

        private void readCars()
        {
            string[] strs = GCarShop.readCarsFromFile();
            listBox2.Items.Clear();
            listBox2.Items.Add("Список автомобілів");
            foreach (string str in strs)
            {
                listBox2.Items.Add(str);
            }
        }

        private void writeShopDataToFileWithout()
        {
            try
            {
                StreamWriter sw = new StreamWriter(File.Create("ShopData/ShopData.txt"));

                //sw.WriteLine("Кількість моделей автомобілів:  " + GCarShop.Property_NumberOfCars);
                //sw.WriteLine("Кількість працівників:  " + GCarShop.Property_NumberOfEmployers);
                sw.WriteLine("Назва магазину:  " + GCarShop.Property_ShopName);
                sw.WriteLine("Адреса магазину:  " + GCarShop.Property_StoreAddres);
                sw.WriteLine("Середній дохід магазину на місяць:  " + GCarShop.Property_AvgIncome);
                sw.WriteLine("Загальна заробітна плата співробітників:  " + GCarShop.Property_Salary);
                sw.WriteLine("Загальні витрати на кіпівлю товарів:  " + GCarShop.Property_Expenses);
                sw.WriteLine("Кількість найменувань товарів:  " + GCarShop.Property_ProductsQuantity);

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private void writeShopDataToFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter(File.Create("ShopData/ShopData.txt"));

                sw.WriteLine("Кількість моделей автомобілів:  " + GCarShop.Property_NumberOfCars);
                sw.WriteLine("Кількість працівників:  " + GCarShop.Property_NumberOfEmployers);
                sw.WriteLine("Назва магазину:  " + GCarShop.Property_ShopName);
                sw.WriteLine("Адреса магазину:  " + GCarShop.Property_StoreAddres);
                sw.WriteLine("Середній дохід магазину на місяць:  " + GCarShop.Property_AvgIncome);
                sw.WriteLine("Загальна заробітна плата співробітників:  " + GCarShop.Property_Salary);
                sw.WriteLine("Загальні витрати на кіпівлю товарів:  " + GCarShop.Property_Expenses);
                sw.WriteLine("Кількість найменувань товарів:  " + GCarShop.Property_ProductsQuantity);

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void readShopDataFromFile()
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            String line;
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                line = sr.ReadLine();
                while (line != null)
                {
                    listBox1.Items.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }

    }


    public class CarShop : IComparable
    {
        public List<Car> CarList;
        private Dictionary<string, Worker> workers = new Dictionary<string, Worker>();

        public Dictionary<string, Worker> Dictionary_Workers { get => workers; set => workers = value; }

        private int NumberOfCars;              //Кількість автомобілів
        private int NumberOfEmployers;         //Кількість співробітників
        private string ShopName = "";           //Назва магазину
        private string StoreAddres;             //Адреса магазину
        private int AvgIncome;                 //Середній дохід магазину на місяць 
        private int Salary;                    //Загальна заробітна плата співробітників
        private int Expenses;                  //Загальні витрати на кіпівлю товарів
        private int ProductsQuantity;          //Кількість найменувань товарів

        private int cost = 10000;

        //Properties 
        public int Property_NumberOfCars { get { return NumberOfCars; } set { NumberOfCars = value; } }
        public int Property_NumberOfEmployers { get { return workers.Count(); } set { NumberOfEmployers = value; } }
        public string Property_ShopName { get { return ShopName; } set { ShopName = value; } }
        public string Property_StoreAddres { get { return StoreAddres; } set { StoreAddres = value; } }
        public int Property_AvgIncome { get { return AvgIncome; } set { AvgIncome = value; } }
        public int Property_Salary { get { return Property_NumberOfEmployers * 1000; } set { Salary = value; } }
        public int Property_Expenses { get { return ProductsQuantity * cost; } set { Expenses = value; } }
        public int Property_ProductsQuantity { get { return ProductsQuantity; } set { ProductsQuantity = value; } }

        public CarShop() {}

        public CarShop(int _NumberOfCars, string _ShopName, string _StoreAddres,
            int _ProductsQuantity)
        {
            NumberOfCars = _NumberOfCars;
            NumberOfEmployers = Property_NumberOfEmployers;
            ShopName = _ShopName;
            StoreAddres = _StoreAddres;
            ProductsQuantity =_ProductsQuantity;
            Expenses = Property_Expenses;
            Salary = Property_Salary;
            AvgIncome = 100000;
        }

        public void calculation()
        {
            Expenses = Property_Expenses;
            Salary = Property_Salary;
            AvgIncome = Property_AvgIncome;
        }

        public int CompareTo(Object obj)
        {
            CarShop cs = (CarShop) obj;
            if(this.Property_AvgIncome > cs.Property_AvgIncome) return 1;
            if (this.Property_AvgIncome == cs.Property_AvgIncome) return 2;
            else return 0;
        }

/*        public static CarShop operator ++ (CarShop carShop)
        {
            carShop.NumberOfEmployers = (uint)carShop.NumberOfEmployers + 1;
            return carShop;
        }

        public static CarShop operator -- (CarShop carShop)
        {
            carShop.NumberOfEmployers = (uint)carShop.NumberOfEmployers - 1;
            return carShop;
        }
*/
        public void Add_Worker(Worker worker)
        {
            String str = "000000000" + Property_NumberOfEmployers.ToString();
            if(!workers.ContainsKey("000000000" + Property_NumberOfEmployers.ToString()))
                workers.Add(str, worker);
        }
        public void Delete_Worker(string tax)
        {
            if (workers.ContainsKey(tax))
            {
                workers.Remove(tax);
            }
        }
        public void Delete_AllWorkers()
        {
            workers.Clear();
        }

        public void addABatchOfCar(string _brand, string name, int count)
        {
            cost = selectCost(_brand);
            ProductsQuantity += count;
            AvgIncome -= count * cost;
            NumberOfCars++;
        }

        private int selectCost(String _brand)
        {
            char[] arr = _brand.ToCharArray();
            if (arr[0].Equals((char)Brand.Ford)) cost = 10000;
            else if (arr[0].Equals((char)Brand.Kia)) cost = 8000;
            else if (arr[0].Equals((char)Brand.BMW)) cost = 15000;
            else if (arr[0].Equals((char)Brand.Mazda)) cost = 7000;
            else if (arr[0].Equals((char)Brand.Opel)) cost = 5000;
            else if (arr[0].Equals((char)Brand.Renault)) cost = 6000;
            else if (arr[0].Equals((char)Brand.Porshe)) cost = 20000;
            else if (arr[1].Equals((char)Brand.Mersedes)) cost = 14000;
            else if (arr[0].Equals((char)Brand.Toyota)) cost = 11000;
            else if (arr[0].Equals((char)Brand.Suzuki)) cost = 10000;

            return cost;
        }

        public void sellCar(string car, string name)
        {
            cost = selectCost(car);
            Property_AvgIncome += cost + (int)(cost * 0.25);
            ProductsQuantity--;
            if (ProductsQuantity == 0) NumberOfCars = 0;
        }

        public void writeWorkersToFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter(File.Create("ShopData/Workers.txt"));
                foreach (String key in workers.Keys)
                {
                    sw.WriteLine("Код: " + key + "; Им'я: " + workers[key].Property_Name + ";");
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public string[] readWorkersFromFile()
        {
            string[] lines = new string[workers.Count];
            String line;
            try
            {
                StreamReader sr = new StreamReader("ShopData/Workers.txt");
                line = sr.ReadLine();
                int i = 0;
                while (line != null)
                {
                    lines[i] = line;
                    i++;
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return lines;
        }

        public void writeCarsToFile(List<string> shopData)
        {
            try
            {
                StreamWriter sw = new StreamWriter(File.Create("ShopData/Cars.txt"));
                foreach (String key in shopData)
                {
                    sw.WriteLine(key);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public string[] readCarsFromFile()
        {
            string[] lines = new string[Form1.shopData.Count];
            String line;
            try
            {
                StreamReader sr = new StreamReader("ShopData/Cars.txt");
                line = sr.ReadLine();
                int i = 0;
                while (line != null)
                {
                    lines[i] = line;
                    i++;
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return lines;
        }
    }

    public enum Brand { Ford = 'F', Kia = 'K', BMW ='B', Mazda='M', Opel='O', Renault='R', Porshe='P', Mersedes='E', Toyota='E', Suzuki='S' };
    public enum ControlType { Mechanics, MACHINE, VARIABLE };
    public enum FuelType { GASOLINE, DIESEL_FUEL, ELECTRIC_ENERGY };

    public class Car
    {
        private Brand CarBrand;
        private string Name;
        private ControlType Control;
        private FuelType Fuel;
        private double Engine_Capacity;
        private double Fuel_Consumption;
        private int Purchase_Price;
        private int Selling_Price;
    }

    enum Education { NO_EDUCATION, TECHNICAL, HUMANITARIAN, FINANCIAL, LEGAL};

    enum Position { DIRECTOR, HR, SENIOR_SALESMAN, SALESPERSON, GUARDIAN};


    public class Worker
    {
        private string Name;
        private string Last_Name;
        private Education Worker_Education;
        private string Birthday;
        private string Tax_Number;
        private Position Worker_Position;
        private int start = 8_000;
        private int end = 60_000;
        private int Salary;

        public string Property_Tax_Number { get => Tax_Number; set => Tax_Number = value; }
        public string Property_Name { get => Name; set => Name = value; }

        public Worker()
        {
            string[] arr1 = { "Karl", "Oleg", "Denis", "Evgeniy", "Andrey", "Lucifer", "Sam" };

            Name = arr1[new Random().Next(0, 6)];
        }
    }
}
