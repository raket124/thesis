using master.Basis;
using master.Models;
using master.Models.Contract;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.ViewModels.Contract.Block;
using master.ViewModels.Contract.Block.Blocks;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract
{
    class VMfunction : MyBindableBase
    {
        private Function root;
        public Function Root
        {
            get { return this.root; }
        }
        private VMcontractModel parent;
        public VMcontractModel Parent
        {
            get { return this.parent; }
        }
        private ObservableCollection<VMbase> blocks;
        public ObservableCollection<VMbase> Blocks
        {
            get { return this.blocks; }
            set
            {
                this.blocks = value;
                this.NotifyPropertyChanged();
            }
        }

        public VMfunction(Function root, VMcontractModel parent)
        {
            this.root = root;
            this.parent = parent;
            this.WrapBlocks();
        }

        private void WrapBlocks()
        {
            var output = new ObservableCollection<VMbase>();
            foreach(Base block in this.root.Blocks)
            {
                if (block.GetType() == typeof(MyInput))
                    output.Add(new VMinput(block as MyInput));
                if (block.GetType() == typeof(MyLog))
                    output.Add(new VMlog(block as MyLog));
                if (block.GetType() == typeof(MyAssign))
                    output.Add(new VMassign(block as MyAssign));
                //Add new blocks here
            }
            this.Blocks = output;
        }

        public string Name
        {
            get { return this.root.Name; }
        }
        public string Docs
        {
            get { return this.root.Docs == string.Empty ? "No documentation is available." : this.root.Docs; }
        }
        public Function.ACCESSIBILITY Access
        {
            get { return this.root.Access; }
        }
    }
}
