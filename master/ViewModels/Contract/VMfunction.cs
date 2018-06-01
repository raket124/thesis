using master.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMfunction
    {
        protected VMcontract parent;
        protected Cfunction root;

        public VMfunction(Cfunction root, VMcontract parent)
        {
            this.root = root;
            this.parent = parent;
        }

        public string Name
        {
            get { return this.root.Name; }
        }

        public Cfunction.ACCESSIBILITY Access
        {
            get { return this.root.Access; }
        }
    }
}
