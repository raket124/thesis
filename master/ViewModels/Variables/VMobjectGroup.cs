using master.Basis;
using master.Models.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMobjectGroup : MyRootedBindableBase
    {
        public new ObjectGroup Root
        {
            get { return this.root as ObjectGroup; }
        }

        protected VMvariableListing parent;
        public VMvariableListing Parent
        {
            get { return this.parent; }
        }
        public string Type
        {
            get { return this.Root.Type.Name; }
        }
        public IList<VMobjectValue> Objects
        {
            get { return (from o in this.Root.Objects select new VMobjectValue(o, this)).ToList(); }
        }

        public VMobjectGroup(ObjectGroup root, VMvariableListing parent) : base(root)
        {
            this.parent = parent;
        }

        public int AliasTotal
        {
            get { return this.Root.Objects.Sum(x => x.Aliases.Count); }
        }
    }
}
