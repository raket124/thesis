using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace master.Models
{
    [DataContract]
    class DataModel
    {
        [DataMember]
        protected object contracts;
        [DataMember]
        protected object dataModel;
        [DataMember]
        protected object authorization;

        public DataModel()
        {

        }
    }
}
