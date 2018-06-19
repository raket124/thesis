﻿using master.Models;
using master.Models.Contract.Block.Blocks;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMinput : VMbase
    {
        protected new MyInput root;
        public new MyInput Root
        {
            get { return this.root; }
        }
        private ObservableCollection<VMvariable> vars;
        public ObservableCollection<VMvariable> Vars
        {
            get { return this.vars; }
            set
            {
                this.vars = value;
                this.NotifyPropertyChanged();
            }
        }

        public DelegateCommand CommandAdd { get; private set; }

        public VMinput(MyInput root) : base(root)
        {
            this.root = root;
            this.WrapVars();

            this.CommandAdd = new DelegateCommand(this.Add);
            this.Root.Vars.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.WrapVars();
        }

        private void WrapVars()
        {
            this.Vars = new ObservableCollection<VMvariable>(
                        from var in this.root.Vars
                        select new VMvariable(var, this));
        }


        public void Add()
        {
            this.Root.Vars.Add(new Models.Contract.Block.Variable());
        }

        public override object Clone()
        {
            return new VMinput(this.root.Clone() as MyInput);
        }

        protected override string BlockName()
        {
            return "Input block";
        }
    }
}