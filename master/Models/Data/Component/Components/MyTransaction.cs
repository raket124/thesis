﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Data.Component.Components
{
    [DataContract]
    public class MyTransaction : Inheritance
    {
        public MyTransaction(string name) : base(name) { }

        protected override string ObjectName()
        {
            return "Transaction";
        }
    }
}
