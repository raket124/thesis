using GraphX.PCL.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Graphs
{
    public class DataVertex : VertexBase
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
