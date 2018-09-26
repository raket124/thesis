using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Combinations;
using Prism.Commands;
using master.Windows.Blocks;
using master.ViewModels.Contract.Block.Blocks;

namespace master.ViewModels.Contract.Block.Combinations
{
    class VMcreation : VMbase
    {
        public new MyCreation Root
        {
            get { return this.root as MyCreation; }
        }

        public VMcreation(MyCreation root, VMfunction parent) : base(root, parent)
        {
            this.CommandOpen = new DelegateCommand(() => new CreationWindow() { DataContext = this }.ShowDialog());
        }

        public override object Clone()
        {
            return new VMcreation(this.Root.Clone() as MyCreation, this.Parent);
        }

        protected override string BlockName() { return "Creation - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 new object"); }
        protected override string Optional() { return string.Format(this.reqFormat, "1 identifier, X features"); }

        public VMassign Object
        {
            get { return new VMassign(this.Root.Object, this.Parent); }
        }

        public IList<VMassign> Assignments
        {
            get
            {
                return new List<VMassign>(from a in this.Root.Modifications.Assignments
                                          select new VMassign(a, this.Parent));
            }
        }

        protected override List<VMvariable> GetVariables()
        {
            return new List<VMvariable>() {
                new VMvariable(new MyVariable(this.Root.Object.Value.Value.Type) { Alias = this.Root.Object.Value.Value.Alias, ObjectName = this.Root.Object.Value.Value.ObjectName })
            };
        }
    }
}
