using master.Models;
using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMassign : VMbase
    {
        public new MyAssign Root
        {
            get { return this.root as MyAssign; }
        }

        public VMassign(MyAssign root) : base(root)
        {

        }

        public override object Clone()
        {
            return new VMassign(this.root.Clone() as MyAssign);
        }

        protected override string BlockName() { return "Block chain block"; }

        protected override string Required() { return string.Format(this.reqFormat, "2 variables"); }

        protected override string Optional() { return string.Empty; }

        public IList<string> Aliases
        {
            get
            {
                if (this.Parent != null)
                    return this.Parent.GetAliases();
                else
                    return new List<string>();
            }
        }

        public override void FullRefresh()
        {
            this.NotifyPropertyChanged("Aliases");
        }
    }
}
