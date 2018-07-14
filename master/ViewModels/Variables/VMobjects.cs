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
    class VMobjects : MyRootedParentalBindableBase
    {
        public new Objects Root
        {
            get { return this.root as Objects; }
        }
        public new VMobjectGroup Parent
        {
            get { return this.parent as VMobjectGroup; }
        }

        public VMobjects(Objects root, VMobjectGroup parent) : base(root, parent)
        {
            
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
