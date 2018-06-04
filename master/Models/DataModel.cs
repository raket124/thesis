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
        protected Ccontracts contracts;
        [DataMember]
        protected Dmodel model;
        [DataMember]
        protected object authorization;

        public DataModel()
        {
            //this.contracts = new Ccontracts();
            //this.dataModel = new Mfile();
            this.SetupDemo(); //Debugging only
        }

        public void SetupDemo()
        {
            this.contracts = CcontractsDemo.KoopmanContract();
            this.model = DmodelDemo.KoopmanCTO();
            this.authorization = null;
        }

        public Ccontracts Contracts
        {
            get { return this.contracts; }
        }

        public Dmodel Model
        {
            get { return this.model; }
        }

        public object Authorization
        {
            get { return this.authorization; }
        }
    }
}
