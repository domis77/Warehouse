using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn.Warehouse
{
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
