using master.Basis;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract
{
    [DataContract (IsReference = true)]
    [
        KnownType(typeof(MyError)), 
        KnownType(typeof(MyAssign)), 
        KnownType(typeof(MyInput)), 
        KnownType(typeof(MyLog)),
        KnownType(typeof(MyUseRegistry)),
    ]
    public class Function : ObjectBase
    {
        public enum ACCESSIBILITY { Public, Private, Controlled }

        [DataMember]
        protected ACCESSIBILITY access;
        public ACCESSIBILITY Access
        {
            get { return this.access; }
            set { this.access = value; }
        }
        [DataMember]
        protected ObservableCollection<Block.Base> blocks;
        public ObservableCollection<Block.Base> Blocks
        {
            get { return this.blocks; }
            set { this.blocks = value; }
        }

        public Function(string name, ACCESSIBILITY access) : base(name)
        {
            this.access = access;
            this.blocks = new ObservableCollection<Block.Base>();
        }

        public IList<Variable> Aliases
        {
            get { return this.blocks.SelectMany(x => x.Aliases).ToList(); }
        }
    }
}
