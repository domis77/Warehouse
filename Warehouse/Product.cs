using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn.Warehouse
{
    class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }

        public bool received { get; set; }
        public DateTime receiverLimit { get; set; }

        public Reciver receiver { get; set; }
        public Category category { get; set; }

        public Product() { }

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
}
