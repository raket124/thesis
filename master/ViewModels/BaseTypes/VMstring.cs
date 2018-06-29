using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.BaseTypes
{
    class VMstring : VMtype<string>
    {
        public VMstring(string value) : base(value) { }
    }
}
