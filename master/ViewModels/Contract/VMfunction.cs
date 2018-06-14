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
    class VMfunction : MyBindableBase
    {
        private VMcontract parent;
        public VMcontract Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }
        private Cfunction root;
        public Cfunction Root
        {
            get { return this.root; }
        }
        private ObservableCollection<VMBbase> blocks;
        public ObservableCollection<VMBbase> Blocks
        {
            get { return this.blocks; }
            set
            {
                this.blocks = value;
                this.NotifyPropertyChanged();
            }
        }


        public VMfunction(Cfunction root, VMcontract parent)
        {
            this.root = root;
            this.parent = parent;

            this.Blocks = new ObservableCollection<VMBbase>(
                          from block in this.root.Blocks
                          select new Y(string.Empty));
        }

        public string Name
        {
            get { return this.root.Name; }
        }
        public string Docs
        {
            get
            {
                return this.root.Docs == string.Empty ? "No documentation is available." : this.root.Docs;
            }
        }
        public Cfunction.ACCESSIBILITY Access
        {
            get { return this.root.Access; }
        }
    }
}
