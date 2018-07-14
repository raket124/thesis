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
        public new MyLog Root
        {
            get { return this.root as MyLog; }
        }

        public VMlog(MyLog root, VMfunction parent) : base(root, parent)
        {

        }

        public override object Clone()
        {
            return new VMlog(this.Root.Clone() as MyLog, this.Parent);
        }

        protected override string BlockName() { return "Log - block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 text"); }
        protected override string Optional() { return string.Format(this.optFormat, "x aliases"); }

        public string Text
        {
            get { return this.Root.Text; }
            set
            {
                this.Root.Text = value;
                this.NotifyPropertyChanged();
            }
        }

        public override void FullRefresh()
        {
            this.NotifyPropertyChanged("Text");
        }
    }
}
