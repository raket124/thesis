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
        protected CATEGORY objectCategory;
        protected string objectName;
        protected string objectNameSpace;
    }
}
