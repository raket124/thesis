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
        protected Dictionary<Type, TYPES> sorter;

        public Mfile(string name) : base(name)
        {
            this.sorter = new Dictionary<Type, TYPES>
            {
                { typeof(Masset), TYPES.Asset },
                { typeof(Mconcept), TYPES.Concept },
                { typeof(Menum), TYPES.Enum },
                { typeof(Mevent), TYPES.Event },
                { typeof(Mparticipant), TYPES.Participant },
                { typeof(Mtransaction), TYPES.Transaction }
            };
            this.assetComponents = new List<Masset>();
            this.conceptComponents = new List<Mconcept>();
            this.enumComponents = new List<Menum>();
            this.eventComponents = new List<Mevent>();
            this.participantComponents = new List<Mparticipant>();
            this.transactionComponents = new List<Mtransaction>();
        }

        public void AddComponent(Mbase component)
        {
            switch (this.sorter[component.GetType()])
            {
                case TYPES.Asset:
                    this.assetComponents.Add(component as Masset);
                    break;
                case TYPES.Concept:
                    this.conceptComponents.Add(component as Mconcept);
                    break;
                case TYPES.Enum:
                    this.enumComponents.Add(component as Menum);
                    break;
                case TYPES.Event:
                    this.eventComponents.Add(component as Mevent);
                    break;
                case TYPES.Participant:
                    this.participantComponents.Add(component as Mparticipant);
                    break;
                case TYPES.Transaction:
                    this.transactionComponents.Add(component as Mtransaction);
                    break;
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
            this.GetReferenceTable<Masset>(output);
            this.GetReferenceTable<Mconcept>(output);
            this.GetReferenceTable<Menum>(output);
            this.GetReferenceTable<Mevent>(output);
            this.GetReferenceTable<Mparticipant>(output);
            this.GetReferenceTable<Mtransaction>(output);
            return output;
        }

        public Dictionary<string, Tuple<Type, int>> GetReferenceTable(Dictionary<Type, bool> activeComponents)
        {
            var output = new Dictionary<string, Tuple<Type, int>>();
            this.GetReferenceTable<Masset>(activeComponents, output);
            this.GetReferenceTable<Mconcept>(activeComponents, output);
            this.GetReferenceTable<Menum>(activeComponents, output);
            this.GetReferenceTable<Mevent>(activeComponents, output);
            this.GetReferenceTable<Mparticipant>(activeComponents, output);
            this.GetReferenceTable<Mtransaction>(activeComponents, output);
            return output;
        }

        private void GetReferenceTable<T>(Dictionary<string, Tuple<Type, int>> output) where T : Mbase
        {
            this.GetReferenceTable<T>(new Dictionary<Type, bool>() { { typeof(T), true } }, output);
        }

        private void GetReferenceTable<T>(Dictionary<Type, bool> addition, Dictionary<string, Tuple<Type, int>> output) where T : Mbase
        {
            if (!addition[typeof(T)])
                return;

            var componentList = this.GetComponent<T>();
            for (int i = 0; i < componentList.Count; i++)
                output.Add(componentList[i].Name, Tuple.Create<Type, int>(typeof(T), i));
        }
    }
}
