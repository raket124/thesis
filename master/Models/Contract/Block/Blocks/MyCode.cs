using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    class MyCode : Base
    {
        public enum OPTIONS { updateEcmrListInVin, retrieveAndUpdateVin };

        public MyCode() : base()
        {

        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
