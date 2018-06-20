using master.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Data
{
    [DataContract]
    public class Variable : ObjectBase
    {
        public enum RELATION { variable, reference };

        [DataMember]
        protected string type;
        [DataMember]
        protected RELATION relation;
        [DataMember]
        protected bool isList;
        [DataMember]
        protected bool isOptional;
        [DataMember]
        protected string defaultValue;
        [DataMember]
        protected string regex;

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public RELATION Relation
        {
            get { return this.relation; }
            set { this.relation = value; }
        }

        public bool List
        {
            get { return this.isList; }
            set { this.isList = value; }
        }

        public bool Optional
        {
            get { return this.isOptional; }
            set { this.isOptional = value; }
        }
        
        public Variable(string type, string name, RELATION relation) : base()
        {
            this.type = type;
            this.name = name;
            this.relation = relation;
            this.isList = false;
            this.isOptional = false;
            this.defaultValue = string.Empty;
            this.regex = string.Empty;
        }
    }
}
