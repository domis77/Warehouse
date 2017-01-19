using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace Magazyn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Warehouse.Warehouse warehouse;

        public MainWindow()
        {
            InitializeComponent();

            warehouse = new Warehouse.Warehouse();
        }



        private void addProduct_button_Click(object sender, RoutedEventArgs e)
        {
            string name;
            string description;
            float price;

            if (name_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Enter name!");
                return;
            }
            else
            {
                name = name_textBox.Text;
            }

            if (description_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Enter description!");
                return;
            }
            else
            {
                description = description_textBox.Text;
            }

            if (price_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Enter price!");
                return;
            }
            else
            {
                if (float.TryParse(price_textBox.Text, out price)) { }
                else
                {
                    System.Windows.MessageBox.Show("Entered price is not a number!");
                    return;
                }
            }

            warehouse.addProduct(name, description, price);
            warehouse.showProducts(addressBook_listView);
            addressBook_listView.Items.Refresh();

            name_textBox.Text = "";
            description_textBox.Text = "";
            price_textBox.Text = "";

            System.Windows.MessageBox.Show("Product added.");
        }

        private void deleteProduct_button_Click(object sender, RoutedEventArgs e)
        {
            Warehouse.Product selectedProduct = (Warehouse.Product)addressBook_listView.SelectedItem;
            
            warehouse.deleteProduct(selectedProduct);
            addressBook_listView.Items.Refresh();
        }

        private void saveToFile_button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            warehouse.saveToFile(folderBrowserDialog.SelectedPath);
        }

    }
}
