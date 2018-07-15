using master.Basis;
using master.Models.Contract.Block;
using master.Models.Data;
using master.Models.Data.Component;
using master.Models.Data.Component.Components;
using master.ViewModels.Contract.Block;
using master.ViewModels.Data;
using master.ViewModels.Variables;
using master.Windows;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace master.ViewModels.Windows
{
    class VMselectVariable : MyBindableBase
    {
        private SelectVariableWindow parent;
        public SelectVariableWindow Parent
        {
            get { return this.parent; }
        }

        private VMvariableList list;
        public VMvariableList List
        {
            get { return this.list; }
        }
        private VMdataModel model;
        public VMdataModel Model
        {
            get { return this.model; }
        }
        private VMvariableLink output;
        public VMvariableLink Output
        {
            get { return this.output; }
        }
        private ObservableCollection<Contract.Block.VMvariable> properties;
        public ObservableCollection<Contract.Block.VMvariable> Properties
        {
            get { return this.properties; }
        }

        public DelegateCommand<object> CommandTreeSelectionChanged { get; private set; }
        public DelegateCommand<object> CommandListSelectionChanged { get; private set; }
        public DelegateCommand CommandRemoveProperty { get; private set; }
        public DelegateCommand CommandConfirm { get; private set; }

        public VMselectVariable(SelectVariableWindow parent, VMvariableList variables, VMdataModel model) : base()
        {
            this.parent = parent;
            this.list = variables;
            this.model = model;
            this.output = new VMvariableLink(new VariableLink(new Models.Contract.Block.Variable(typeof(Nullable))));
            this.properties = new ObservableCollection<Contract.Block.VMvariable>();

            this.CommandTreeSelectionChanged = new DelegateCommand<object>(this.TreeSelectionChanged);
            this.CommandListSelectionChanged = new DelegateCommand<object>(this.ListSelectionChanged);
            this.CommandRemoveProperty = new DelegateCommand(this.RemoveProperty, this.CanRemoveProperty);
            this.CommandConfirm = new DelegateCommand(this.Confirm, this.CanConfirm);
        }

        private void TreeSelectionChanged(object input)
        {
            this.Output.Clear();
            if (input.GetType() == typeof(Contract.Block.VMvariable))
            {
                this.Output.Value = input as Contract.Block.VMvariable;
                this.PopulatePropertyList();
            }

            this.CommandRemoveProperty.RaiseCanExecuteChanged();
            this.CommandConfirm.RaiseCanExecuteChanged();
        }

        private void ListSelectionChanged(object input)
        {
            if (input == null)
                return;

            this.Output.AddLast(input as Contract.Block.VMvariable);
            this.PopulatePropertyList();
            this.CommandRemoveProperty.RaiseCanExecuteChanged();
            this.CommandConfirm.RaiseCanExecuteChanged();
        }

        private void RemoveProperty()
        {
            this.Output.RemoveLast();

            //this.propertyList.RemoveAt(this.propertyList.Count - 1);
            //this.PopulatePropertyList();
            //this.CommandRemoveProperty.RaiseCanExecuteChanged();
            //this.CommandConfirm.RaiseCanExecuteChanged();
        }

        private bool CanRemoveProperty()
        {
            //return this.propertyList.Count > 1;
            return true;
        }

        private void Confirm()
        {
            this.parent.DialogResult = true;
            this.parent.Close();
        }

        private bool CanConfirm()
        {
            //return this.propertyList.Count > 0;
            return true;
        }

        private void PopulatePropertyList()
        {
            this.Properties.Clear();
            Contract.Block.VMvariable current;
            if (this.Output.Value.Type == typeof(Nullable)) // Root is not set
                return;
            if (this.Output.Listing.Count == 0)             // No children
                current = this.Output.Value;
            else                                            // Last child
                current = this.Output.Listing.Last();


            if (current.ObjectName == string.Empty)
                this.PopulatePropertyListVariables(current);
            else
                this.PopulatePropertyListObjects(current);
        }

        private void PopulatePropertyListObjects(Contract.Block.VMvariable input)
        {
            var varList = this.Model.GetComponents();
            var group = varList.Where(vl => vl.Name == input.ObjectName).First();
            if (group.GetType() == typeof(MyEnum))
                return;

            var variables = new List<Contract.Block.VMvariable>();
            foreach(var x in (group as Inheritance).Components)
                variables.Add(new Contract.Block.VMvariable(new Models.Contract.Block.Variable(x, group.GetType())));

            this.Properties.AddRange(variables);
        }

        private void PopulatePropertyListVariables(Contract.Block.VMvariable input)
        {
            //Add special properties?
        }
    }
}
