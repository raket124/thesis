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
    class MyValidation : Base
    {
        [DataMember]
        protected VariableLink myObject;
        public VariableLink Object
        {
            get { return this.myObject; }
            set { this.myObject = value; }
        }
        [DataMember]
        protected ObservableCollection<MyIfError> validations;
        public ObservableCollection<MyIfError> Validations
        {
            get { return this.validations; }
            set { this.validations = value; }
        }

        public MyValidation() : base()
        {
            this.myObject = new VariableLink(new MyVariable(typeof(string)));
            this.validations = new ObservableCollection<MyIfError>();
        }

        public override object Clone()
        {
            return new MyValidation()
            {
                Name = this.Name,
                Docs = this.Docs,
                Object = this.Object.Clone() as VariableLink,
                Validations = new ObservableCollection<MyIfError>(
                              from v in this.Validations
                              select v.Clone() as MyIfError)
            };
        }
    }
}
