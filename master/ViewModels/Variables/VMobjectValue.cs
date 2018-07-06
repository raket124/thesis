using master.Basis;
using master.Models.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMobjectValue : MyRootedBindableBase
    {
        public new ObjectValue Root
        {
            get { return this.root as ObjectValue; }
        }

        protected VMobjectGroup parent;
        public VMobjectGroup Parent
        {
            get { return this.parent; }
        }
        public string Name
        {
            get { return this.Root.Name; }
        }
        public IList<VMobjectValueAlias> Aliases
        {
            get
            {
                return (from a in this.Root.Aliases
                        select new VMobjectValueAlias(a, this)).ToList();
            }
        }

        public VMobjectValue(ObjectValue root, VMobjectGroup parent) : base(root)
        {
            this.parent = parent;
        }
    }
}
