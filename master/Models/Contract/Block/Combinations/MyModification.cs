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
    class MyModification : Base
    {
        [DataMember]
        protected VariableLink myObject;
        public VariableLink Object
        {
            get { return this.myObject; }
            set { this.myObject = value; }
        }
        [DataMember]
        protected ObservableCollection<MyAssign> assignments;
        public ObservableCollection<MyAssign> Assignments
        {
            get { return this.assignments; }
            set { this.assignments = value; }
        }

        public MyModification() : base()
        {
            this.myObject = new VariableLink(new MyVariable(typeof(string)));
            this.assignments = new ObservableCollection<MyAssign>();
        }

        public override object Clone()
        {
            return new MyModification()
            {
                Name = this.Name,
                Docs = this.Docs,
                Object = this.Object.Clone() as VariableLink,
                Assignments = new ObservableCollection<MyAssign>(
                              from a in this.Assignments
                              select a.Clone() as MyAssign)
            };
        }
    }
}
