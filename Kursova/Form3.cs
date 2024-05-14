using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova
{
    public partial class Form3 : Form
    {
        class Product
        {
            public string Name { get; set; }
            public string Unit { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public DateTime LastReplenishmentDate { get; set; }

            public Product(string name, string unit, decimal unitPrice, int quantity, DateTime lastReplenishmentDate)
            {
                Name = name;
                Unit = unit;
                UnitPrice = unitPrice;
                Quantity = quantity;
                LastReplenishmentDate = lastReplenishmentDate;
            }
        }

        class Inventory
        {
            public List<Product> products;

            public Inventory()
            {
                products = new List<Product>();
            }

            public void AddProduct(Product product)
            {
                products.Add(product);
            }
            public string GoodsString(Product product)
            {
                return ($"Найменування: {product.Name}\nОдиниці виміру: {product.Unit}\nЦіна одиниці: {product.UnitPrice}\nКількість: {product.Quantity}\nДата останнього завезення: {product.LastReplenishmentDate}");
            }
            public string mes(List<Product> products)
            {
                string temp = "";
                int i = 1;
                foreach (var product in products)
                {
                    temp += $"{i}) {GoodsString(product)} \n \n";
                }
                return temp;
            }
            public void ReplenishProduct(string productName, int quantity)
            {
                Product product = products.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

                if (product != null)
                {
                    // Якщо товар знайдено, збільшити його кількість та оновити дату останнього завезення
                    product.Quantity = quantity;
                    product.LastReplenishmentDate = DateTime.Now.AddMinutes(-5);
                }
                else
                {
                    // Якщо товар не знайдено, додати новий товар
                    MessageBox.Show($"Товар {productName} не знайдено. Додаємо новий товар.");
                    string unit = Interaction.InputBox("Одиниці виміру:", "Введення даних");
                    string price = Interaction.InputBox("Ціна одиниці:", "Введення даних");
                    decimal unitPrice = Convert.ToDecimal(price);
                    DateTime lastReplenishmentDate = Convert.ToDateTime(Console.ReadLine());

                    Product newProduct = new Product(productName, unit, unitPrice, quantity, lastReplenishmentDate);
                    products.Add(newProduct);
                }
            }
        }
        Inventory inventory = new Inventory();
        public Form3()
        {
            InitializeComponent();


            // Додаємо товари в базу
            inventory.AddProduct(new Product("ProductA", "pcs", 10.0m, 50, DateTime.Now.AddDays(-5)));
            inventory.AddProduct(new Product("ProductB", "kg", 20.5m, 30, DateTime.Now.AddDays(-10)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = Interaction.InputBox("Введіть назву товару", "Введення даних");
            string quan = Interaction.InputBox("Введіть кількість товару", "Введення даних");
            int quantity = Convert.ToInt32(quan);
            inventory.ReplenishProduct(name, quantity);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ssage = inventory.mes(inventory.products);
            MessageBox.Show(ssage, "Інформація про товари", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
