using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks.Custom;
using Prism.Commands;

namespace master.ViewModels.Contract.Block.Blocks.Custom
{
    class VMtotalEcmrs : VMbase
    {
        public new MyTotalEcmrs Root
        {
            get { return this.root as MyTotalEcmrs; }
        }

        public DelegateCommand CommandSetInput { get; private set; }

        public VMtotalEcmrs(MyTotalEcmrs root, VMfunction parent) : base(root, parent)
        {
            this.CommandSetInput = new DelegateCommand(() => this.Input = this.Parent.SelectVar());
        }

        public override object Clone()
        {
            return new VMtotalEcmrs(this.Root.Clone() as MyTotalEcmrs, this.Parent);
        }

        protected override string BlockName() { return "Total ecmrs - block"; }
        protected override string Optional() { return string.Empty; }
        protected override string Required() { return string.Format(this.reqFormat, "1 List of ecmrs"); }

        public string Input
        {
            get { return this.Root.Input; }
            set
            {
                this.Root.Input = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Alias
        {
            get { return this.Root.Alias; }
            set
            {
                this.Root.Alias = value;
                this.NotifyPropertyChanged();
            }
        }

        protected override List<VMvariable> GetVariables()
        {
            return new List<VMvariable>()
            {
                new VMvariable(new MyVariable(typeof(int))
                {
                    Alias = this.Alias,
                })
            };
        }
    }
}
