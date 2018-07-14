using master.Basis;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
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
        KnownType(typeof(MyCode)), 
        KnownType(typeof(MyElse)), 
        KnownType(typeof(MyEnd)),
        KnownType(typeof(MyError)),
        KnownType(typeof(MyGetRegistry)),
        KnownType(typeof(MyIf)),
        KnownType(typeof(MyInput)),
        KnownType(typeof(MyLog)),
        KnownType(typeof(MySimpleIf)),
        KnownType(typeof(MyUseRegistry))
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

        public VariableList List
        {
            get
            {
                var output = new VariableList();

                return output;
            }
        }
    }
}
