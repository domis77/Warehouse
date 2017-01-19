using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using System.Collections.ObjectModel;

namespace Magazyn.Warehouse
{
    class Warehouse
    {
        public List<Product> productList = new List<Product>();

        private ObservableCollection<Reciver> reciverList = new ObservableCollection<Reciver>();
        

        public void addProduct(string name, string description, float price, double reciverLimit, Reciver reciver, Category category)
        {
            Product product = new Product(name, description, price, reciverLimit, reciver, category);
            product.id = productList.Count;

            productList.Add(product);
        }

        public void deleteProduct(Product selectedProduct)
        {
            productList.Remove(selectedProduct);
        }

        public void showProducts(ListView warehouse_listView)
        {
            warehouse_listView.ItemsSource = productList; 
        }

        public void saveToFile(string filePatch)
        {
            string file = filePatch + "\\warehouse.txt";
            try
            {
                if ((!File.Exists(filePatch)))
                {
                    using(StreamWriter sw = File.CreateText(file))
                    {
                        foreach(var p in productList)
                        {
                            sw.WriteLine("Id: " + p.id + ", Name: " +  p.name + ", Description: " + p.description + ", Price: " + p.price + ", Category: " + p.category.category + ", Receiver: " + p.receiver.receiver + ", Received: " + p.received + ", Receiver limit: " + p.receiverLimit);
                        }
                        sw.Close();
                    }
                }
                else
                {
                    MessageBox.Show("File exist already!");
                    return;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed create file! Orginal Error: " + ex.Message);
            }
        }
    }



    class Product
    {
        public int id { get; set; }
        public string name { get; private set; }
        public string description { get; private set; }
        public float price { get; private set; }

        public bool received { get; set; }
        public DateTime receiverLimit { get; private set; }

        public Reciver receiver { get; private set; }
        public Category category { get; private set; }

        
        public Product(string name, string description, float price, double receiverLimit, Reciver receiver, Category category)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.receiver = receiver;
            this.category = category;
            this.receiverLimit = DateTime.Now.AddDays(receiverLimit);

            this.received = false;

        }        
    }


    class Reciver
    {
        public int id { get; private set; }
        public string receiver { get; private set; }

        public Reciver(int id, string reciverName)
        {
            this.id = id;
            this.receiver = reciverName;
        }

        public void editName(string newReciverName)
        {
            this.receiver = newReciverName;
        }
    }

    class Category
    {
        public int id { get; set; }
        public string category { get; set; }

        public Category(int id, string categoryName)
        {
            this.id = id;
            this.category = categoryName;
        }

        public void editName(string newCategoryName)
        {
            this.category = newCategoryName;
        }
    }
}
