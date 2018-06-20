using master.Models.Contract;
using master.Models.Data;
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
    public class MasterModel
    {
        [DataMember]
        protected ContractCollection contracts;
        public ContractCollection Contracts
        {
            get { return this.contracts; }
        }
        [DataMember]
        protected DataModel model;
        public DataModel Model
        {
            get { return this.model; }
        }
        [DataMember]
        protected object authorization;
        public object Authorization
        {
            get { return this.authorization; }
        }

        public MasterModel()
        {
            //this.contracts = new Ccontracts();
            //this.dataModel = new Mfile();
            this.SetupDemo(); //Debugging only
        }

        public void SetupDemo()
        {
            this.contracts = ContractCollectionDemo.KoopmanContract();
            this.model = DataModelDemo.KoopmanCTO();
            this.authorization = null;
        }
    }
}
