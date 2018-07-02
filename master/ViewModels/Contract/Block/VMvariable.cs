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

        ObservableCollection<string> availableTypes;

        public VMvariable(Variable root, VMinput parent)
        {
            this.root = root;
            this.parent = parent;

            this.CommandRemove = new DelegateCommand<object>(this.Remove);

            availableTypes = new ObservableCollection<string>()
            {
                "LegalOwnerAdmin",
                "CompoundAdmin"
            };
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
                this.root.Type = value;
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged("ObjectNames");
                this.NotifyPropertyChanged("IsObject");
            }
        }

        public bool List
        {
            get { return this.root.List; }
            set
            {
                this.root.List = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> ObjectNames
        {
            get { return this.IsObject ? this.availableTypes : new ObservableCollection<string>(); }
            set
            {
                this.availableTypes = value;
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
                this.Parent.Parent.FullRefresh();
            }
        }

        public bool IsObject
        {
            get
            {
                return Variable.TYPES_DICT[this.Type].Item2;

                //switch (this.Type)
                //{
                //    case Variable.TYPES.Asset:
                //    case Variable.TYPES.Concept:
                //    case Variable.TYPES.Enum:
                //    case Variable.TYPES.Participant:
                //        return true;
                //    case Variable.TYPES.String:
                //    case Variable.TYPES.Double:
                //    case Variable.TYPES.Integer:
                //    case Variable.TYPES.Long:
                //    case Variable.TYPES.DateTime:
                //    case Variable.TYPES.Boolean:
                //        return false;
                //    default:
                //        return false;
                //}
            }
        }

        public IList<Variable.TYPES> Types
        {
            get { return EnumUtil.EnumToList<Variable.TYPES>(); }
        }
    }
}
