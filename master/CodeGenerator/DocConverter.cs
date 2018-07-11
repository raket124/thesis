using master.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.CodeGenerator
{
    class DocConverter
    {
        public static string Convert(Function function)
        {
            if(function.Access == Function.ACCESSIBILITY.Private)
            {
                return string.Join("\n", new List<string>
                {
                    StartComment(),
                    Docs(function.Docs),
                    EndComment(),
                    Header(function.Name)
                });
            }
            else
            {
                return string.Join("\n", new List<string>
                {
                    StartComment(),
                    Docs(function.Docs),
                    PublicHeader("org.namespace", function.Name),
                    EndComment(),
                    Header(function.Name)
                });
            }
        }

        private static string StartComment()
        {
            return "/**";
        }

        private static string EndComment()
        {
            return " */";
        }

        private static string PublicHeader(string NameSpace, string ObjectName)
        {
            return string.Join("\n", new List<string> {
                string.Format(" * @param {{{0}.{1}}} tx", NameSpace, ObjectName),
                string.Format(" * @transaction")
            });
        }

        private static string Docs(string docs)
        {
            var doc = (from d in docs.Split('\n')
                       select " * " + d).ToList();
            return doc.Count > 0 ? string.Join("\n", doc) : " * ";
        }

        private static string Header(string name)
        {
            return string.Format("function {0}(tx) {{", name);
        }
    }
}
