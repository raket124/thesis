using master.Models;
using master.Views;
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

namespace master.ViewModels
{
    class VMcontracts : MyBindableBase
    {
        private Ccontracts root;
        public Ccontracts Root
        {
            get { return this.root; }
        }
        private ObservableCollection<VMcontract> contracts;
        public ObservableCollection<VMcontract> Contracts
        {
            get { return this.contracts; }
            set
            {
                this.contracts = value;
                this.NotifyPropertyChanged();
            }
        }

        public DelegateCommand<object> CommandDelete { get; private set; }
        public DelegateCommand CommandAddGroup { get; private set; }
        public DelegateCommand<object> CommandAddContract { get; private set; }
        public DelegateCommand<object> CommandSelectionChanged { get; private set; }

        public VMcontracts(Ccontracts root)
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
            this.Contracts = new ObservableCollection<VMcontract>((
                             from contract in this.root.Contracts
                             select new VMcontract(contract, this)));
        }

        private void AddGroup()
        {
            var nameWindow = new GroupNameWindow("Group name:", new List<string>());
            if(nameWindow.ShowDialog() == true)
                this.Root.Contracts.Add(new Ccontract(nameWindow.Answer));
        }

        private void AddContract(object input)
        {
            var nameWindow = new GroupNameWindow("Contract name:", new List<string>());
            if (nameWindow.ShowDialog() == true)
                (input as VMcontract).Root.Functions.Add(new Cfunction(nameWindow.Answer, Cfunction.ACCESSIBILITY.Controlled));
        }

        private bool CanAddContract(object input)
        {
            return input != null && input.GetType() == typeof(VMcontract);
        }

        private void Remove(object input)
        {
            if (input.GetType() == typeof(VMfunction))
            {
                var function = input as VMfunction;
                function.Parent.Root.Functions.Remove(function.Root);
            }

            if (input.GetType() == typeof(VMcontract))
                this.Root.Contracts.Remove((input as VMcontract).Root);
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
                this.CLB.ListBox.ItemsSource = (input as VMfunction).Blocks;
            if (input.GetType() == typeof(VMcontract))
                this.CLB.ListBox.ItemsSource = (input as VMcontract).Functions;
        }

        public ContractListBox CLB { get; set; }
    }
}
