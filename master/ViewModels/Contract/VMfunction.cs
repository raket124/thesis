using GongSolutions.Wpf.DragDrop;
using master.Basis;
using master.Models;
using master.Models.Contract;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.ViewModels.Contract.Block;
using master.ViewModels.Contract.Block.Blocks;
using Prism.Mvvm;
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
    class VMfunction : MyBindableBase, IDropTarget
    {
        private Function root;
        public Function Root
        {
            get { return this.root; }
        }
        private VMcontractModel parent;
        public VMcontractModel Parent
        {
            get { return this.parent; }
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

        public VMfunction(Function root, VMcontractModel parent)
        {
            this.root = root;
            this.parent = parent;
            this.WrapBlocks();

            this.blocks.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.root.Blocks = new ObservableCollection<Base>(
                               from vm in this.blocks
                               select vm.Root);
        }

        private void WrapBlocks()
        {
            var output = new ObservableCollection<VMbase>();
            foreach(Base block in this.root.Blocks)
            {
                if (block.GetType() == typeof(MyInput))
                    output.Add(new VMinput(block as MyInput) { Parent = this });
                if (block.GetType() == typeof(MyLog))
                    output.Add(new VMlog(block as MyLog) { Parent = this });
                if (block.GetType() == typeof(MyAssign))
                    output.Add(new VMassign(block as MyAssign) { Parent = this });
                if (block.GetType() == typeof(MyIf))
                    output.Add(new VMif(block as MyIf) { Parent = this });
                if (block.GetType() == typeof(MyUseRegistry))
                    output.Add(new VMuseRegistry(block as MyUseRegistry) { Parent = this });
                //Add new blocks here
            }
            this.Blocks = output;
        }

        public string Name
        {
            get { return this.root.Name; }
        }
        public string Docs
        {
            get { return this.root.Docs == string.Empty ? "No documentation is available." : this.root.Docs; }
        }
        public Function.ACCESSIBILITY Access
        {
            get { return this.root.Access; }
        }
        public bool IsFunction
        {
            get { return true; }
        }

        public IList<string> GetAliases()
        {
            var input = this.root.Aliases;
            var output = new List<string>();

            foreach(var variable in input)
            {
                //Prune results based on parameters
                output.Add(variable.Alias);
            }
            return output;
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
            //else // Not a copy
            //{
            //    var source = dropInfo.Data as VMbase;
            //    if (source.GetType() == typeof(VMinput))
            //        return;
            //}

            base.DragOver(dropInfo);
        }

        public override void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.TargetCollection != dropInfo.DragInfo.SourceCollection) //If copy
            {
                var source = dropInfo.Data as VMbase;
                var clone = source.Clone() as VMbase;

                clone.Parent = this;
                clone.Root.Parent = this.root;
            }
            base.Drop(dropInfo);
        }

        public override void FullRefresh()
        {
            this.NotifyPropertyChanged("Aliases");
            foreach(VMbase block in this.blocks)
                block.FullRefresh();
        }
    }
}
