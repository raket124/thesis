using GraphX.PCL.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Graphs
{
    public class DataEdge : EdgeBase<BaseVertex>
    {
        public DataEdge(BaseVertex source, BaseVertex target, double weight = 1) : base(source, target, weight)
        {
        }
    }
}
