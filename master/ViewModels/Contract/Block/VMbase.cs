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
using master.ViewModels.Variables;
using Prism.Commands;
using System.Windows;

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

        public DelegateCommand CommandOpen { get; protected set; }
        public DelegateCommand CommandDelete { get; protected set; }

        public VMbase(Base root, VMfunction parent) : base(root, parent)
        {
            this.CommandDelete = new DelegateCommand(() => this.Parent.Blocks.Remove(this));
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

        protected virtual List<VMvariable> GetVariables()
        {
            return new List<VMvariable>();
        }

        public List<VMvariable> Variables
        {
            get { return this.GetVariables(); }
        }
    }
}
