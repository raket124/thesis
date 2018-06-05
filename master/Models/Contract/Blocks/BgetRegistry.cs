using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class BgetRegistry : Bbase
    {
        public enum CATEGORY { Participant, Asset };

        [DataMember]
        protected CATEGORY objectCategory;
        [DataMember]
        protected string objectName;
        [DataMember]
        protected string objectNameSpace;
    }
}
