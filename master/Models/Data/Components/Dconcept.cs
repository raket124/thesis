﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    class Dconcept : Dinheritance
    {
        public Dconcept(string name) : base(name)
        {

        }
    }
}