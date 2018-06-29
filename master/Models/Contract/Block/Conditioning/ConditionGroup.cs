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
        protected ObservableCollection<string> conditions;
        public ObservableCollection<string> Conditions
        {
            get { return this.conditions; }
            set { this.conditions = value; }
        }
        [DataMember]
        protected ObservableCollection<COMPARE> connectors;
        public ObservableCollection<COMPARE> Connectors
        {
            get { return this.connectors; }
            set { this.connectors = value; }
        }
        [DataMember]
        protected string alias;
        public string Alias
        {
            get { return this.alias; }
            set { this.alias = value; }
        }

        public ConditionGroup()
        {
            this.conditions = new ObservableCollection<string>()
            {
                string.Empty,
                string.Empty
            };
            this.connectors = new ObservableCollection<COMPARE>()
            {
                COMPARE.and
            };
            this.alias = string.Empty;
        }
    }
}
