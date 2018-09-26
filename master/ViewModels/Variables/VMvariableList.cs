using master.Basis;
using master.Models.Variables;
using master.ViewModels.Contract.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Variables
{
    class VMvariableList : MyRootedBindableBase
    {
        public new VariableList Root
        {
            get { return this.root as VariableList; }
        }

        public VMvariableList(VariableList root) : base(root)
        {

        }

        public IList<VMvariableGroup> VariableGroups
        {
            get
            {
                return (from vg in this.Root.VariableGroups
                        select new VMvariableGroup(vg, this)).ToList();
            }
        }

        public IList<VMobjectGroup> ObjectGroups
        {
            get
            {
                return (from og in this.Root.ObjectGroups
                        select new VMobjectGroup(og, this)).ToList();
            }
        }

        public IList<object> Groups
        {
            get
            {
                var output = new List<object>();
                output.AddRange(this.VariableGroups);
                output.AddRange(this.ObjectGroups);
                return output;
            }
        }

        public void AddVars(List<VMvariable> input)
        {
            foreach (var variable in input)
            {
                if (variable.Type == typeof(Nullable))
                    continue; //Skip dummy vars
                if(variable.ObjectName == string.Empty)
                    this.Root.VariableGroups.Where(vg => vg.Type == variable.Type).First().Variables.Add(variable.Root);
                else
                {
                    var group = this.Root.ObjectGroups.Where(og => og.Type == variable.Type).First();
                    var category = group.Objects.Where(o => o.Name == variable.ObjectName).First();
                    category.Variables.Add(variable.Root);
                }
            }
            this.NotifyPropertyChanged("VariableGroups");
            this.NotifyPropertyChanged("ObjectGroups");
            this.NotifyPropertyChanged("Groups");
        }
    }
}
