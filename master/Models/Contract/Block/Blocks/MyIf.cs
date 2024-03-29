﻿using master.Models.Contract.Block.Conditioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MyIf : Base
    {
        protected Condition condition;
        public Condition Condition
        {
            get { return this.condition; }
            set { this.condition = value; }
        }

        public MyIf() : base()
        {
            this.condition = new Condition();
        }

        public override object Clone()
        {
            return new MyIf()
            {
                Name = this.Name,
                Docs = this.Docs,
                Condition = this.Condition.Clone() as Condition,
            };
        }
    }
}
