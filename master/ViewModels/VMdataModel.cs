using master.Models;
using master.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMdataModel
    {
        private DataModel root;
        private VMcontracts contracts;
        private VMmodel model;
        private object authorization;

        public VMdataModel(DataModel root)
        {
            this.root = root;
            this.contracts = new VMcontracts(this.root.Contracts);
            this.model = new VMmodel(this.root.Model);
            this.authorization = null;
        }

        public VMcontracts Contracts
        {
            get { return this.contracts; }
        }

        public VMmodel Model
        {
            get { return this.model; }
        }

        public object Authorization
        {
            get { return this.authorization; }
        }
    }
}
