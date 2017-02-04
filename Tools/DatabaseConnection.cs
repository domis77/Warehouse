using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Dapper;
using System.Collections;


namespace Magazyn.Tools
{
    class DatabaseConnection
    {
        private static string connectionString = "User Id=ziibd1;Password=haslo1;Data Source=155.158.112.45:1521/oltpstud";


        public static void CheckIfTableExist()
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                try
                {
                    connection.Execute(@"Select * from PRODUCT");
                    return;
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2147467259)
                    {
                        connection.Execute(@"CREATE TABLE PRODUCT 
                            (ID INTEGER NOT NULL PRIMARY KEY
                            , NAME VARCHAR(20) 
                            , DESCRIPTION VARCHAR(100) 
                            , PRICE FLOAT(126) 
                            , RECEIVED CHAR(1) DEFAULT 'N' 
                            , RECEIVER_LIMIT DATE 
                            , RECEIVER VARCHAR(20) 
                            , CATEGORY VARCHAR(20) )");
                    }
                    else
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                connection.Clone();
            }
        }


        public static void GetProducts(List<Warehouse.Product> productList)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();


                List<object> rows = new List<object>();
                rows = connection.Query<object>(@"Select * from products").ToList();

                ArrayList val = new ArrayList();
                foreach (IDictionary<string, object> row in rows)
                {
                    foreach (var pair in row)
                    {
                        val.Add(pair.Value);
                    }

                    productList.Add(new Warehouse.Product());
                    var last = productList.Last<Warehouse.Product>();

                    last.id = Convert.ToInt32(val[0]);
                    last.name = Convert.ToString(val[1]);
                    last.description = Convert.ToString(val[2]);
                    last.price = Convert.ToSingle(val[3]);
                    if (Convert.ToChar(val[4]) == 'N')
                    {
                        last.received = false;
                    }
                    else
                    {
                        last.received = true;
                    }
                    last.receiverLimit = (DateTime)val[5];
                    last.receiver = new Warehouse.Reciver(-1, Convert.ToString(val[6]));
                    last.category = new Warehouse.Category(-1, Convert.ToString(val[7]));

                    val.Clear();
                }
                connection.Close();
            }
        }


        public static void InsertProduct(List<Warehouse.Product> productList)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {

                connection.Open();

                Warehouse.Product product = productList.Last<Warehouse.Product>();

                DynamicParameters values = new DynamicParameters();
                values.Add(name: "id", value: product.id, direction: ParameterDirection.Input);
                values.Add(name: "name", value: product.name, direction: ParameterDirection.Input);
                values.Add(name: "description", value: product.description, direction: ParameterDirection.Input);
                values.Add(name: "price", value: product.price, direction: ParameterDirection.Input);
                if (product.received)
                {
                    values.Add(name: "received", value: 'Y', direction: ParameterDirection.Input);
                }
                else
                {
                    values.Add(name: "received", value: 'N', direction: ParameterDirection.Input);
                }
                values.Add(name: "receiverLimit", value: product.receiverLimit, direction: ParameterDirection.Input);
                values.Add(name: "receiver", value: product.receiver.receiver, direction: ParameterDirection.Input);
                values.Add(name: "category", value: product.category.category, direction: ParameterDirection.Input);


                try
                {
                    string insertQuery = "insert into PRODUCTS values (:id, :name, :description, :price, :received, :receiverLimit, :receiver, :category)";
                    connection.Query<Warehouse.Product>(insertQuery, values);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

                connection.Close();
            }
        }

        public static void DeleteProduct(Warehouse.Product product)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                try
                {
                    string deleteQuery = "DELETE FROM PRODUCTS WHERE id = :id";
                    connection.Query<Warehouse.Product>(deleteQuery, product);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                connection.Close();
            }
        }

        public static void UpdateReceived(Warehouse.Product product)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                try
                {
                    string updateQuery;
                    if (product.received)
                    {
                        updateQuery = "UPDATE PRODUCTS SET received = 'Y' WHERE id = :id";
                    }
                    else
                    {
                        updateQuery = "UPDATE PRODUCTS SET received = 'N' WHERE id = :id";
                    }
                    connection.Query<Warehouse.Product>(updateQuery, product);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                connection.Close();
            }
        }

    }
}