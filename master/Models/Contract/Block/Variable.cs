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
        public enum TYPES { Asset, Concept, Enum, Participant, String, Double, Integer, Long, DateTime, Boolean }
        public static readonly Dictionary<TYPES, Tuple<Type, bool>> TYPES_DICT = new Dictionary<TYPES, Tuple<Type, bool>>()
        {
            { TYPES.Asset, Tuple.Create<Type, bool>(typeof(MyAsset), true) },
            { TYPES.Concept, Tuple.Create<Type, bool>(typeof(MyConcept), true) },
            { TYPES.Enum, Tuple.Create<Type, bool>(typeof(MyEnum), true) },
            { TYPES.Participant, Tuple.Create<Type, bool>(typeof(MyParticipant), true) },
            { TYPES.String, Tuple.Create<Type, bool>(typeof(string), false) },
            { TYPES.Double, Tuple.Create<Type, bool>(typeof(double), false) },
            { TYPES.Integer, Tuple.Create<Type, bool>(typeof(int), false) },
            { TYPES.Long, Tuple.Create<Type, bool>(typeof(long), false) },
            { TYPES.DateTime, Tuple.Create<Type, bool>(typeof(DateTime), false) },
            { TYPES.Boolean, Tuple.Create<Type, bool>(typeof(bool), false) }
        };

        [DataMember]
        protected new TYPES type;
        public new TYPES Type
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

        public Variable(TYPES type) : base(string.Empty, string.Empty, RELATION.variable)
        {
            this.type = type;
            this.objectName = "Listing";
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
