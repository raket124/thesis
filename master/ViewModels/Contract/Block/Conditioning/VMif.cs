using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using System.Collections.ObjectModel;

namespace master.ViewModels.Contract.Block.Conditioning
{
    class VMif : VMbase
    {
        //private ObservableCollection<object> conditions;
        //public ObservableCollection<object> Conditions
        //{
        //    get { return this.conditions; }
        //    set
        //    {
        //        this.conditions = value;
        //        this.NotifyPropertyChanged();
        //    }
        //}

        public VMif(Base root) : base(root)
        {

        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        protected override string BlockName()
        {
            return "If - block";
        }

        protected override string Required()
        {
            return string.Format(this.reqFormat, "1+ condition(s)");
        }

        protected override string Optional()
        {
            return string.Empty;
        }
    }
}
