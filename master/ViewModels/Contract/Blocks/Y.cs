using master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class Y : VMBbase
    {
        private string y;

        public Y(string y) : base(new Bassign())
        {
            this.y = y;
        }

        public override object Clone()
        {
            return new Y(this.y);
        }

        public override string ToString()
        {
            return this.y;
        }

        protected override string BlockName()
        {
            return "Assign block";
        }
    }
}
