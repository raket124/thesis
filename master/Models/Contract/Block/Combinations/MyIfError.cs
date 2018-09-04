using master.Models.Contract.Block.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Combinations
{
    [DataContract]
    class MyIfError : Base
    {
        [DataMember]
        protected MyIf myIf;
        public MyIf If
        {
            get { return this.myIf; }
            set { this.myIf = value; }
        }
        [DataMember]
        protected MyError myError;
        public MyError Error
        {
            get { return this.myError; }
            set { this.myError = value; }
        }

        public MyIfError() : base()
        {
            this.myIf = new MyIf();
            this.myError = new MyError();
        }

        public override object Clone()
        {
            return new MyIfError()
            {
                Name = this.Name,
                Docs = this.Docs,
                If = this.If.Clone() as MyIf,
                Error = this.Error.Clone() as MyError,
            };
        }
    }
}
