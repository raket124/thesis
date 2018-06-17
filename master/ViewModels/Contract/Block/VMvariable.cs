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
    class VMvariable : VMbase
    {
        protected VMinput parent;
        public VMinput Parent
        {
            get { return this.parent; }
        }
        protected new Variable root;
        public new Variable Root
        {
            get { return this.root; }
        }

        public DelegateCommand<object> CommandRemove { get; private set; }

        ObservableCollection<string> temp;

        public VMvariable(Variable root, VMinput parent) : base(root)
        {
            this.root = root;
            this.parent = parent;

            this.CommandRemove = new DelegateCommand<object>(this.Remove);

            temp = new ObservableCollection<string>()
            {
                "LegalOwnerAdmin",
                "CompoundAdmin"
            };
        }

        public void Remove(object input)
        {
            this.parent.Root.Vars.Remove((input as VMvariable).Root);
        }

        public override object Clone()
        {
            throw new Exception("This item should not be cloned!");
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

        public IList<Variable.TYPES> Types
        {
            get { return EnumUtil.EnumToList<Variable.TYPES>(); }
        }

        public bool IsObject
        {
            get
            {
                switch (this.Type)
                {
                    case Variable.TYPES.Asset:
                    case Variable.TYPES.Concept:
                    case Variable.TYPES.Enum:
                    case Variable.TYPES.Participant:
                        return true;
                    case Variable.TYPES.String:
                    case Variable.TYPES.Double:
                    case Variable.TYPES.Integer:
                    case Variable.TYPES.Long:
                    case Variable.TYPES.DateTime:
                    case Variable.TYPES.Boolean:
                        return false;
                    default:
                        return false;
                }
            }
        }

        public ObservableCollection<string> ObjectNames
        {
            get { return this.IsObject ? this.temp : new ObservableCollection<string>(); }
            set
            {
                this.temp = value;
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

        protected override string BlockName()
        {
            return "Function input";
        }
    }
}
