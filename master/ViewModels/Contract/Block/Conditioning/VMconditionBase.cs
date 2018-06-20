using master.Basis;
using master.Models.Contract.Block.Conditioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Conditioning
{
    class VMconditionBase : MyBindableBase, ICloneable
    {
        protected ConditionBase root;
        public ConditionBase Root
        {
            get { return this.root; }
        }

        public VMconditionBase(ConditionBase root)
        {
            this.root = root;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
