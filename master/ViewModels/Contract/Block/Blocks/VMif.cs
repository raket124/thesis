using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using System.Collections.ObjectModel;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Conditioning;
using master.Utils;
using master.ViewModels.Contract.Block.Conditioning;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMif : VMbase
    {
        public new MyIf Root
        {
            get { return this.root as MyIf; }
        }
        protected VMcondition condition;
        public VMcondition Condition
        {
            get { return this.condition; }
            set
            {
                this.condition = value;
                this.NotifyPropertyChanged();
            }
        }

        public VMif(MyIf root, VMfunction parent) : base(root, parent)
        {
            this.Condition = new VMcondition(this.Root.Condition, this);
        }

        public override object Clone()
        {
            return new VMif(this.Root.Clone() as MyIf, this.Parent);
        }

        protected override string BlockName() { return "If - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1+ condition(s)"); }
        protected override string Optional() { return string.Empty; }
    }
}
