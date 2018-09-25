using master.Models.Contract;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Blocks.Custom;
using master.Models.Contract.Block.Combinations;
using master.ViewModels.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.CodeGenerator
{
    static class BlockConverter
    {
        private static string Indent(int indent)
        {
            return new String('\t', indent);
        }

        private static string ConvertOutput(string input)
        {
            var variable = input.Replace("->", ".");
            if (variable.StartsWith("input."))
                variable = variable.Replace("input.", "tx.");
            return variable;
        }

        public static string ConvertBlock(Base b, Function f, ref int i)
        {
            if (b.GetType() == typeof(MyAssign))
                return Convert(b as MyAssign, f, ref i);
            if (b.GetType() == typeof(MyElse))
                return Convert(b as MyElse, f, ref i);
            if (b.GetType() == typeof(MyEnd))
                return Convert(b as MyEnd, f, ref i);
            if (b.GetType() == typeof(MyError))
                return Convert(b as MyError, f, ref i);
            if (b.GetType() == typeof(MyForeach))
                return Convert(b as MyForeach, f, ref i);
            if (b.GetType() == typeof(MyIf))
                return Convert(b as MyIf, f, ref i);
            if (b.GetType() == typeof(MyLog))
                return Convert(b as MyLog, f, ref i);
            if (b.GetType() == typeof(MyRegistry))
                return Convert(b as MyRegistry, f, ref i);

            if (b.GetType() == typeof(MyTotalEcmrs))
                return Convert(b as MyTotalEcmrs, f, ref i);

            if (b.GetType() == typeof(MyCreation))
                return Convert(b as MyCreation, f, ref i);
            if (b.GetType() == typeof(MyIfError))
                return Convert(b as MyIfError, f, ref i);
            if (b.GetType() == typeof(MyInput))
                return Convert(b as MyInput, f, ref i);
            if (b.GetType() == typeof(MyModification))
                return Convert(b as MyModification, f, ref i);
            if (b.GetType() == typeof(MyValidation))
                return Convert(b as MyValidation, f, ref i);

            return "Unidentified block provided.";
        }

        private static string Convert(MyAssign input, Function f, ref int i)
        {
            var variable = ConvertOutput(input.Variable.Output);
            var value = ConvertOutput(input.Value.Output);

            if (input.Value.Value.Alias == string.Empty)
                value = string.Format("factory.NewResource('namespace','{0}');", input.Value.Value.ObjectName);
            if (input.Value.Value.Alias == "#currentUser")
                value = "me.getIdentifier()";
            if (input.Value.Value.Alias == "#currentDateTime")
                value = "tx.timestamp";


            return string.Format("{0}{1} = {2};", BlockConverter.Indent(i), variable, value);
        }
        private static string Convert(MyElse input, Function f, ref int i)
        {
            return string.Format("{0}}}else {{", BlockConverter.Indent(i - 1));
        }
        private static string Convert(MyEnd input, Function f, ref int i)
        {
            return string.Format("{0}}}", BlockConverter.Indent(--i));
        }
        private static string Convert(MyError input, Function f, ref int i)
        {
            return string.Format("{0}throw new Error('[{1}] {2}');", BlockConverter.Indent(i), f.Name, ConvertText(input.Text));
        }
        private static string Convert(MyForeach input, Function f, ref int i)
        {
            var indentifierName = input.IteratorAlias.Value.Alias == string.Empty ? "i" : input.IteratorAlias.Value.Alias;
            return string.Join("\n", new List<string> {
                    string.Format("{0}for (var {1} = 0; {1} < {2}.length; {1}++)  {{", Indent(i++), indentifierName, input.List.Output),
                    string.Format("{0}var {1} = {2}[{3}];", Indent(i), input.ObjectAlias.Output, input.List.Output, indentifierName)
                });
        }
        private static string Convert(MyIf input, Function f, ref int i)
        {
            var conditionList = new Dictionary<string, string>();
            foreach(var c in input.Condition.Conditions)
            {
                conditionList.Add(c.Alias, string.Format("{0} {1} {2}", ConvertOutput(c.LHS.Output), new VMconditionBaseCompare(c.Comparison).Value, ConvertOutput(c.RHS.Output)));
            }

            if (conditionList.Count == 1)
                return string.Format("{0}if({1}){{", Indent(i++), conditionList.First().Value);

            var groupList = new Dictionary<string, string>();
            foreach (var g in input.Condition.Groups)
            {
                var content = conditionList[g.Conditions[0]] + " ";

                for (int j = 0; j < g.Connectors.Count; j++)
                {
                    content += new VMconditionGroupCompare(g.Connectors[j]).Value + " ";
                    content += conditionList[g.Conditions[j + 1]] + " ";
                }

                conditionList.Add(g.Alias, content);
            }

            var output = groupList[input.Condition.Value.Conditions[0]];
            for (int j = 0; j < input.Condition.Value.Connectors.Count; j++)
            {
                output += new VMconditionGroupCompare(input.Condition.Value.Connectors[j]).Value + " ";
                output += groupList[input.Condition.Value.Conditions[j + 1]] + " ";
            }

            return string.Format("{0}if({1}){{", Indent(i++), output);
        }
        private static string Convert(MyLog input, Function f, ref int i)
        {
            return string.Format("{0}console.log('[{1}] {2}');", BlockConverter.Indent(i), f.Name, ConvertText(input.Text));
        }
        private static string Convert(MyRegistry input, Function f, ref int i)
        {
            return string.Join("\n", new List<string> {
                    string.Format("{0}const reg = await get{1}Registry(namespace.{2}).catch(function (error) {{", Indent(i), input.Variable.Value.Type.Name.Replace("My", ""), input.Variable.Value.ObjectName),
                    string.Format("{0}throw new Error('[{1}] An error occurred: ' + error);", Indent(i + 1), f.Name),
                    string.Format("{0}}});", Indent(i)),
                    string.Format("{0}await reg.{1}({2}).catch(function (error) {{", Indent(i), input.Action, ConvertOutput(input.Variable.Output)),
                    string.Format("{0}throw new Error('[{1}] An error occurred: ' + error);", Indent(i + 1), f.Name),
                    string.Format("{0}}});", Indent(i))
                });
        }

        //------------------------------------------------------------------------

        private static string Convert(MyTotalEcmrs input, Function f, ref int i)
        {
            return string.Join("\n", new List<string> {
                    string.Format("{0}var {1} = tx.{2}", Indent(i), input.Alias, input.Input),
                    string.Format("{0}.map(function (ecmr) {{", Indent(i + 1)),
                    string.Format("{0}return ecmr.goods.length;", Indent(i + 2)),
                    string.Format("{0}}}).reduce(function (prev, curr) {{", Indent(i + 1)),
                    string.Format("{0}return prev + curr;", Indent(i + 2)),
                    string.Format("{0}}});", Indent(i))
                });
        }

        //------------------------------------------------------------------------

        private static string Convert(MyCreation input, Function f, ref int i)
        {
            var output = string.Format("{0}const {1} = factory.new{2}('namespace', '{3}');", Indent(i), input.Object.Value.Value.Alias, input.Object.Value.Value.Type.Name, input.Object.Value.Value.ObjectName);
            foreach (var a in input.Modifications.Assignments)
                output += string.Format("\n{0}", Convert(a, f, ref i));
            return output;
                    
        }
        private static string Convert(MyIfError input, Function f, ref int i)
        {
            return Convert(input.If, f, ref i) + "\n" + Convert(input.Error, f, ref i) + "\n" + Convert(new MyEnd(), f, ref i);
        }
        private static string Convert(MyInput input, Function f, ref int i)
        {
            i++;
            return DocConverter.Convert(f);
        }
        private static string Convert(MyModification input, Function f, ref int i)
        {
            var modifications = new List<string>();
            foreach (var a in input.Assignments)
                modifications.Add(Convert(a, f, ref i));
            return string.Join("\n", modifications);
        }
        private static string Convert(MyValidation input, Function f, ref int i)
        {
            var validations = new List<string>();
            foreach (var v in input.Validations)
                validations.Add(Convert(v, f, ref i));
            return string.Join("\n", validations);
        }

        //------------------------------------------------------------------------

        private static string ConvertText(string input)
        {
            var alias_marker = "#alias:";
            var input_marker = "input.";

            var parts = input.Split(' ');
            for (int i = 0; i < parts.Count(); i++)
            {
                var part = parts[i];
                if (part.StartsWith(alias_marker))
                {
                    var variable = part.Replace(alias_marker, string.Empty);
                    if(variable.StartsWith(input_marker))
                        variable = variable.Replace(input_marker, "tx.");

                    parts[i] = string.Format("' + {0} + '", variable);
                    if (part.EndsWith("\n"))
                        parts[i] += "\n";
                }
                
            }
            return string.Join(" ", parts);
        }
    }
}
