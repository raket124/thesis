using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using Prism.Commands;
using master.Windows.Blocks;
using master.Models.Data.Component.Components;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMforeach : VMbase
    {
        public new MyForeach Root
        {
            get { return this.root as MyForeach; }
        }

        public DelegateCommand CommandSetList { get; private set; }

        public VMforeach(MyForeach root, VMfunction parent) : base(root, parent)
        {
            this.CommandOpen = new DelegateCommand(() => new ForeachWindow() { DataContext = this }.ShowDialog());
            this.CommandSetList = new DelegateCommand(() => this.List = this.Parent.SelectVar());
        }

        public override object Clone()
        {
            return new VMforeach(this.Root.Clone() as MyForeach, this.Parent);
        }

        protected override string BlockName() { return "Foreach - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 variable, 1 alias"); }
        protected override string Optional() { return string.Empty; }

        public string ObjectAlias
        {
            get { return this.Root.ObjectAlias.Value.Alias; }
            set
            {
                this.Root.ObjectAlias.Value.Alias = value;
                this.NotifyPropertyChanged();
            }
        }

        public string IteratorAlias
        {
            get { return this.Root.IteratorAlias.Value.Alias; }
            set
            {
                this.Root.IteratorAlias.Value.Alias = value;
                this.NotifyPropertyChanged();
            }
        }

        public VariableLink List
        {
            get { return this.Root.List; }
            set
            {
                this.Root.List = value;
                this.NotifyPropertyChanged();
            }
        }

        protected override List<VMvariable> GetVariables()
        {
            return new List<VMvariable>() {
                new VMvariable(new MyVariable(typeof(int)) { Alias = "i" }),
                new VMvariable(new MyVariable(typeof(MyParticipant)) { Alias = "user", ObjectName = "User" })
            };
        }
    }
}
