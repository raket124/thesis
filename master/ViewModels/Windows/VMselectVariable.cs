using master.Basis;
using master.Models.Data;
using master.Models.Data.Component.Components;
using master.Models.Variables;
using master.ViewModels.Data;
using master.ViewModels.Variables;
using master.Windows;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Windows
{
    class VMselectVariable : MyBindableBase
    {
        private SelectVariableWIndow parent;
        private VMdataModel model;

        private VMvariableListing vars;
        public VMvariableListing Vars
        {
            get { return this.vars; }
        }
        private ObservableCollection<Tuple<string, string>> propertyList;
        public ObservableCollection<Tuple<string, string>> PropertyList
        {
            get { return this.propertyList; }
        }
        private ObservableCollection<Tuple<string, string>> propertyOptions;
        public ObservableCollection<Tuple<string, string>> PropertyOptions
        {
            get { return this.propertyOptions; }
        }

        public DelegateCommand<object> CommandTreeSelectionChanged { get; private set; }
        public DelegateCommand<object> CommandListSelectionChanged { get; private set; }
        public DelegateCommand CommandRemoveProperty { get; private set; }
        public DelegateCommand CommandConfirm { get; private set; }

        public string Variable
        {
            get
            {
                return string.Join(".", from p in this.propertyList
                                        select p.Item1);
            }
        }

        public VMselectVariable(SelectVariableWIndow parent, DataModel model) : base()
        {
            this.parent = parent;
            this.model = new VMdataModel(model);


            var objectGroup = new List<ObjectGroup>()
            {
                new ObjectGroup()
                {
                    Type = typeof(MyAsset),
                    Objects = this.model.GetObjectList<MyAsset>()
                },
                new ObjectGroup()
                {
                    Type = typeof(MyConcept),
                    Objects = this.model.GetObjectList<MyConcept>()
                },
                new ObjectGroup()
                {
                    Type = typeof(MyEnum),
                    Objects = this.model.GetObjectListEnum()
                },
                new ObjectGroup()
                {
                    Type = typeof(MyEvent),
                    Objects = this.model.GetObjectList<MyEvent>()
                },
                new ObjectGroup()
                {
                    Type = typeof(MyParticipant),
                    Objects = this.model.GetObjectList<MyParticipant>()
                },
                new ObjectGroup()
                {
                    Type = typeof(MyTransaction),
                    Objects = this.model.GetObjectList<MyTransaction>()
                }
            };
            objectGroup[0].Objects[0].Aliases.Add("A");


            this.vars = new VMvariableListing(new VariableListing()
            {
                VariableTypes = new List<VariableGroup>()
                {
                    new VariableGroup() { Type = typeof(string) },
                    new VariableGroup() { Type = typeof(double) },
                    new VariableGroup() { Type = typeof(int) },
                    new VariableGroup() { Type = typeof(long) },
                    new VariableGroup() { Type = typeof(DateTime) },
                    new VariableGroup() { Type = typeof(bool) },
                },
                ObjectTypes = objectGroup
            });
            this.propertyList = new ObservableCollection<Tuple<string, string>>();
            this.propertyOptions = new ObservableCollection<Tuple<string, string>>();

            this.CommandTreeSelectionChanged = new DelegateCommand<object>(this.TreeSelectionChanged);
            this.CommandListSelectionChanged = new DelegateCommand<object>(this.ListSelectionChanged);
            this.CommandRemoveProperty = new DelegateCommand(this.RemoveProperty, this.CanRemoveProperty);
            this.CommandConfirm = new DelegateCommand(this.Confirm, this.CanConfirm);
        }

        private void TreeSelectionChanged(object input)
        {
            this.propertyList.Clear();

            if (input.GetType() == typeof(VMvariableGroupAlias))
            {
                var value = input as VMvariableGroupAlias;
                this.propertyList.Add(Tuple.Create(value.Root, value.Parent.Type));
            }
            if (input.GetType() == typeof(VMobjectValueAlias))
            {
                var value = input as VMobjectValueAlias;
                this.propertyList.Add(Tuple.Create(value.Root, value.Parent.Name));
                this.PopulatePropertyList();
            }

            if (input.GetType() == typeof(VMvariableGroup))
            {
                var value = input as VMvariableGroup;
                this.propertyList.Add(Tuple.Create(value.Type, value.Type));
            }
            if (input.GetType() == typeof(VMobjectValue))
            {
                var value = input as VMobjectValue;
                this.propertyList.Add(Tuple.Create(value.Name, value.Name));
            }

            this.CommandRemoveProperty.RaiseCanExecuteChanged();
            this.CommandConfirm.RaiseCanExecuteChanged();
        }

        private void ListSelectionChanged(object input)
        {
            if (input == null)
                return;

            this.propertyList.Add(input as Tuple<string, string>);
            this.PopulatePropertyList();
            this.CommandRemoveProperty.RaiseCanExecuteChanged();
            this.CommandConfirm.RaiseCanExecuteChanged();
        }

        private void RemoveProperty()
        {
            this.propertyList.RemoveAt(this.propertyList.Count - 1);
            this.PopulatePropertyList();
            this.CommandRemoveProperty.RaiseCanExecuteChanged();
            this.CommandConfirm.RaiseCanExecuteChanged();
        }

        private bool CanRemoveProperty()
        {
            return this.propertyList.Count > 1;
        }

        private void Confirm()
        {
            this.parent.DialogResult = true;
            this.parent.Close();
        }

        private bool CanConfirm()
        {
            return this.propertyList.Count > 0;
        }

        private void PopulatePropertyList()
        {
            this.propertyOptions.Clear();

            var last = this.propertyList.Last();
            foreach (var ot in this.vars.Root.ObjectTypes)
            {
                var current = ot.Objects.Where(o => o.Name == last.Item2);
                if (current.Count() > 0)
                    this.propertyOptions.AddRange(from p in current.First().Properties
                                                  select Tuple.Create(p.Name, p.Type));
            }
        }
    }
}
