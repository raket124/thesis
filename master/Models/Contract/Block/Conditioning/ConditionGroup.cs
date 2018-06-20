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
    class ConditionGroup
    {
        public enum COMPARE { and, or }

        [DataMember]
        protected ObservableCollection<ConditionBase> conditions;
        public ObservableCollection<ConditionBase> Conditions
        {
            get { return this.conditions; }
        }
        [DataMember]
        protected ObservableCollection<COMPARE> connectors;
        public ObservableCollection<COMPARE> Connectors
        {
            get { return this.connectors; }
        }

        public ConditionGroup()
        {
            this.conditions = new ObservableCollection<ConditionBase>();
            this.connectors = new ObservableCollection<COMPARE>();

            this.conditions.Add(new ConditionBase());
            this.connectors.Add(COMPARE.and);
            this.conditions.Add(new ConditionBase());
        }

        public void Add(ConditionBase condition, COMPARE? comparision = null)
        {
            this.conditions.Add(condition);
            if (comparision.HasValue)
                this.connectors.Add(comparision.Value);
        }
    }
}
