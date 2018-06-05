using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class BuseRegistry : Bbase
    {
        public enum ACTION { Add, Update, Remove };

        [DataMember]
        protected ACTION action;
        [DataMember]
        protected string objectVariable;

        //TODO add error catching as variable
    }
}
