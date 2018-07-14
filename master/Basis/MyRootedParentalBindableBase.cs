using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Basis
{
    class MyRootedParentalBindableBase : MyRootedBindableBase
    {
        protected object parent;
        public object Parent
        {
            get { return this.parent; }
        }

        public MyRootedParentalBindableBase(object root, object parent) : base(root)
        {
            this.parent = parent;
        }
    }
}
