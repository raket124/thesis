using master.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMvariableGroupAlias : MyRootedBindableBase
    {
        public new string Root
        {
            get { return this.root as string; }
        }

        protected VMvariableGroup parent;
        public VMvariableGroup Parent
        {
            get { return this.parent; }
        }

        public VMvariableGroupAlias(string root, VMvariableGroup parent) : base(root)
        {
            this.parent = parent;
        }
    }
}
