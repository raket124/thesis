using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Basis;
using master.Models.Contract.Block.Conditioning;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.Collections.Specialized;
using master.Models.Data;

namespace master.ViewModels.Contract.Block.Conditioning
{
    class VMcondition : MyRootedParentalBindableBase, ICloneable
    {
        public new Condition Root
        {
            get { return this.root as Condition; }
        }
        public new VMbase Parent
        {
            get { return this.parent as VMbase; }
        }

        protected ObservableCollection<VMconditionBase> conditions;
        public ObservableCollection<VMconditionBase> Conditions
        {
            get { return this.conditions; }
            set
            {
                this.conditions = value;
                this.NotifyPropertyChanged();
            }
        }
        protected ObservableCollection<VMconditionGroup> groups;
        public ObservableCollection<VMconditionGroup> Groups
        {
            get { return this.groups; }
            set
            {
                this.groups = value;
                this.NotifyPropertyChanged();
            }
        }
        protected VMconditionGroup value;
        public VMconditionGroup Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                this.NotifyPropertyChanged();
            }
        }

        public DelegateCommand CommandAddBase { get; private set; }
        public DelegateCommand CommandAddGroup { get; private set; }

        public VMcondition(Condition root, VMbase parent) : base(root, parent)
        {
            this.Wrap();

            this.CommandAddBase = new DelegateCommand(() => 
            {
                this.Root.Conditions.Add(new ConditionBase());
                this.NotifyPropertyChanged("Simple");
            });
            this.CommandAddGroup = new DelegateCommand(() =>
            {
                this.Root.Groups.Add(new ConditionGroup()
                {
                    Conditions = new ObservableCollection<string>()
                    {
                        string.Empty,
                        string.Empty
                    },
                    Connectors = new ObservableCollection<ConditionGroup.COMPARE>()
                    {
                        ConditionGroup.COMPARE.and
                    }
                });
                this.NotifyPropertyChanged("Simple");
            });

            this.Root.Conditions.CollectionChanged += new NotifyCollectionChangedEventHandler(ConditionsChanged);
            this.Root.Groups.CollectionChanged += new NotifyCollectionChangedEventHandler(GroupsChanged);
        }

        private void ConditionsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.WrapConditions();
        }

        private void GroupsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.WrapGroups();
        }

        public void WrapConditions()
        {
            this.Conditions = new ObservableCollection<VMconditionBase>(
                              from conditions in this.Root.Conditions
                              select new VMconditionBase(conditions, this));
        }
        public void WrapGroups()
        {
            this.Groups = new ObservableCollection<VMconditionGroup>(
                          from groups in this.Root.Groups
                          select new VMconditionGroup(groups, this));
        }
        public void WrapValue()
        {
            this.Value = new VMconditionGroup(this.Root.Value, this);
        }
        public void Wrap()
        {
            this.WrapConditions();
            this.WrapGroups();
            this.WrapValue();
        }

        public object Clone()
        {
            return new VMcondition(this.Root, this.Parent)
            {
                //TODO complete this
            };
        }

        public bool Simple
        {
            get { return this.Root.Conditions.Count > 1; }
        }
    }
}
