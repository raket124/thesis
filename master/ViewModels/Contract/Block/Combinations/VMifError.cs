using master.Models.Contract.Block.Combinations;
using master.ViewModels.Contract.Block.Blocks;
using master.Windows.Blocks;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Combinations
{
    class VMifError : VMbase
    {
        public new MyIfError Root
        {
            get { return this.root as MyIfError; }
        }

        public VMif If
        {
            get { return new VMif(this.Root.If, this.Parent); }
        }

        public VMerror Error
        {
            get { return new VMerror(this.Root.Error, this.Parent); }
        }

        public VMifError(MyIfError root, VMfunction parent) : base(root, parent)
        {
            this.CommandOpen = new DelegateCommand(() => new IfErrorWindow() { DataContext = this }.ShowDialog());
        }

        public override object Clone()
        {
            return new VMifError(this.Root.Clone() as MyIfError, this.Parent);
        }

        protected override string BlockName() { return "If error - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 comparison, 1 error"); }
        protected override string Optional() { return string.Empty; }
    }
}
