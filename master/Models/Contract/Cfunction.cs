using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models
{
    [DataContract]
    [KnownType(typeof(Berror))]
    [KnownType(typeof(Bassign))]
    class Cfunction : Basis
    {
        public enum ACCESSIBILITY { Public, Private, Controlled }

        [DataMember]
        protected ACCESSIBILITY access;
        public ACCESSIBILITY Access
        {
            get { return this.access; }
            set { this.access = value; }
        }
        [DataMember]
        protected ObservableCollection<Bbase> blocks;
        public ObservableCollection<Bbase> Blocks
        {
            get { return this.blocks; }
            set { this.blocks = value; }
        }

        
        //protected Dictionary<int, Berror> Get
        //{
        //    get
        //    {
        //        var output = new Dictionary<int, Berror>();
        //        for (int i = 0; i < blocks.Count; i++)
        //        {
        //            if(blocks[i].GetType() == typeof(Berror))
        //            {
        //                output.Add(i, blocks[i] as Berror);
        //            }
        //        }
        //        return output;
        //    }
        //}

        public Cfunction(string name, ACCESSIBILITY access) : base(name)
        {
            this.access = access;
            this.blocks = new ObservableCollection<Bbase>();
        }


    }
}
