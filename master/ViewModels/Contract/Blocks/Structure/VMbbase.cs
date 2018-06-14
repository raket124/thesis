using master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    abstract class VMBbase: ICloneable
    {
        protected Bbase root;

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
