using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazyn.Warehouse
{
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
}
