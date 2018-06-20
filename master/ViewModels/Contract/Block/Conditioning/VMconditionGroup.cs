using master.Basis;
using master.Models.Contract.Block.Conditioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Conditioning
{
    class VMconditionGroup : MyBindableBase, ICloneable
    {
        protected ConditionGroup root;
        public ConditionGroup Root
        {
            get { return this.root; }
        }

        public VMconditionGroup(ConditionGroup root)
        {
            this.root = root;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
