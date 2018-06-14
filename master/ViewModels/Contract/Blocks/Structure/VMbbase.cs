using master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    abstract class VMBbase: MyBindableBase, ICloneable
    {
        protected Bbase root;
        public Bbase Root
        {
            get { return this.root; }
            set { this.root = value; }
        }

        public VMBbase(Bbase root)
        {
            this.root = root;
        }

        public string Documentation
        {
            get { return this.root.Docs; }
        }

        public string Name
        {
            get { return this.BlockName(); }
        }

        public abstract object Clone();

        protected abstract string BlockName();
    }
}
