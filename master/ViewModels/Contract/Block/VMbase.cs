using master.Basis;
using master.Models;
using master.Models.Contract.Block;
using master.Models.Data;
using master.ViewModels.Windows;
using master.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block.Blocks;

namespace master.ViewModels.Contract.Block
{
    abstract class VMbase : MyRootedParentalBindableBase, ICloneable
    {
        protected readonly string reqFormat = "Requirements: {0}";
        protected readonly string optFormat = "Optional: {0}";

        public new Base Root
        {
            get { return this.root as Base; }
        }
        public new VMfunction Parent
        {
            get { return this.parent as VMfunction; }
            set { this.parent = value; }
        }

        public VMbase(Base root, VMfunction parent) : base(root, parent)
        {
        }

        public string Documentation
        {
            get { return this.Root.Docs; }
        }

        public string Name
        {
            get { return this.BlockName(); }
        }

        public string RequiredParams
        {
            get { return this.Required(); }
        }

        public string OptionalParams
        {
            get { return this.Optional(); }
        }

        public abstract object Clone();

        protected abstract string BlockName();

        protected abstract string Required();

        protected abstract string Optional();

        //public virtual Dictionary<Type, Dictionary<string, List<string>>> Aliases
        //{
        //    get { return new Dictionary<Type, Dictionary<string, List<string>>>(); }
        //}

        //public string SelectVar()
        //{
        //    var window = new SelectVariableWIndow();
        //    var vmWindow = new VMselectVariable(window, this.Parent.Parent.Parent.Parent.Model.Root);
        //    window.DataContext = vmWindow;
        //    if (window.ShowDialog() == true)
        //        return vmWindow.Variable;
        //    return string.Empty;
        //}
    }
}
