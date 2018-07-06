using master.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMobjectValueAlias : MyRootedBindableBase
    {
        public new string Root
        {
            get { return this.root as string; }
        }

        protected VMobjectValue parent;
        public VMobjectValue Parent
        {
            get { return this.parent; }
        }

        public VMobjectValueAlias(string root, VMobjectValue parent) : base(root)
        {
            this.parent = parent;
        }
    }
}
