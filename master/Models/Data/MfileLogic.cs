using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    partial class Mfile
    {
        public enum TYPES { Asset, Participant, Transaction, Event, Concept, Enum };
        protected readonly Dictionary<Type, TYPES> sorter = new Dictionary<Type, TYPES>()
        {
            { typeof(Dasset), TYPES.Asset },
            { typeof(Dconcept), TYPES.Concept },
            { typeof(Denum), TYPES.Enum },
            { typeof(Devent), TYPES.Event },
            { typeof(Dparticipant), TYPES.Participant },
            { typeof(Dtransaction), TYPES.Transaction }
        };

        public Mfile(string name) : base(name)
        {
            this.assetComponents = new List<Dasset>();
            this.conceptComponents = new List<Dconcept>();
            this.enumComponents = new List<Denum>();
            this.eventComponents = new List<Devent>();
            this.participantComponents = new List<Dparticipant>();
            this.transactionComponents = new List<Dtransaction>();
        }

        public void AddComponent(Dbase component)
        {
            switch (this.sorter[component.GetType()])
            {
                case TYPES.Asset:
                    this.assetComponents.Add(component as Dasset); return;
                case TYPES.Concept:
                    this.conceptComponents.Add(component as Dconcept); return;
                case TYPES.Enum:
                    this.enumComponents.Add(component as Denum); return;
                case TYPES.Event:
                    this.eventComponents.Add(component as Devent); return;
                case TYPES.Participant:
                    this.participantComponents.Add(component as Dparticipant); return;
                case TYPES.Transaction:
                    this.transactionComponents.Add(component as Dtransaction); return;
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
            this.GetReferenceTable<Dasset>(output);
            this.GetReferenceTable<Dconcept>(output);
            this.GetReferenceTable<Denum>(output);
            this.GetReferenceTable<Devent>(output);
            this.GetReferenceTable<Dparticipant>(output);
            this.GetReferenceTable<Dtransaction>(output);
            return output;
        }

        public Dictionary<string, Tuple<Type, int>> GetReferenceTable(Dictionary<Type, bool> activeComponents)
        {
            var output = new Dictionary<string, Tuple<Type, int>>();
            this.GetReferenceTable<Dasset>(activeComponents, output);
            this.GetReferenceTable<Dconcept>(activeComponents, output);
            this.GetReferenceTable<Denum>(activeComponents, output);
            this.GetReferenceTable<Devent>(activeComponents, output);
            this.GetReferenceTable<Dparticipant>(activeComponents, output);
            this.GetReferenceTable<Dtransaction>(activeComponents, output);
            return output;
        }

        private void GetReferenceTable<T>(Dictionary<string, Tuple<Type, int>> output) where T : Dbase
        {
            this.GetReferenceTable<T>(new Dictionary<Type, bool>() { { typeof(T), true } }, output);
        }

        private void GetReferenceTable<T>(Dictionary<Type, bool> addition, Dictionary<string, Tuple<Type, int>> output) where T : Dbase
        {
            if (!addition[typeof(T)])
                return;

            var componentList = this.GetComponent<T>();
            for (int i = 0; i < componentList.Count; i++)
                output.Add(componentList[i].Name, Tuple.Create<Type, int>(typeof(T), i));
        }

        public void Test()
        {

        }
    }
}
