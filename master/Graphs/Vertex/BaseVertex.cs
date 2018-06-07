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
    class BaseVertex : VertexBase
    {
        protected Dbase root;

        public BaseVertex(Dbase root)
        {
            this.root = root;
        }

        public string Name
        {
            get { return this.root.Name; }
        }

        public string Docs
        {
            get { return this.root.Docs; }
        }

        public string ClassName
        {
            get { return this.root.ClassName; }
        }
    }
}
