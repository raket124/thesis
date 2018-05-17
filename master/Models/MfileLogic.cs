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

        public List<T> getComponent<T>()
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

        public Dictionary<string, Tuple<TYPES, int>> GetReferenceTable()
        {
            var output = new Dictionary<string, Tuple<TYPES, int>>();

            for (int i = 0; i < this.assetComponents.Count; i++)
                output.Add(this.assetComponents[i].Name, Tuple.Create<TYPES, int>(TYPES.Asset, i));

            for (int i = 0; i < this.conceptComponents.Count; i++)
                output.Add(this.conceptComponents[i].Name, Tuple.Create<TYPES, int>(TYPES.Concept, i));

            for (int i = 0; i < this.enumComponents.Count; i++)
                output.Add(this.enumComponents[i].Name, Tuple.Create<TYPES, int>(TYPES.Enum, i));

            for (int i = 0; i < this.eventComponents.Count; i++)
                output.Add(this.eventComponents[i].Name, Tuple.Create<TYPES, int>(TYPES.Event, i));

            for (int i = 0; i < this.participantComponents.Count; i++)
                output.Add(this.participantComponents[i].Name, Tuple.Create<TYPES, int>(TYPES.Participant, i));

            for (int i = 0; i < this.transactionComponents.Count; i++)
                output.Add(this.transactionComponents[i].Name, Tuple.Create<TYPES, int>(TYPES.Transaction, i));

            return output;
        }
    }
}
