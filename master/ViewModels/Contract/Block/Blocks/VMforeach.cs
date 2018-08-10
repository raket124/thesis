using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using Prism.Commands;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMforeach : VMbase
    {
        public new MyForeach Root
        {
            get { return this.root as MyForeach; }
        }

        public DelegateCommand CommandSetVariable { get; private set; }

        public VMforeach(MyForeach root, VMfunction parent) : base(root, parent)
        {
            this.CommandSetVariable = new DelegateCommand(() => this.Variable = this.Parent.SelectVar());
        }

        public override object Clone()
        {
            return new VMforeach(this.Root.Clone() as MyForeach, this.Parent);
        }

        protected override string BlockName() { return "Foreach - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 variable, 1 alias"); }
        protected override string Optional() { return string.Empty; }

        public string Variable
        {
            get { return this.Root.Variable; }
            set
            {
                this.Root.Variable = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Alias
        {
            get { return this.Root.Alias; }
            set
            {
                this.Root.Alias = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}
