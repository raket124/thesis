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
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        [DataMember]
        protected RELATION relation;
        public RELATION Relation
        {
            get { return this.relation; }
            set { this.relation = value; }
        }
        [DataMember]
        protected bool isList;
        public bool List
        {
            get { return this.isList; }
            set { this.isList = value; }
        }
        [DataMember]
        protected bool isOptional;
        public bool Optional
        {
            get { return this.isOptional; }
            set { this.isOptional = value; }
        }
        [DataMember]
        protected string defaultValue;
        public string Default
        {
            get { return this.defaultValue; }
            set { this.defaultValue = value; }
        }
        [DataMember]
        protected string regex;
        public string Regex
        {
            get { return this.regex; }
            set { this.regex = value; }
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
