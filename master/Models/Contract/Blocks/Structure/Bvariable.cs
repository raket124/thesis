using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Bvariable : Bbase
    {
        public enum TYPES { Asset, Concept, Enum, Participant, String, Double, Integer, Long, DateTime, Boolean }

        protected TYPES type;
        public TYPES Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        protected string objectName;
        public string ObjectName
        {
            get { return this.objectName; }
            set { this.objectName = value; }
        }
        protected string alias;
        public string Alias
        {
            get { return this.alias; }
            set { this.alias = value; }
        }

        public Bvariable() : base()
        {
            this.type = TYPES.Asset;
            this.objectName = string.Empty;
            this.alias = string.Empty;

        }
    }
}
