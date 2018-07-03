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
    class VMvariable : MyBindableBase
    {
        protected VMinput parent;
        public VMinput Parent
        {
            get { return this.parent; }
        }
        protected Variable root;
        public Variable Root
        {
            get { return this.root; }
        }

        public DelegateCommand<object> CommandRemove { get; private set; }

        public VMvariable(Variable root, VMinput parent)
        {
            this.root = root;
            this.parent = parent;

            this.CommandRemove = new DelegateCommand<object>(this.Remove);
        }

        public void Remove(object input)
        {
            this.parent.Root.Vars.Remove((input as VMvariable).Root);
        }

        public Variable.TYPES Type
        {
            get { return this.root.Type; }
            set
            {
                var old_dic = this.Parent.Parent.Variables;
                this.root.Type = value;

                if (this.IsObject)
                {
                    this.Root.ObjectName = old_dic[Variable.TYPES_DICT[value].Item1].Keys.First();
                }
                else
                {
                    this.Root.ObjectName = "Listing";
                }


                
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged("ObjectNames");
                this.NotifyPropertyChanged("IsObject");


                this.Parent.Parent.FullRefresh();
            }
        }

        public bool List
        {
            get { return this.root.List; }
            set
            {
                this.Root.List = value;
                this.NotifyPropertyChanged();
            }
        }

        public string ObjectName
        {
            get { return this.root.ObjectName; }
            set
            {
                this.Root.ObjectName = value;
                this.NotifyPropertyChanged();
            }
        }

        public List<string> ObjectNames
        {
            get
            {
                var type = Variable.TYPES_DICT[this.root.Type].Item1;
                return this.IsObject ? 
                       this.Parent.Parent.Variables[type].Keys.ToList() : 
                       new List<string>();
            }
        }

        public string Alias
        {
            get { return this.Root.Alias; }
            set
            {
                this.Root.Alias = value;
                this.NotifyPropertyChanged();
                this.Parent.Parent.FullRefresh();
            }
        }

        public bool IsObject
        {
            get { return Variable.TYPES_DICT[this.Type].Item2; }
        }

        public IList<Variable.TYPES> Types
        {
            get { return EnumUtil.EnumToList<Variable.TYPES>(); }
        }
    }
}
