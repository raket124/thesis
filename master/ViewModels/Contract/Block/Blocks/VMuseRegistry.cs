using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Basis;
using master.Models.Contract.Block.Blocks;
using master.Utils;
using master.Models.Data.Component.Components;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMuseRegistry : VMbase
    {
        public new MyRegistry Root
        {
            get { return this.root as MyRegistry; }
        }

        public VMuseRegistry(MyRegistry root, VMfunction parent) : base(root, parent)
        {

        }

        public override object Clone()
        {
            return new VMuseRegistry(this.Root.Clone() as MyRegistry, this.Parent);
        }

        protected override string BlockName() { return "Registry block"; }
        protected override string Required() { return string.Format(this.reqFormat, "1 variable, 1 action"); }
        protected override string Optional() { return string.Format(this.optFormat, "1 boolean"); }

        public MyRegistry.ACTION Action
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

        public IList<MyRegistry.ACTION> Actions
        {
            get { return EnumUtil.EnumToList<MyRegistry.ACTION>(); }
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

        public IList<string> AliasOptions
        {
            get
            {
                var vars = this.Parent.VariableList;
                var possibleVars = vars.ObjectGroups.Where(og => og.Type == typeof(MyAsset) || og.Type == typeof(MyParticipant));
                var output = possibleVars.SelectMany(pv => pv.Objects.SelectMany(o => o.Variables.Select(v => v.Alias)));
                return output.ToList();
            }
        }

        public override void FullRefresh()
        {
            this.NotifyPropertyChanged("Alias");
            this.NotifyPropertyChanged("AliasOptions");
            this.NotifyPropertyChanged("Action");
            this.NotifyPropertyChanged("Delay");
        }
    }
}
