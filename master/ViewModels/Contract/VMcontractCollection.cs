using master.Models;
using master.Windows.Views;
using master.Windows;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using master.Basis;
using master.Models.Contract;
using master.ViewModels.Windows;

namespace master.ViewModels.Contract
{
    class VMcontractCollection : MyBindableBase
    {
        private ContractCollection root;
        public ContractCollection Root
        {
            get { return this.root; }
        }
        private ObservableCollection<VMcontractModel> contracts;
        public ObservableCollection<VMcontractModel> Contracts
        {
            get { return this.contracts; }
            set
            {
                this.contracts = value;
                this.NotifyPropertyChanged();
            }
        }

        public ContractListBox CLB { get; set; } //TODO fix this into a proper setter

        public DelegateCommand<object> CommandDelete { get; private set; }
        public DelegateCommand CommandAddGroup { get; private set; }
        public DelegateCommand<object> CommandAddContract { get; private set; }
        public DelegateCommand<object> CommandSelectionChanged { get; private set; }

        public VMcontractCollection(ContractCollection root)
        {
            this.root = root;
            this.WrapContracts();

            this.CommandDelete = new DelegateCommand<object>(this.Remove, this.CanRemove);
            this.CommandAddGroup = new DelegateCommand(this.AddGroup);
            this.CommandAddContract = new DelegateCommand<object>(this.AddContract, this.CanAddContract);
            this.CommandSelectionChanged = new DelegateCommand<object>(this.SelectionChanged);

            this.Root.Contracts.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.WrapContracts();
        }

        private void WrapContracts()
        {
            this.Contracts = new ObservableCollection<VMcontractModel>((
                             from contract in this.root.Contracts
                             select new VMcontractModel(contract, this)));
        }

        private void AddGroup()
        {
            var window = new NewGroupWindow();
            var vmWindow = new VMnewGroup(window, 
                                          new List<string>(
                                          from contract in this.Root.Contracts
                                          select contract.Name));
            window.DataContext = vmWindow;

            if (window.ShowDialog() == true)
                this.Root.Contracts.Add(new ContractModel(vmWindow.Name));

            //new NewFunctionWindow(new List<string>(), new List<string>()).ShowDialog();
        }

        private void AddContract(object input)
        {
            var selectedContract = (input as VMcontractModel).Root;

            var window = new NewFunctionWindow();
            var vmWindow = new VMnewFunction(window, 
                                             new List<string>(
                                             from function in selectedContract.Functions
                                             select function.Name),
                                             new List<string>()
                                             {
                                                "Fill",
                                                "This",
                                                "List"
                                             });
            window.DataContext = vmWindow;

            

            if (window.ShowDialog() == true)
                selectedContract.Functions.Add(vmWindow.Function);
        }

        private bool CanAddContract(object input)
        {
            return input != null && input.GetType() == typeof(VMcontractModel);
        }

        private void Remove(object input)
        {
            if (input.GetType() == typeof(VMfunction))
            {
                var function = input as VMfunction;
                function.Parent.Root.Functions.Remove(function.Root);
            }

            if (input.GetType() == typeof(VMcontractModel))
                this.Root.Contracts.Remove((input as VMcontractModel).Root);
        }

        private bool CanRemove(object input)
        {
            return input != null;
        }

        private void SelectionChanged(object input)
        {
            this.CommandDelete.RaiseCanExecuteChanged();
            this.CommandAddContract.RaiseCanExecuteChanged();

            if (input.GetType() == typeof(VMfunction))
                this.CLB.ListBox.DataContext = input as VMfunction;
            if (input.GetType() == typeof(VMcontractModel))
                this.CLB.ListBox.DataContext = input as VMcontractModel;
        }
    }
}
