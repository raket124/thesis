using GraphX.PCL.Common.Models;
using master.Models;
using master.Models.Data.Component;
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
        protected Base root;

        public BaseVertex(Base root)
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
