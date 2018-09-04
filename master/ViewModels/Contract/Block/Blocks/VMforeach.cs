using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using Prism.Commands;
using master.Windows.Blocks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMforeach : VMbase
    {
        public new MyForeach Root
        {
            get { return this.root as MyForeach; }
        }

        public VMforeach(MyForeach root, VMfunction parent) : base(root, parent)
        {
            this.CommandOpen = new DelegateCommand(() => new ForeachWindow() { DataContext = this }.ShowDialog());
        }

        public override object Clone()
        {
            return new VMforeach(this.Root.Clone() as MyForeach, this.Parent);
        }

        protected override string BlockName() { return "Foreach - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 variable, 1 alias"); }
        protected override string Optional() { return string.Empty; }

        public string ViewObjectAlias
        {
            get
            {
                if (this.Root.ObjectAlias.Value.Alias == string.Empty)
                    return "?";
                else
                    return this.Root.ObjectAlias.Value.Alias;
            }
        }

        public string ViewIteratorAlias
        {
            get { return this.Root.IteratorAlias.Value.Alias; }
        }

        public string ViewListAlias
        {
            get
            {
                if (this.Root.List.Value.Alias == string.Empty)
                    return "?";
                else
                    return this.Root.List.Value.Alias;
            }
        }
    }
}
