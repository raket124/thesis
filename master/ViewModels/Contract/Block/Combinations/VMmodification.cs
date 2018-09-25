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
    class VMmodification : VMbase
    {
        public new MyModification Root
        {
            get { return this.root as MyModification; }
        }

        public VMmodification(MyModification root, VMfunction parent) : base(root, parent)
        {
            this.CommandOpen = new DelegateCommand(() => new ModificationWindow() { DataContext = this }.ShowDialog());
        }

        public override object Clone()
        {
            return new VMmodification(this.Root.Clone() as MyModification, this.Parent);
        }

        protected override string BlockName() { return "Modification - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 existing object"); }
        protected override string Optional() { return string.Format(this.reqFormat, "X features"); }

        public IList<VMassign> Assignments
        {
            get { return new List<VMassign>(from a in this.Root.Assignments
                                            select new VMassign(a, this.Parent)); }
        }

    }
}
