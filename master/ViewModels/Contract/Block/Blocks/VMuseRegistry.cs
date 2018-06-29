using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Basis;
using master.Models.Contract.Block.Blocks;
using master.Utils;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMuseRegistry : VMbase
    {
        public new MyUseRegistry Root
        {
            get { return this.root as MyUseRegistry; }
        }

        public VMuseRegistry(MyUseRegistry root) : base(root)
        {

        }

        public override object Clone()
        {
            return new VMuseRegistry(this.root.Clone() as MyUseRegistry)
            {
                Parent = this.Parent
            };
        }

        protected override string BlockName() { return "Registry block"; }

        protected override string Required() { return string.Format(this.reqFormat, "1 variable, 1 action"); }

        protected override string Optional() { return string.Format(this.optFormat, "1 boolean"); }

        public MyUseRegistry.ACTION Action
        {
            get { return this.Root.Action; }
            set
            {
                this.Root.Action = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool Delay
        {
            get { return this.Root.Delay; }
            set
            {
                this.Root.Delay = value;
                this.NotifyPropertyChanged();
            }
        }

        public IList<MyUseRegistry.ACTION> Actions
        {
            get { return EnumUtil.EnumToList<MyUseRegistry.ACTION>(); }
        }

        public IList<string> AliasOptions
        {
            get { return this.Parent.Aliases; }
        }

        public override void FullRefresh()
        {
            this.NotifyPropertyChanged("AliasOptions");
        }
    }
}
