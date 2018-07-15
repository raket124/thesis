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
                //return (from a in this.Root.Aliases
                //        select new VMvariableGroupAlias(a, this)).ToList();
                return new List<VMvariable>()
                {
                    new VMvariable(new Models.Contract.Block.Variable(typeof(string))
                    {
                        Alias = "A"
                    })
                };
            }
        }

    }
}
