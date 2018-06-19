using master.Basis;
using master.Models;
using master.Models.Contract.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block
{
    abstract class VMbase: MyBindableBase, ICloneable
    {
        protected VMfunction parent;
        public VMfunction Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }
        protected Base root;
        public Base Root
        {
            get { return this.root; }
        }

        public VMbase(Base root)
        {
            this.root = root;
            this.parent = null;
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
