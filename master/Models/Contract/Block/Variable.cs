using master.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block
{
    [DataContract]
    class Variable : ObjectBase, ICloneable
    {
        public enum TYPES { Asset, Concept, Enum, Participant, String, Double, Integer, Long, DateTime, Boolean }

        [DataMember]
        protected TYPES type;
        public TYPES Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        [DataMember]
        protected string objectName;
        public string ObjectName
        {
            get { return this.objectName; }
            set { this.objectName = value; }
        }
        [DataMember]
        protected string alias;
        public string Alias
        {
            get { return this.alias; }
            set { this.alias = value; }
        }
        [DataMember]
        protected bool isList;
        public bool List
        {
            get { return this.isList; }
            set { this.isList = value; }
        }
        [DataMember]
        protected bool isRef;
        public bool Ref
        {
            get { return this.isRef; }
            set { this.isRef = value; }
        }

        public Variable() : base()
        {
            this.type = TYPES.Asset;
            this.objectName = string.Empty;
            this.alias = string.Empty;
            this.isList = false;
            this.isRef = false;
        }

        public object Clone()
        {
            return new Variable()
            {
                Name = this.Name,
                Docs = this.Docs,
                Type = this.Type,
                ObjectName = this.ObjectName,
                Alias = this.Alias,
                List = this.List,
                Ref = this.Ref
            };
        }
    }
}
