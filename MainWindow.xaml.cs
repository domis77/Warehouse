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

        private List<Warehouse.Reciver> receiverList = new List<Warehouse.Reciver>();
        private List<Warehouse.Category> categoryList = new List<Warehouse.Category>();

        public MainWindow()
        {
            InitializeComponent();

            warehouse = new Warehouse.Warehouse();

            receiverList_listView.ItemsSource = receiverList;
            categoryList_listView.ItemsSource = categoryList;
            
        }



        private void addProduct_button_Click(object sender, RoutedEventArgs e)
        {
            string name;
            string description;
            float price;
            double receiveLimit;

            Warehouse.Reciver selectedReceiver = (Warehouse.Reciver)receiverList_listView.SelectedItem;
            if (selectedReceiver == null)
            {
                System.Windows.MessageBox.Show("Select reciver!");
                return;
            }

            Warehouse.Category selectedCategory = (Warehouse.Category)categoryList_listView.SelectedItem;
            if (selectedCategory == null)
            {
                System.Windows.MessageBox.Show("Select category!");
                return;
            }

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

            if (reciveLimit_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Enter recive limit!");
                return;
            }
            else
            {
                if (double.TryParse(reciveLimit_textBox.Text, out receiveLimit)) { }
                else
                {
                    System.Windows.MessageBox.Show("Entered recive limit is not a number!");
                    return;
                }
            }

            warehouse.addProduct(name, description, price, receiveLimit, selectedReceiver, selectedCategory);
            warehouse.showProducts(warehouse_listView);
            warehouse_listView.Items.Refresh();

            name_textBox.Text = "";
            description_textBox.Text = "";
            price_textBox.Text = "";
            reciveLimit_textBox.Text = "";

            System.Windows.MessageBox.Show("Product added.");
        }

        private void deleteProduct_button_Click(object sender, RoutedEventArgs e)
        {
            Warehouse.Product selectedProduct = (Warehouse.Product)warehouse_listView.SelectedItem;
            
            warehouse.deleteProduct(selectedProduct);
            warehouse_listView.Items.Refresh();
        }

        private void saveToFile_button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            warehouse.saveToFile(folderBrowserDialog.SelectedPath);
        }


        private void find_button_Click(object sender, RoutedEventArgs e)
        {
            string name;
            List<Warehouse.Product> foundProductList = new List<Warehouse.Product>();

            if (findName_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Enter name to find!");
                return;
            }
            else
            {
                name = findName_textBox.Text;
            }
            foreach (var product in warehouse.productList)
            {
                if(product.name == name)
                {
                    foundProductList.Add(product);
                }
            }

            if(foundProductList.Count == 0)
            {
                System.Windows.MessageBox.Show("Product not found!");
                return;
            }
            else
            {
                warehouse_listView.ItemsSource = foundProductList;
                warehouse_listView.Items.Refresh();
                findName_textBox.Text = "";
            }
        }

        private void restore_button_Click(object sender, RoutedEventArgs e)
        {
            warehouse.showProducts(warehouse_listView);
            warehouse_listView.Items.Refresh();
        }



        //---RECIVER---------------->
        private void addReceiver_button_Click(object sender, RoutedEventArgs e)
        {
            string name;

            if (addReceiverName_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Enter receive name!");
                return;
            }
            else
            {
                name = addReceiverName_textBox.Text;
            }

            receiverList.Add(new Warehouse.Reciver(receiverList.Count, name));

            receiverList_listView.Items.Refresh();
            addReceiverName_textBox.Text = "";
        }

        private void deleteReceiver_button_Click(object sender, RoutedEventArgs e)
        {
            Warehouse.Reciver selectedReceiver = (Warehouse.Reciver)receiverList_listView.SelectedItem;
            if(selectedReceiver == null)
            {
                System.Windows.MessageBox.Show("Select receive to delete!");
                return;
            }

            receiverList.Remove(selectedReceiver);
            receiverList_listView.Items.Refresh();
        }

        private void editReceiver_button_Click(object sender, RoutedEventArgs e)
        {
            string name;

            Warehouse.Reciver selectedReceiver = (Warehouse.Reciver)receiverList_listView.SelectedItem;
            if (selectedReceiver == null)
            {
                System.Windows.MessageBox.Show("Select receive to edit!");
                return;
            }

            if (editReceiverName_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Enter receive name to edit!");
                return;
            }
            else
            {
                name = editReceiverName_textBox.Text;
            }
            
            int selectedReciverIndex = receiverList.IndexOf(selectedReceiver);

            receiverList[selectedReciverIndex].editName(name);
            receiverList_listView.Items.Refresh();
            editReceiverName_textBox.Text = "";
        }



        //---CATEGORY---------------->
        private void addCategory_button_Click(object sender, RoutedEventArgs e)
        {
            string name;

            if (addCategoryName_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Enter category name!");
                return;
            }
            else
            {
                name = addCategoryName_textBox.Text;
            }

            categoryList.Add(new Warehouse.Category(categoryList.Count, name));

            categoryList_listView.Items.Refresh();
            addCategoryName_textBox.Text = "";
        }

        private void deleteCategory_button_Click(object sender, RoutedEventArgs e)
        {
            Warehouse.Category selectedCategory = (Warehouse.Category)categoryList_listView.SelectedItem;
            if (selectedCategory == null)
            {
                System.Windows.MessageBox.Show("Select category to delete!");
                return;
            }

            categoryList.Remove(selectedCategory);
            categoryList_listView.Items.Refresh();
        }

        private void editCategory_button_Click(object sender, RoutedEventArgs e)
        {
            string name;

            Warehouse.Category selectedCategory = (Warehouse.Category)categoryList_listView.SelectedItem;
            if (selectedCategory == null)
            {
                System.Windows.MessageBox.Show("Select category to edit!");
                return;
            }

            if (editCategoryName_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Enter category name to edit!");
                return;
            }
            else
            {
                name = editCategoryName_textBox.Text;
            }

            int selectedCategoryIndex = categoryList.IndexOf(selectedCategory);

            categoryList[selectedCategoryIndex].editName(name);
            categoryList_listView.Items.Refresh();
            editCategoryName_textBox.Text = "";
        }
    }
}
