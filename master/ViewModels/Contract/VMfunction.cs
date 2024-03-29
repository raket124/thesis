﻿using GongSolutions.Wpf.DragDrop;
using master.Basis;
using master.Models;
using master.Models.Contract;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Blocks.Custom;
using master.Models.Contract.Block.Combinations;
using master.Models.Variables;
using master.ViewModels.Contract.Block;
using master.ViewModels.Contract.Block.Blocks;
using master.ViewModels.Contract.Block.Blocks.Custom;
using master.ViewModels.Contract.Block.Combinations;
using master.ViewModels.Variables;
using master.ViewModels.Windows;
using master.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace master.ViewModels.Contract
{
    class VMfunction : Basis.DefaultDropHandler, IDropTarget
    {
        public new Function Root
        {
            get { return this.root as Function; }
        }
        public new VMcontractModel Parent
        {
            get { return this.parent as VMcontractModel; }
        }
        private ObservableCollection<VMbase> blocks;
        public ObservableCollection<VMbase> Blocks
        {
            get { return this.blocks; }
            set
            {
                this.blocks = value;
                this.NotifyPropertyChanged();
            }
        }

        public VMfunction(Function root, VMcontractModel parent) : base(root, parent)
        {
            this.root = root;
            this.parent = parent;
            this.WrapBlocks();

            this.blocks.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Root.Blocks = new ObservableCollection<Base>(
                               from vm in this.blocks
                               select vm.Root);
        }

        private void WrapBlocks()
        {
            var output = new ObservableCollection<VMbase>();
            foreach(Base block in this.Root.Blocks)
            {
                if (block.GetType() == typeof(MyAssign))
                    output.Add(new VMassign(block as MyAssign, this));
                if (block.GetType() == typeof(MyElse))
                    output.Add(new VMelse(block as MyElse, this));
                if (block.GetType() == typeof(MyEnd))
                    output.Add(new VMend(block as MyEnd, this));
                if (block.GetType() == typeof(MyError))
                    output.Add(new VMerror(block as MyError, this));
                if (block.GetType() == typeof(MyForeach))
                    output.Add(new VMforeach(block as MyForeach, this));
                if (block.GetType() == typeof(MyIf))
                    output.Add(new VMif(block as MyIf, this));
                if (block.GetType() == typeof(MyLog))
                    output.Add(new VMlog(block as MyLog, this));
                if (block.GetType() == typeof(MyRegistry))
                    output.Add(new VMuseRegistry(block as MyRegistry, this));

                if (block.GetType() == typeof(MyCreation))
                    output.Add(new VMcreation(block as MyCreation, this));
                if (block.GetType() == typeof(MyIfError))
                    output.Add(new VMifError(block as MyIfError, this));
                if (block.GetType() == typeof(MyInput))
                    output.Add(new VMinput(block as MyInput, this));
                if (block.GetType() == typeof(MyModification))
                    output.Add(new VMmodification(block as MyModification, this));
                if (block.GetType() == typeof(MyValidation))
                    output.Add(new VMvalidation(block as MyValidation, this));

                if (block.GetType() == typeof(MyTotalEcmrs))
                    output.Add(new VMtotalEcmrs(block as MyTotalEcmrs, this));
                //Add new blocks here
            }
            this.Blocks = output;
        }

        public string Name
        {
            get { return this.Root.Name; }
        }
        public string Docs
        {
            get { return this.Root.Docs == string.Empty ? "No documentation is available." : this.Root.Docs; }
        }
        public Function.ACCESSIBILITY Access
        {
            get { return this.Root.Access; }
        }
        public bool IsFunction
        {
            get { return true; }
        }

        public override void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.TargetCollection != dropInfo.DragInfo.SourceCollection) //If copy
            {
                var source = dropInfo.Data as VMbase;
                if (source == null)
                    return;
                if (source.GetType() == typeof(VMinput) && blocks.OfType<VMinput>().Any())
                    return;
            }
            base.DragOver(dropInfo);
        }

        public override void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.TargetCollection != dropInfo.DragInfo.SourceCollection) //If copy
            {
                var source = dropInfo.Data as VMbase;
                source.Parent = this;
            }
            base.Drop(dropInfo);
        }

        public override void FullRefresh()
        {
            foreach(VMbase block in this.blocks)
                block.FullRefresh();
        }

        public VMvariableList BasicVariableList
        {
            get
            {
                var root = new VariableList();
                root.ReadDataModel(this.Parent.Parent.Parent.Model.Root);
                return new VMvariableList(root);
            }
        }

        public VMvariableList VariableList
        {
            get
            {
                var output = this.BasicVariableList;
                foreach (var item in this.Blocks)
                    output.AddVars(item.Variables);
                return output;
            }
        }

        public VariableLink SelectVar()
        {
            var window = new SelectVariableWindow();
            var vmWindow = new VMselectVariable(window, this.VariableList, this.Parent.Parent.Parent.Model);
            window.DataContext = vmWindow;

            if (window.ShowDialog() == true)
                return vmWindow.Output.Root;
            return null;
        }
    }
}
