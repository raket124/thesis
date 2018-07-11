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

        public VMtotalEcmrs(MyTotalEcmrs root) : base(root)
        {
            this.CommandSetInput = new DelegateCommand(this.SetInput);
        }

        public override object Clone()
        {
            return new VMtotalEcmrs(this.Root.Clone() as MyTotalEcmrs)
            {
                Parent = this.Parent
            };
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

        public void SetInput()
        {
            this.Input = this.SelectVar();
        }
    }
}
