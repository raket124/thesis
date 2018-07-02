﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block.Blocks
{
    [DataContract]
    class MyInput : Base
    {
        [DataMember]
        protected ObservableCollection<Variable> vars;
        public ObservableCollection<Variable> Vars
        {
            get { return this.vars; }
            set { this.vars = value; }
        }

        public MyInput() : base()
        {
            this.vars = new ObservableCollection<Variable>();
        }

        public override object Clone()
        {
            return new MyInput()
            {
                Name = this.Name,
                Docs = this.Docs,
                Vars = new ObservableCollection<Variable>(
                       from var in this.Vars
                       select var.Clone() as Variable)
            };
        }
    }
}
