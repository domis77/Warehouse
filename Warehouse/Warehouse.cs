using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using System.Collections.ObjectModel;
using Magazyn.Tools;

namespace Magazyn.Warehouse
{
    class Warehouse
    {
        public List<Product> productList = new List<Product>();
        

        public void addProduct(string name, string description, float price, double reciverLimit, Reciver reciver, Category category)
        {
            Product product = new Product(name, description, price, reciverLimit, reciver, category);
            product.id = productList.Count;

            productList.Add(product);

            DatabaseConnection.InsertProduct(productList);
        }

        public void deleteProduct(Product selectedProduct)
        {
            productList.Remove(selectedProduct);
            DatabaseConnection.DeleteProduct(selectedProduct);
        }

        public void showProducts(ListView warehouse_listView)
        {
            warehouse_listView.ItemsSource = productList; 
        }

        public void getProductFromDatabase()
        {
            DatabaseConnection.GetProducts(productList);
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

}
