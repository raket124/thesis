using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Mvariable : Mbase
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
        
        public Mvariable(string type, string name, RELATION relation) : base(name)
        {
            this.type = type;
            this.relation = relation;
            this.isList = false;
            this.isOptional = false;
            this.defaultValue = string.Empty;
            this.regex = string.Empty;
        }

        public override string ToString()
        {
            var output = new List<string>()
            {
                this.Relation == Mvariable.RELATION.variable ? "  o" : "-->",
                this.Type,
                this.List ? "[]" : string.Empty,
                this.Name,
                //this.defaultValue != string.Empty ? string.Format("default=", this.defaultValue) : string.Empty,
                this.Optional ? "optional" : string.Empty

            };

            return string.Join(" ", output);
        }
    }
}
