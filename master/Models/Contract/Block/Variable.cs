using master.Basis;
using master.Models.Data.Component.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block
{
    [DataContract]
    public class Variable : Data.Variable, ICloneable
    {
        [DataMember]
        protected new Type type;
        public new Type Type
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

        public Variable(Type type) : base(string.Empty, string.Empty, RELATION.variable)
        {
            this.type = type;
            this.objectName = string.Empty;
            this.alias = string.Empty;
            this.isList = false;
        }

        public Variable(Data.Variable var, Type type) : base(var.Type, var.Name, var.Relation)
        {
            this.type = type;
            this.objectName = var.Name;
            this.alias = string.Empty;
            this.isList = false;
        }

        public object Clone()
        {
            return new Variable(this.Type)
            {
                Name = this.Name,
                Docs = this.Docs,
                Type = this.Type,
                Relation = this.Relation,
                List = this.List,
                Optional = this.Optional,
                Default = this.Default,
                Regex = this.Regex,
                ObjectName = this.ObjectName,
                Alias = this.Alias,
            };
        }
    }
}
