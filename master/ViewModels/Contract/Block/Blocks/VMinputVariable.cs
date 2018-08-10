using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using Prism.Commands;
using master.Models.Data.Component.Components;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMinputVariable : VMvariable
    {
        protected VMinput parent;
        public VMinput Parent
        {
            get { return this.parent; }
        }

        public DelegateCommand<object> CommandRemove { get; private set; }

        public VMinputVariable(Variable root, VMinput parent) : base(root)
        {
            this.parent = parent;

            this.CommandRemove = new DelegateCommand<object>(this.Remove);
        }

        public void Remove(object input)
        {
            this.Parent.Root.Vars.Remove((input as VMvariable).Root);
        }

        public new Type Type
        {
            get { return base.Type; }
            set
            {
                base.Type = value;
                base.ObjectName = this.SelectableOptions ? this.Parent.Parent.BasicVariableList.ObjectGroups.Where(og => og.Type == value).First().Objects.First().Name : string.Empty;


                this.NotifyPropertyChanged("ObjectName");
                this.NotifyPropertyChanged("IsObject");
                this.NotifyPropertyChanged("TypeOptions");
                this.Parent.Parent.FullRefresh();
            }
        }

        public IList<Type> Types
        {
            get
            {
                return new List<Type>()
                {
                    typeof(int),
                    typeof(long),
                    typeof(double),
                    typeof(bool),
                    typeof(string),
                    typeof(DateTime),
                    typeof(MyAsset),
                    typeof(MyConcept),
                    typeof(MyEnum),
                    typeof(MyEvent),
                    typeof(MyParticipant),
                    typeof(MyTransaction)
                };
            }
        }

        public bool SelectableOptions
        {
            get
            {
                return IsObject &&
                       this.Parent.Parent.BasicVariableList.ObjectGroups.Where(og => og.Type == this.Type).First().Objects.Count > 0;
            }
        }

        public bool IsObject
        {
            get
            {
                return !(this.Type == typeof(int) ||
                    this.Type == typeof(long) ||
                    this.Type == typeof(double) ||
                    this.Type == typeof(bool) ||
                    this.Type == typeof(string) ||
                    this.Type == typeof(DateTime));
            }
        }

        public IList<string> TypeOptions
        {
            get
            {
                if (this.SelectableOptions)
                {
                    var vars = this.Parent.Parent.VariableList;
                    var possibleVars = vars.ObjectGroups.Where(og => og.Type == this.Type);
                    var output = possibleVars.SelectMany(pv => pv.Objects.Select(o => o.Name));
                    return output.ToList();
                } 
                else
                    return new List<string>();
            }
        }
    }
}
