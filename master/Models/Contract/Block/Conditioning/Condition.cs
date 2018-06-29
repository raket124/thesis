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
            this.conditions = new ObservableCollection<ConditionBase>()
            {
                new ConditionBase()
            };
            this.groups = new ObservableCollection<ConditionGroup>();
            this.value = new ConditionGroup();



            //this.conditions.Add(new ConditionBase()
            //{
            //    Alias = "A",
            //    Comparison = ConditionBase.COMPARE.equal,
            //    LHS = "Variable 1",
            //    RHS = "Variable 2"
            //});

            //this.conditions.Add(new ConditionBase()
            //{
            //    Alias = "B",
            //    Comparison = ConditionBase.COMPARE.equal,
            //    LHS = "Variable 3",
            //    RHS = "Variable 4"
            //});


            //this.conditions.Add(new ConditionBase()
            //{
            //    Alias = "C",
            //    Comparison = ConditionBase.COMPARE.equal,
            //    LHS = "Variable 1",
            //    RHS = "Variable 4"
            //});

            //this.groups.Add(new ConditionGroup()
            //{
            //    Alias = "1",
            //    Conditions = new ObservableCollection<string>()
            //    {
            //        "A",
            //        "B"
            //    },
            //    Connectors = new ObservableCollection<ConditionGroup.COMPARE>()
            //    {
            //        ConditionGroup.COMPARE.and
            //    }
            //});

            //this.value = new ConditionGroup()
            //{
            //    Conditions = new ObservableCollection<string>()
            //    {
            //        "1",
            //        "C"
            //    },
            //    Connectors = new ObservableCollection<ConditionGroup.COMPARE>()
            //    {
            //        ConditionGroup.COMPARE.or
            //    }
            //};
        }
    }
}
