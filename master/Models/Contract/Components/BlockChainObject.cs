using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Blocks
{
    class BlockChainObject : BlockBase
    {
        public enum OPTION { Add, Update, Remove };
        public enum OBJECT_CATEGORY { Participant, Asset };

        protected OPTION option;
        protected OBJECT_CATEGORY objectCategory;
        protected bool isList;
        protected string NS;
        protected string objectType;
        protected string objectName;

        protected string tempVar;
        protected string mainName;

        public BlockChainObject() : base()
        {
            this.tempVar = "br";
            this.mainName = "ExportVinNumbers";
        }

        public void Set(OPTION option, OBJECT_CATEGORY objectCategory, bool isList, string NS, string objectType, string objectName)
        {
            this.option = option;
            this.objectCategory = objectCategory;
            this.isList = isList;
            this.NS = NS;
            this.objectType = objectType;
            this.objectName = objectName;
        }

        protected string GetAction()
        {
            return string.Format("{0}{1}", this.option, this.isList ? "All" : string.Empty);
        }
        protected string GetRegistry()
        {
            return string.Format("get{0}Registry", this.objectCategory);
        }
        protected string GetObjectNS()
        {
            return string.Format("{0}.{1}", this.NS, this.objectType);
        }

        public override string ToCode(int indent = 0)
        {
            this.Add("const {0} = await {1}('{2}')", this.tempVar, this.GetRegistry(), this.GetObjectNS());
            this.AddErrorCatch(this.mainName, "Failed to retrieve the object registery.");
            this.Add("await {0}.{1}({2})", this.tempVar, this.GetAction(), this.objectName);
            this.AddErrorCatch(this.mainName, "Failed to ");
            return this.ProduceCode(indent);
        }
    }
}
