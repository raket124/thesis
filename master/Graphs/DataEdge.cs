using GraphX.PCL.Common.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Graphs
{
    class DataEdge : EdgeBase<BaseVertex>
    {
        private Brush color;

        public DataEdge(BaseVertex source, BaseVertex target) : base(source, target, 1)
        {
            this.color = Brushes.Black;
        }

        public Brush Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
    }
}
