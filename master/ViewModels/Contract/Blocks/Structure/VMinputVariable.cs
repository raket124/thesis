using master.Models;
using master.Utils;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMinputVariable : VMBbase
    {
        protected VMinput parent;
        protected new Bvariable root;
        public new Bvariable Root
        {
            get { return this.root; }
            set
            {
                this.root = value;
                base.root = value;
            }
        }

        public DelegateCommand<object> CommandRemove { get; private set; }

        ObservableCollection<string> temp;

        public VMinputVariable(Bvariable root, VMinput parent) : base(root)
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
            this.parent.Root.Vars.Remove((input as VMinputVariable).Root);
        }

        public override object Clone()
        {
            throw new Exception("This item should not be cloned!");
        }

        public Bvariable.TYPES Type
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

        public IList<Bvariable.TYPES> Types
        {
            get { return EnumUtil.EnumToList<Bvariable.TYPES>(); }
        }

        public bool IsObject
        {
            get
            {
                switch (this.Type)
                {
                    case Bvariable.TYPES.Asset:
                    case Bvariable.TYPES.Concept:
                    case Bvariable.TYPES.Enum:
                    case Bvariable.TYPES.Participant:
                        return true;
                    case Bvariable.TYPES.String:
                    case Bvariable.TYPES.Double:
                    case Bvariable.TYPES.Integer:
                    case Bvariable.TYPES.Long:
                    case Bvariable.TYPES.DateTime:
                    case Bvariable.TYPES.Boolean:
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
