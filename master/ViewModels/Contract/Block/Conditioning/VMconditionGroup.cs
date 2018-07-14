using master.Basis;
using master.Models.Contract.Block.Conditioning;
using master.Utils;
using master.ViewModels.BaseTypes;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Conditioning
{
    class VMconditionGroup : MyRootedParentalBindableBase, ICloneable
    {
        public new ConditionGroup Root
        {
            get { return this.root as ConditionGroup; }
        }
        public new VMcondition Parent
        {
            get { return this.parent as VMcondition; }
        }

        public List<VMstring> conditions;
        public List<VMconditionGroupCompare> connectors;

        public DelegateCommand CommandAdd { get; private set; }
        public DelegateCommand CommandRemove { get; private set; }

        public VMconditionGroup(ConditionGroup root, VMcondition parent) : base(root, parent)
        {
            this.Wrap();

            this.CommandAdd = new DelegateCommand(this.Add);
            this.CommandRemove = new DelegateCommand(() => this.Parent.Root.Groups.Remove(this.Root));

            this.Root.Conditions.CollectionChanged += new NotifyCollectionChangedEventHandler(this.CollectionChanged);
            this.Root.Connectors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Wrap();
        }

        public void Wrap()
        {
            this.conditions = new List<VMstring>(from condition in this.Root.Conditions
                                                 select new VMstring(condition));
            this.connectors = new List<VMconditionGroupCompare>(from compare in this.Root.Connectors
                                                                select new VMconditionGroupCompare(compare));
        }

        public void Add()
        {
            this.Root.Conditions.Add(string.Empty);
            this.Root.Connectors.Add(ConditionGroup.COMPARE.and);
            this.NotifyPropertyChanged("ConditionSet");
        }

        public void Remove()
        {
            this.Root.Conditions.RemoveAt(this.Root.Conditions.Count - 1);
            this.Root.Connectors.RemoveAt(this.Root.Connectors.Count - 1);
            this.NotifyPropertyChanged("ConditionSet");
        }

        public bool CanRemove()
        {
            return this.Root.Connectors.Count > 1;
        }

        public object Clone()
        {
            return new VMconditionGroup(this.Root.Clone() as ConditionGroup, this.Parent);
            //TODO set parent for other vms

        }

        public string Alias
        {
            get { return this.Root.Alias; }
            set
            {
                this.Root.Alias = value;
                this.NotifyPropertyChanged();
                this.Parent.RenewAliasList();
            }
        }

        public IList<object> ConditionSet
        {
            get { return EnumerableUtil.Interleave(this.conditions, this.connectors).ToList(); }
        }
    }
}
