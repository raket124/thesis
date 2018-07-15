using master.Basis;
using master.Models.Contract.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block
{
    class VMvariableLink : MyRootedBindableBase, ICloneable
    {
        public new VariableLink Root
        {
            get { return this.root as VariableLink; }
        }

        public VMvariableLink(VariableLink root) : base(root)
        {

        }

        public object Clone()
        {
            return new VMvariableLink(this.Root.Clone() as VariableLink);
        }

        public VMvariable Value
        {
            get { return new VMvariable(this.Root.Value); }
            set
            {
                this.Root.Value = value.Root;
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged("Listing");
            }
        }

        public void AddLast(VMvariable input)
        {
            this.Root.AddLast(input.Root);
            this.NotifyPropertyChanged("Listing");
        }

        public void RemoveLast()
        {
            this.Root.RemoveLast();
            this.NotifyPropertyChanged("Listing");
        }

        public bool CanRemoveLast
        {
            get { return this.Root.CanRemoveLast; }
        }

        public void Clear()
        {
            this.Root.Clear();
            this.NotifyPropertyChanged("Listing");
        }

        public IList<VMvariable> Listing
        {
            get
            {
                return new List<VMvariable>(from l in this.Root.Listing
                                            select new VMvariable(l));
            }
        }
    }
}
