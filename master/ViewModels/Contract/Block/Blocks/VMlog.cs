using master.Models;
using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMlog : VMbase
    {
        protected new MyLog root;
        public new MyLog Root
        {
            get { return this.root; }
        }

        public VMlog(MyLog root) : base(root)
        {
            this.root = root;
        }

        public override object Clone()
        {
            return new VMlog(this.root.Clone() as MyLog);
        }

        public string Text
        {
            get { return this.root.Text; }
            set
            {
                this.root.Text = value;
                this.NotifyPropertyChanged();
            }
        }

        protected override string BlockName()
        {
            return "Log block";
        }
    }
}
