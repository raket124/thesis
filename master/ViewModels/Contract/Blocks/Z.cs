using master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class Z : VMBbase
    {
        private string z;

        public Z(string z) : base(new Berror())
        {
            this.z = z;
        }

        public override object Clone()
        {
            return new Z(this.z);
        }

        public override string ToString()
        {
            return this.z;
        }

        protected override string BlockName()
        {
            return "Error block";
        }
    }
}
