using master.Basis;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Blocks.Custom;
using master.Models.Contract.Block.Combinations;
using master.Models.Variables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract
{
    [DataContract]
    [
        KnownType(typeof(MyAssign)), 
        KnownType(typeof(MyElse)), 
        KnownType(typeof(MyEnd)),
        KnownType(typeof(MyError)),
        KnownType(typeof(MyForeach)),
        KnownType(typeof(MyIf)),
        KnownType(typeof(MyLog)),
        KnownType(typeof(MyRegistry)),

        KnownType(typeof(MyTotalEcmrs)),

        KnownType(typeof(MyCreation)),
        KnownType(typeof(MyIfError)),
        KnownType(typeof(MyInput)),
        KnownType(typeof(MyModification)),
        KnownType(typeof(MyValidation)),
    ]
    public class Function : ObjectBase, ICloneable
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
        protected ObservableCollection<Base> blocks;
        public ObservableCollection<Base> Blocks
        {
            get { return this.blocks; }
            set { this.blocks = value; }
        }

        public Function(string name, ACCESSIBILITY access) : base(name)
        {
            this.access = access;
            this.blocks = new ObservableCollection<Block.Base>();
        }

        public object Clone()
        {
            return new Function(this.Name, this.Access)
            {
                Docs = this.Docs,
                Blocks = new ObservableCollection<Base>(
                         from b in this.Blocks
                         select b.Clone() as Base)
            };
        }
    }
}
