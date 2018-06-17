using master.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace master.Models.Contract.Block
{
    [DataContract]
    abstract class Base : ObjectBase, ICloneable
    {
        public abstract object Clone();

        //protected List<string> lines;

        //public abstract string ToCode(int indent = 0);

        //protected string ThrowError(string functionName, string description, bool addError)
        //{
        //    return string.Format("throw {0}", this.Error(functionName, description, addError));
        //}
        //protected string Error(string functionName, string description, bool addError)
        //{
        //    var msg = this.MessageFormat(functionName, description);
        //    var error = addError ? " + error" : string.Empty;
        //    return string.Format("new Error('{0}'{1});", msg, error);
        //}
        //protected string Log(string functionName, string description)
        //{
        //    var msg = this.MessageFormat(functionName, description);
        //    return string.Format("console.log('{0}');", msg);
        //}
        //protected string MessageFormat(string functionName, string description)
        //{
        //    return string.Format("[{0}] {1}", functionName, description);
        //}

        //protected void Add(string template, params object[] values)
        //{
        //    this.lines.Add(string.Format(template, values));
        //}
        //protected void AddToLast(string template, params object[] values)
        //{
        //    this.lines[this.lines.Count - 1] += string.Format(template, values);
        //}
        //protected void AddErrorCatch(string functionName, string description)
        //{
        //    this.AddToLast(".catch(function (error) {{");
        //    this.Add("\t{0}", this.ThrowError(functionName, description, true));
        //    this.Add("}}");
        //}

        //protected string ProduceCode(int indent)
        //{
        //    return string.Join(Environment.NewLine, this.lines.Select(s => new string('\t', indent) + s));
        //}
    }
}
