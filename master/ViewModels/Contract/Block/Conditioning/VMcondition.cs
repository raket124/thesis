using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Basis;
using master.Models.Contract.Block.Conditioning;

namespace master.ViewModels.Contract.Block.Conditioning
{
    class VMcondition : MyBindableBase, ICloneable
    {
        protected Condition root;
        public Condition Root
        {
            get { return this.root; }
        }

        public VMcondition(Condition root)
        {
            this.root = root;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
