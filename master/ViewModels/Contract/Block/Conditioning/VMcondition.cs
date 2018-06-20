﻿using System;
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

namespace master.ViewModels.Contract.Block.Conditioning
{
    public class VMcondition : MyBindableBase, ICloneable
    {
        protected Condition root;
        public Condition Root
        {
            get { return this.root; }
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

        public DelegateCommand CommandAddCondition { get; private set; }
        public DelegateCommand CommandAddGroup { get; private set; }

        public VMcondition(Condition root)
        {
            this.root = root;
            this.Wrap();

            this.CommandAddCondition = new DelegateCommand(() => this.Root.Conditions.Add(new ConditionBase()));
            this.CommandAddGroup = new DelegateCommand(() => this.Root.Groups.Add(new ConditionGroup()));

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

        public void Wrap()
        {
            this.WrapConditions();
            this.WrapGroups();
            this.Value = new VMconditionGroup(null, null);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}