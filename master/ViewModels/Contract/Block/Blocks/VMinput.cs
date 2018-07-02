using master.Models;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Contract.Block.Blocks
{
    class VMinput : VMbase
    {
        public new MyInput Root
        {
            get { return this.root as MyInput; }
        }
        private List<VMvariable> vars;
        public List<VMvariable> Vars
        {
            get { return this.vars; }
            set
            {
                this.vars = value;
                this.NotifyPropertyChanged();
            }
        }

        public DelegateCommand CommandAdd { get; private set; }

        public VMinput(MyInput root) : base(root)
        {
            this.WrapVars();

            this.CommandAdd = new DelegateCommand(this.Add);
            this.Root.Vars.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.WrapVars();
        }

        private void WrapVars()
        {
            this.Vars = new List<VMvariable>(
                        from var in this.Root.Vars
                        select new VMvariable(var, this));
        }


        public void Add()
        {
            this.Root.Vars.Add(new Models.Contract.Block.Variable(Models.Contract.Block.Variable.TYPES.String));
            this.Parent.FullRefresh();
        }

        public override object Clone()
        {
            return new VMinput(this.root.Clone() as MyInput)
            {
                Parent = this.Parent
            };
        }

        protected override string BlockName() { return "Input block"; }

        protected override string Required() { return string.Format(this.reqFormat, "1+ variable(s)"); }

        protected override string Optional() { return string.Empty; }

        public override Dictionary<Type, Dictionary<string, List<string>>> Aliases
        {
            get
            {
                var output = new Dictionary<Type, Dictionary<string, List<string>>>();
                foreach (VMvariable var in this.vars)
                {
                    var type = Variable.TYPES_DICT[var.Type].Item1;
                    var name = var.Root.ObjectName;
                    var alias = var.Alias;

                    if (output.ContainsKey(type))
                    {
                        if(output[type].ContainsKey(name))
                        {
                            output[type][name].Add(alias);
                        }
                        else
                        {
                            output[type].Add(name, new List<string>());
                            output[type][name].Add(alias);
                        }
                    }
                    else
                    {
                        output.Add(type, new Dictionary<string, List<string>>());
                        output[type].Add(name, new List<string>());
                        output[type][name].Add(alias);
                    }
                }
                return output;
            }
        }
    }
}
