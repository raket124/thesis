using master.Basis;
using master.Models;
using master.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Data
{
    class VMdataModel : MyBindableBase
    {
        private DataModel root;

        public VMdataModel(DataModel root)
        {
            this.root = root;
        }
    }
}
