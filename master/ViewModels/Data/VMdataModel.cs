using master.Basis;
using master.Models;
using master.Models.Data;
using master.Models.Data.Component;
using master.Models.Data.Component.Components;
using master.Models.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Data
{
    class VMdataModel : MyBindableBase
    {
        private DataModel root;
        public DataModel Root
        {
            get { return this.root; }
        }

        public VMdataModel(DataModel root)
        {
            this.root = root;
        }

        public Dictionary<Type, List<string>> GetTypeList()
        {
            var group_name = "Listing";
            var output = this.GetObjectList();
            output.Add(typeof(string), new List<string>() { group_name });
            output.Add(typeof(double), new List<string>() { group_name });
            output.Add(typeof(int), new List<string>() { group_name });
            output.Add(typeof(long), new List<string>() { group_name });
            output.Add(typeof(DateTime), new List<string>() { group_name });
            output.Add(typeof(bool), new List<string>() { group_name });
            return output;
        }

        private Dictionary<Type, List<string>> GetObjectList()
        {
            var output = new Dictionary<Type, List<string>>();
            this.GetObjectList<MyAsset>(output);
            this.GetObjectList<MyConcept>(output);
            this.GetObjectListEnum(output);
            this.GetObjectList<MyEvent>(output);
            this.GetObjectList<MyParticipant>(output);
            this.GetObjectList<MyTransaction>(output);
            return output;
        }

        //public List<ObjectValue> GetObjectList<T>() where T : Inheritance
        //{
        //    return new List<ObjectValue>(from c in this.root.GetComponent<T>()
        //                                 select new ObjectValue()
        //                                 {
        //                                     Name = c.Name,
        //                                     Properties = new List<Variable>(c.Components)
        //                                 });
        //}

        //public List<ObjectValue> GetObjectListEnum()
        //{
        //    return new List<ObjectValue>(from c in this.root.GetComponent<MyEnum>()
        //                                 select new ObjectValue()
        //                                 {
        //                                     Name = c.Name
        //                                 });
        //}


        private void GetObjectListEnum(Dictionary<Type, List<string>> output)
        {
            output.Add(typeof(MyEnum), (from component in this.root.GetComponent<MyEnum>()
                                        select component.Name).ToList());
        }

        private void GetObjectList<T>(Dictionary<Type, List<string>> output) where T : Inheritance
        {
            output.Add(typeof(T), (from component in this.root.GetComponent<T>()
                                   where !component.Abstract
                                   select component.Name).ToList());
        }
    }
}
