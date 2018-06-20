using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Conditioning
{
    public class Cvariable
    {
        protected string name;
        protected Cvariable child;
        protected object type;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public Cvariable Child
        {
            get { return this.child; }
            set { this.child = value; }
        }

        public object Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public Cvariable()
        {
            this.name = string.Empty;
            this.child = null;
            this.type = null;
        }

        public override string ToString()
        {
            if (child == null)
                return this.name;
            else
                return string.Format("{0}.{1}", this.name, this.child);
        }
    }
}
