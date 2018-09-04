using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Combinations
{
    [DataContract]
    class MyInput : Base
    {
        [DataMember]
        protected ObservableCollection<MyVariable> variables;
        public ObservableCollection<MyVariable> Variables
        {
            get { return this.variables; }
            set { this.variables = value; }
        }

        public MyInput() : base()
        {
            this.variables = new ObservableCollection<MyVariable>();
        }

        public override object Clone()
        {
            return new MyInput()
            {
                Name = this.Name,
                Docs = this.Docs,
                Variables = new ObservableCollection<MyVariable>(
                            from v in this.Variables
                            select v.Clone() as MyVariable)
            };
        }
    }
}
