using master.Models.Data.Component;
using master.Models.Data.Component.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Data
{
    [DataContract]
    class DataModel
    {
        public enum TYPES { Asset, Participant, Transaction, Event, Concept, Enum };
        protected readonly Dictionary<Type, TYPES> sorter = new Dictionary<Type, TYPES>()
        {
            { typeof(MyAsset), TYPES.Asset },
            { typeof(MyConcept), TYPES.Concept },
            { typeof(MyEnum), TYPES.Enum },
            { typeof(MyEvent), TYPES.Event },
            { typeof(MyParticipant), TYPES.Participant },
            { typeof(MyTransaction), TYPES.Transaction }
        };

        [DataMember]
        protected List<MyAsset> assetComponents;
        [DataMember]
        protected List<MyConcept> conceptComponents;
        [DataMember]
        protected List<MyEnum> enumComponents;
        [DataMember]
        protected List<MyEvent> eventComponents;
        [DataMember]
        protected List<MyParticipant> participantComponents;
        [DataMember]
        protected List<MyTransaction> transactionComponents;
        [DataMember]
        protected string fileNamespace;

        public DataModel()
        {
            this.assetComponents = new List<MyAsset>();
            this.conceptComponents = new List<MyConcept>();
            this.enumComponents = new List<MyEnum>();
            this.eventComponents = new List<MyEvent>();
            this.participantComponents = new List<MyParticipant>();
            this.transactionComponents = new List<MyTransaction>();
        }

        public string Namespace
        {
            get { return this.fileNamespace; }
            set { this.fileNamespace = value; }
        }

        public void AddComponent(Base component)
        {
            switch (this.sorter[component.GetType()])
            {
                case TYPES.Asset:
                    this.assetComponents.Add(component as MyAsset); return;
                case TYPES.Concept:
                    this.conceptComponents.Add(component as MyConcept); return;
                case TYPES.Enum:
                    this.enumComponents.Add(component as MyEnum); return;
                case TYPES.Event:
                    this.eventComponents.Add(component as MyEvent); return;
                case TYPES.Participant:
                    this.participantComponents.Add(component as MyParticipant); return;
                case TYPES.Transaction:
                    this.transactionComponents.Add(component as MyTransaction); return;
                default:
                    throw new Exception("Invalid class is provided");
            }
        }

        public List<T> GetComponent<T>()
        {
            switch (this.sorter[typeof(T)])
            {
                case TYPES.Asset:
                    return this.assetComponents as List<T>;
                case TYPES.Concept:
                    return this.conceptComponents as List<T>;
                case TYPES.Enum:
                    return enumComponents as List<T>;
                case TYPES.Event:
                    return eventComponents as List<T>;
                case TYPES.Participant:
                    return participantComponents as List<T>;
                case TYPES.Transaction:
                    return transactionComponents as List<T>;
                default:
                    throw new Exception("Invalid class is provided");
            }
        }

        public Dictionary<string, Tuple<Type, int>> GetReferenceTable()
        {
            var output = new Dictionary<string, Tuple<Type, int>>();
            this.GetReferenceTable<MyAsset>(output);
            this.GetReferenceTable<MyConcept>(output);
            this.GetReferenceTable<MyEnum>(output);
            this.GetReferenceTable<MyEvent>(output);
            this.GetReferenceTable<MyParticipant>(output);
            this.GetReferenceTable<MyTransaction>(output);
            return output;
        }

        public Dictionary<string, Tuple<Type, int>> GetReferenceTable(Dictionary<Type, bool> activeComponents)
        {
            var output = new Dictionary<string, Tuple<Type, int>>();
            this.GetReferenceTable<MyAsset>(activeComponents, output);
            this.GetReferenceTable<MyConcept>(activeComponents, output);
            this.GetReferenceTable<MyEnum>(activeComponents, output);
            this.GetReferenceTable<MyEvent>(activeComponents, output);
            this.GetReferenceTable<MyParticipant>(activeComponents, output);
            this.GetReferenceTable<MyTransaction>(activeComponents, output);
            return output;
        }

        private void GetReferenceTable<T>(Dictionary<string, Tuple<Type, int>> output) where T : Base
        {
            this.GetReferenceTable<T>(new Dictionary<Type, bool>() { { typeof(T), true } }, output);
        }

        private void GetReferenceTable<T>(Dictionary<Type, bool> addition, Dictionary<string, Tuple<Type, int>> output) where T : Base
        {
            if (!addition[typeof(T)])
                return;

            var componentList = this.GetComponent<T>();
            for (int i = 0; i < componentList.Count; i++)
                output.Add(componentList[i].Name, Tuple.Create<Type, int>(typeof(T), i));
        }
    }
}
