using GraphX.PCL.Common.Models;
using master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace master.Graphs
{
    public class DataVertex : VertexBase
    {
        public string Name { get; set; }

        public List<string> Vars { get; set; }

        public DataVertex()
        {
            this.Vars = new List<string>();
        }

        public override string ToString()
        {
            return string.Format("[{0}]\n{1}", this.Name, string.Join("\n", this.Vars));
        }
    }
}
