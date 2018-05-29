using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMfunction
    {
        protected object root;

        public VMfunction(object root)
        {
            this.root = root;
        }



        public string Name
        {
            get { return "hello"; }
            set { this.Name = value; }
        }
    }
}
