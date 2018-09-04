using master.Models.Contract.Block.Combinations;
using master.Windows.Blocks;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Combinations
{
    class VMvalidation : VMbase
    {
        public new MyValidation Root
        {
            get { return this.root as MyValidation; }
        }

        public VMvalidation(MyValidation root, VMfunction parent) : base(root, parent)
        {
            this.CommandOpen = new DelegateCommand(() => new ValidationWindow() { DataContext = this }.ShowDialog());
        }

        public override object Clone()
        {
            return new VMvalidation(this.Root.Clone() as MyValidation, this.Parent);
        }

        protected override string BlockName() { return "Validation - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 existing object"); }
        protected override string Optional() { return string.Format(this.reqFormat, "X features"); }


    }
}
