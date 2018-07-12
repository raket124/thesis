using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMerror : VMbase
    {
        public new MyError Root
        {
            get { return this.root as MyError; }
        }

        public VMerror(MyError root) : base(root)
        {
        }

        public override object Clone()
        {
            return new VMerror(this.root.Clone() as MyError)
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

        protected override string BlockName() { return "Error block"; }

        protected override string Required() { return string.Format(this.reqFormat, "1 text"); }

        protected override string Optional() { return string.Format(this.optFormat, "x aliases"); }

        public override void FullRefresh()
        {
            this.NotifyPropertyChanged("Text");
        }
    }
}
