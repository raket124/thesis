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
    class Condition
    {
        [DataMember]
        protected ObservableCollection<ConditionBase> conditions;
        public ObservableCollection<ConditionBase> Conditions
        {
            get { return this.conditions; }
        }
        [DataMember]
        protected ObservableCollection<ConditionGroup> groups;
        public ObservableCollection<ConditionGroup> Groups
        {
            get { return this.groups; }
        }
        [DataMember]
        protected ConditionGroup value;
        public ConditionGroup Value
        {
            get { return this.value; }
        }

        public Condition()
        {
            this.conditions = new ObservableCollection<ConditionBase>();
            this.groups = new ObservableCollection<ConditionGroup>();
            this.value = new ConditionGroup();
        }
    }
}
