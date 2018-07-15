using master.Models.Data;
using master.Models.Data.Component;
using master.Models.Data.Component.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Variables
{
    public class VariableList
    {
        protected List<VariableGroup> variableGroups;
        public List<VariableGroup> VariableGroups
        {
            get { return this.variableGroups; }
            set { this.variableGroups = value; }
        }
        protected List<ObjectGroup> objectGroups;
        public List<ObjectGroup> ObjectGroups
        {
            get { return this.objectGroups; }
            set { this.objectGroups = value; }
        }

        public VariableList()
        {
            this.variableGroups = new List<VariableGroup>()
            {
                new VariableGroup(typeof(int)),
                new VariableGroup(typeof(long)),
                new VariableGroup(typeof(double)),
                new VariableGroup(typeof(bool)),
                new VariableGroup(typeof(string)),
                new VariableGroup(typeof(DateTime))
            };
            this.objectGroups = new List<ObjectGroup>()
            {
                new ObjectGroup(typeof(MyAsset)),
                new ObjectGroup(typeof(MyConcept)),
                new ObjectGroup(typeof(MyEnum)),
                new ObjectGroup(typeof(MyEvent)),
                new ObjectGroup(typeof(MyParticipant)),
                new ObjectGroup(typeof(MyTransaction))
            };
        }

        public void ReadDataModel(DataModel model)
        {
            this.ReadDataModel<MyAsset>(model);
            this.ReadDataModel<MyConcept>(model);
            this.ReadDataModel<MyEnum>(model);
            this.ReadDataModel<MyEvent>(model);
            this.ReadDataModel<MyParticipant>(model);
            this.ReadDataModel<MyTransaction>(model);
        }

        private void ReadDataModel<T>(DataModel model) where T : Base
        {
            var group = this.objectGroups.Where(o => o.Type == typeof(T)).First();
            foreach (var component in model.GetComponent<T>())
                group.Objects.Add(new Objects(component.Name));
        }
    }
}
