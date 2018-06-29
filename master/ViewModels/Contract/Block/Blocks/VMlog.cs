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

        public VMlog(MyLog root) : base(root)
        {

        }

        public override object Clone()
        {
            return new VMlog(this.root.Clone() as MyLog)
            {
                Parent = this.Parent
            };
        }

        public string Text
        {
            get { return this.Root.Text; }
            set
            {
                this.Root.Text = value;
                this.NotifyPropertyChanged();
            }
        }

        protected override string BlockName() { return "Log block"; }

        protected override string Required() { return string.Format(this.reqFormat, "1 text"); }

        protected override string Optional() { return string.Format(this.optFormat, "x aliases"); }

        public override void FullRefresh()
        {
            this.NotifyPropertyChanged("Text");
        }
    }
}
