using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Kursova.Form2;

namespace Kursova
{
    public partial class Form2 : Form
    {
        public class Store
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public List<string> PhoneNumbers { get; set; }
            public string Specialization { get; set; }
            public string Ownership { get; set; }
            public string WorkingHours { get; set; }
        }
        City city = new City
        {
            Name = "Sample City"
        };
        public Form2()
        {

            InitializeComponent();
            // Створення міста


            // Додавання крамниць
            city.Stores.Add(new Store
            {
                Name = "Store1",
                Address = "Address1",
                PhoneNumbers = new List<string> { "123456789", "987654321" },
                Specialization = "Grocery",
                Ownership = "Private",
                WorkingHours = "9:00 AM - 6:00 PM"
            });

            city.Stores.Add(new Store
            {
                Name = "Store2",
                Address = "Address2",
                PhoneNumbers = new List<string> { "111222333", "444555666" },
                Specialization = "Electronics",
                Ownership = "Public",
                WorkingHours = "10:00 AM - 8:00 PM"
            });
        }
        public class City
        {
            public string store_message { get; set; }
            public string Name { get; set; }
            public List<Store> Stores { get; set; }

            public City()
            {
                Stores = new List<Store>();
            }

            public List<Store> SelectStores(Func<Store, bool> predicate)
            {
                return Stores.Where(predicate).ToList();
            }

            public void SaveSelectedStoresToFile(List<Store> selectedStores, string filePath)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var store in selectedStores)
                    {
                        writer.WriteLine($"Name: {store.Name}");
                        writer.WriteLine($"Address: {store.Address}");
                        writer.WriteLine($"Phone Numbers: {string.Join(", ", store.PhoneNumbers)}");
                        writer.WriteLine($"Specialization: {store.Specialization}");
                        writer.WriteLine($"Ownership: {store.Ownership}");
                        writer.WriteLine($"Working Hours: {store.WorkingHours}");
                        writer.WriteLine();
                    }
                }
            }

            public string StoreInfo(Store store)
            {
                return $"Назва: {store.Name}\nАдреса: {store.Address}\nТелефон: {string.Join(", ", store.PhoneNumbers)}\nСпеціалізація: {store.Specialization}\nФорма власності {store.Ownership}\nЧас роботи {store.WorkingHours}";

            }
            public string MessageStore(List<Store> stores)
            {
                int i = 1;
                string res = "";
                foreach (var store in stores)
                {
                    res += $"{i}) {StoreInfo(store)} \n \n";
                    i++;
                }
                return res;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string temp = city.MessageStore(city.Stores);
            MessageBox.Show(temp, "Інформація про магазини", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name, address, number, spec, ownersip, hours;
            name = Interaction.InputBox("Введіть назву магазину", "Введення даних");
            address = Interaction.InputBox("Введіть адресу магазину", "Введення даних");
            number = Interaction.InputBox("Введіть телефон магазину", "Введення даних");
            spec = Interaction.InputBox("Введіть спеціалізація магазину", "Введення даних");
            ownersip = Interaction.InputBox("Введіть форму власності магазину", "Введення даних");
            hours = Interaction.InputBox("Введіть години роботи магазину", "Введення даних");
            city.Stores.Add(new Store
            {
                Name = name,
                Address = address,
                PhoneNumbers = new List<string> { number },
                Specialization = spec,
                Ownership = ownersip,
                WorkingHours = hours
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Вибір крамниць за шаблоном (наприклад, тільки ті, що належать до категорії Grocery)
            var selectedStores = city.SelectStores(store => store.Specialization == "Grocery");
            // Збереження в файл
            city.SaveSelectedStoresToFile(selectedStores, "SelectedStores.txt");
            MessageBox.Show("Магазини відсортировані. Результати виведені у файл");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
