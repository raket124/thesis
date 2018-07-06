using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Basis
{
    public class MyRootedBindableBase : MyBindableBase
    {
        protected object root;
        public object Root
        {
            get { return this.root; }
        }

        public MyRootedBindableBase(object root)
        {
            this.root = root;
        }
    }
}
