using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;

namespace Magazyn.Warehouse
{
    class Warehouse
    {
        private List<Product> productList = new List<Product>();

        public void addProduct(string name, string description, float price)
        {
            Product product = new Product(name, description, price);
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
                            sw.WriteLine("Id: " + p.id + ", Name: " +  p.name + ", Description: " + p.description + ", Price: " + p.price);
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
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }

        public Product(string name, string description, float price)
        {
            this.name = name;
            this.description = description;
            this.price = price;
        }
    }
}
