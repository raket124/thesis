using master.Basis;
using master.Models;
using master.Models.Contract.Block;
using master.Utils;
using master.ViewModels.Contract.Block.Blocks;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block
{
    class VMvariable : MyRootedBindableBase
    {
        public new Variable Root
        {
            get { return this.root as Variable; }
        }

        public VMvariable(Variable root) : base(root)
        {
            this.root = root;
        }

        public Type Type
        {
            get { return this.Root.Type; }
            set
            {
                this.Root.Type = value;
                this.NotifyPropertyChanged();
            }
        }

        public string ObjectName
        {
            get { return this.Root.ObjectName; }
            set
            {
                this.Root.ObjectName = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Alias
        {
            get { return this.Root.Alias; }
            set
            {
                this.Root.Alias = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool Ref
        {
            get { return this.Root.Relation == Models.Data.Variable.RELATION.reference; }
            set
            {
                this.Root.Relation = value ? Models.Data.Variable.RELATION.reference : Models.Data.Variable.RELATION.variable;
                this.NotifyPropertyChanged();
            }
        }

        public bool List
        {
            get { return this.Root.List; }
            set
            {
                this.Root.List = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}
