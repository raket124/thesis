using master.Basis;
using master.Models.Variables;
using master.ViewModels.Contract.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMvariableGroup : MyRootedParentalBindableBase
    {
        public new VariableGroup Root
        {
            get { return this.root as VariableGroup; }
        }
        public new VMvariableList Parent
        {
            get { return this.parent as VMvariableList; }
        }

        public VMvariableGroup(VariableGroup root, VMvariableList parent) : base(root, parent)
        {

        }

        public Type Type
        {
            get { return this.Root.Type; }
        }

        public IList<VMvariable> Variables
        {
            get
            {
                return (from v in this.Root.Variables
                        select new VMvariable(v)).ToList();
            }
        }

    }
}
