using master.Basis;
using master.Models;
using master.ViewModels;
using master.ViewModels.Contract;
using master.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMmasterModel : MyBindableBase
    {
        private MasterModel root;
        public MasterModel Root
        {
            get { return this.root; }
        }
        private VMcontractCollection contracts;
        public VMcontractCollection Contracts
        {
            get { return this.contracts; }
        }
        private VMdataModel model;
        public VMdataModel Model
        {
            get { return this.model; }
        }
        private object authorization;
        public object Authorization
        {
            get { return this.authorization; }
        }

        public VMmasterModel(MasterModel root)
        {
            this.root = root;
            this.contracts = new VMcontractCollection(this.root.Contracts);
            this.model = new VMdataModel(this.root.Model);
            this.authorization = null;
        }
    }
}
