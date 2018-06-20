using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using System.Collections.ObjectModel;

namespace master.ViewModels.Contract.Block.Conditioning
{
    class VMend : VMbase
    {
        public VMend(Base root) : base(root)
        {

        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        protected override string BlockName()
        {
            return "End - block";
        }

        protected override string Required()
        {
            return string.Format(this.reqFormat, "1 \"If block\"");
        }

        protected override string Optional()
        {
            return string.Empty;
        }
    }
}
