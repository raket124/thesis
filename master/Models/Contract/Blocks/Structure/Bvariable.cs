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

        protected TYPES Type;
        protected string objectName;
        protected string alias;

        public Bvariable() : base()
        {

        }
    }
}
