using master.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.BaseTypes
{
    class VMenum<T> : VMtype<T> where T : struct, IConvertible
    {
        protected Dictionary<T, string> dictionary;

        public new string Value
        {
            get { return this.dictionary[this.value]; }
            set
            {
                this.value = (from entry in this.dictionary
                              where entry.Value == value
                              select entry.Key).First();
                this.NotifyPropertyChanged();
            }
        }

        public VMenum(T value) : base(value)
        {
            if (!typeof(T).IsEnum) throw new Exception("Enum only");
        }

        public IList<string> Options
        {
            get { return this.dictionary.Values.ToList(); }
        }
    }
}
