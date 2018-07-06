using master.Basis;
using master.Models.Data;
using master.Models.Data.Component.Components;
using master.Models.Variables;
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

        private VMvariableListing vars;
        public VMvariableListing Vars
        {
            get { return this.vars; }
        }
        private ObservableCollection<string> propertyList;
        public ObservableCollection<string> PropertyList
        {
            get { return this.propertyList; }
        }
        private ObservableCollection<string> propertyOptions;
        public ObservableCollection<string> PropertyOptions
        {
            get { return this.propertyOptions; }
        }

        public DelegateCommand<object> CommandTreeSelectionChanged { get; private set; }
        public DelegateCommand<object> CommandListSelectionChanged { get; private set; }
        public DelegateCommand CommandRemoveProperty { get; private set; }

        public VMselectVariable(SelectVariableWIndow parent) : base()
        {
            this.parent = parent;

            this.vars = new VMvariableListing(new VariableListing()
            {
                VariableTypes = new List<VariableGroup>()
                {
                    new VariableGroup()
                    {
                        Type = typeof(string),
                        Aliases = new List<string>()
                        {
                            "A",
                            "B"
                        }
                    },
                     new VariableGroup()
                    {
                        Type = typeof(int),
                        Aliases = new List<string>()
                        {
                            "ID",
                            "Number",
                            "PI"
                        }
                    }
                },
                ObjectTypes = new List<ObjectGroup>()
                {
                    new ObjectGroup()
                    {
                        Type = typeof(MyAsset),
                        Objects = new List<ObjectValue>()
                        {
                            new ObjectValue()
                            {
                                Name = "ECMR",
                                Aliases = new List<string>()
                                {
                                    "Var 1",
                                    "Var 2",
                                    "Var 3"
                                }
                            },
                            new ObjectValue()
                            {
                                Name = "Vehicle",
                                Aliases = new List<string>()
                                {
                                    "Var 1",
                                    "Var 2",
                                    "Var 3"
                                }
                            }
                        }
                    },
                    new ObjectGroup()
                    {
                        Type = typeof(MyParticipant),
                        Objects = new List<ObjectValue>()
                        {
                            new ObjectValue()
                            {
                                Name = "User",
                                Aliases = new List<string>()
                                {
                                    "Arjan",
                                    "Arjen",
                                    "Patrick"
                                }
                            }
                        }
                    }
                }
            });
            this.propertyList = new ObservableCollection<string>();
            this.propertyOptions = new ObservableCollection<string>();

            this.CommandTreeSelectionChanged = new DelegateCommand<object>(this.TreeSelectionChanged);
            this.CommandListSelectionChanged = new DelegateCommand<object>(this.ListSelectionChanged);
            this.CommandRemoveProperty = new DelegateCommand(this.RemoveProperty, this.CanRemoveProperty);
        }

        private void TreeSelectionChanged(object input)
        {
            this.propertyList.Clear();

            if (input.GetType() == typeof(VMvariableGroupAlias))
            {
                var value = input as VMvariableGroupAlias;
                this.Variable(value);
                this.propertyList.Add(value.Root);
            }
            if (input.GetType() == typeof(VMobjectValueAlias))
            {
                var value = input as VMobjectValueAlias;
                this.Object(value);
                this.propertyList.Add(value.Root);
            }
            if (input.GetType() == typeof(VMvariableGroup))
            {
                var value = input as VMvariableGroup;
                this.NewVariable(value);
                this.propertyList.Add(value.Type);
            }
            if (input.GetType() == typeof(VMobjectValue))
            {
                var value = input as VMobjectValue;
                this.NewObject(value);
                this.propertyList.Add(value.Name);
            }
        }

        private void ListSelectionChanged(object input)
        {
            if (input == null)
                return;
            this.propertyList.Add(input as string);

            this.CommandRemoveProperty.RaiseCanExecuteChanged();

            //TODO retrieve new property list
        }

        private void RemoveProperty()
        {
            this.propertyList.RemoveAt(this.propertyList.Count - 1);

            this.CommandRemoveProperty.RaiseCanExecuteChanged();

            //TODO retrieve new property list
        }

        private bool CanRemoveProperty()
        {
            return this.propertyList.Count > 1;
        }

        private void Variable(VMvariableGroupAlias input)
        {
            this.propertyOptions.Clear();
        }

        private void Object(VMobjectValueAlias input)
        {
            this.propertyOptions.Clear();
            this.propertyOptions.Add("Property 1");
            this.propertyOptions.Add("Property 2");
            this.propertyOptions.Add("Property 3");
            this.propertyOptions.Add("Property 4");
        }

        private void NewVariable(VMvariableGroup input)
        {
            this.propertyOptions.Clear();
        }

        private void NewObject(VMobjectValue input)
        {
            this.propertyOptions.Clear();
        }
    }
}
