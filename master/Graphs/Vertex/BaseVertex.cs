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
    public class BaseVertex : VertexBase
    {
        private Mvariable root;
        
        public BaseVertex()
        {
            this.Vars = new List<string>();
            this.root = new Mvariable("A", "B", Mvariable.RELATION.reference);
        }

        public string Name
        {
            get { return this.root.Name; }
            set { this.root.Name = value; }
        }

        public List<string> Vars { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}]\n{1}", this.Name, string.Join("\n", this.Vars));
        }
    }
}
