using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Conditioning
{
    [DataContract]
    class Condition : ICloneable
    {
        [DataMember]
        protected ObservableCollection<ConditionBase> conditions;
        public ObservableCollection<ConditionBase> Conditions
        {
            get { return this.conditions; }
            set { this.conditions = value; }
        }
        [DataMember]
        protected ObservableCollection<ConditionGroup> groups;
        public ObservableCollection<ConditionGroup> Groups
        {
            get { return this.groups; }
            set { this.groups = value; }
        }
        [DataMember]
        protected ConditionGroup value;
        public ConditionGroup Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Condition()
        {
            this.conditions = new ObservableCollection<ConditionBase>();
            this.groups = new ObservableCollection<ConditionGroup>();
            this.value = new ConditionGroup();
        }

        public object Clone()
        {
            return new Condition()
            {
                Conditions = new ObservableCollection<ConditionBase>(from cb in this.Conditions
                                                                     select cb.Clone() as ConditionBase),
                Groups = new ObservableCollection<ConditionGroup>(from cg in this.Groups
                                                                  select cg.Clone() as ConditionGroup),
                Value = this.Value.Clone() as ConditionGroup
            };
        }
    }
}
