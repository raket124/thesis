using master.Models.Contract;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Blocks.Custom;
using master.Models.Contract.Block.Combinations;
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

        public static string ConvertBlock(Base b, Function f, ref int i)
        {
            if (b.GetType() == typeof(MyAssign))
                return BlockConverter.Convert(b as MyAssign, f, ref i);
            if (b.GetType() == typeof(MyElse))
                return BlockConverter.Convert(b as MyElse, f, ref i);
            if (b.GetType() == typeof(MyEnd))
                return BlockConverter.Convert(b as MyEnd, f, ref i);
            if (b.GetType() == typeof(MyError))
                return BlockConverter.Convert(b as MyError, f, ref i);
            if (b.GetType() == typeof(MyForeach))
                return BlockConverter.Convert(b as MyForeach, f, ref i);
            if (b.GetType() == typeof(MyIf))
                return BlockConverter.Convert(b as MyIf, f, ref i);
            if (b.GetType() == typeof(MyLog))
                return BlockConverter.Convert(b as MyLog, f, ref i);
            if (b.GetType() == typeof(MyRegistry))
                return BlockConverter.Convert(b as MyRegistry, f, ref i);

            if (b.GetType() == typeof(MyTotalEcmrs))
                return BlockConverter.Convert(b as MyTotalEcmrs, f, ref i);

            if (b.GetType() == typeof(MyCreation))
                return BlockConverter.Convert(b as MyCreation, f, ref i);
            if (b.GetType() == typeof(MyIfError))
                return BlockConverter.Convert(b as MyIfError, f, ref i);
            if (b.GetType() == typeof(MyInput))
                return BlockConverter.Convert(b as MyInput, f, ref i);
            if (b.GetType() == typeof(MyModification))
                return BlockConverter.Convert(b as MyModification, f, ref i);
            if (b.GetType() == typeof(MyValidation))
                return BlockConverter.Convert(b as MyValidation, f, ref i);

            return "Unidentified block provided.";
        }

        private static string Convert(MyAssign input, Function f, ref int i)
        {
            return "Unidentified block provided.";
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
            return string.Format("{0}throw new Error('[{1}] {2}');", BlockConverter.Indent(i), f.Name, BlockConverter.Convert(input.Text));
        }
        private static string Convert(MyForeach input, Function f, ref int i)
        {
            return "Unidentified block provided.";
        }
        private static string Convert(MyIf input, Function f, ref int i)
        {
            return "Unidentified block provided.";
        }
        private static string Convert(MyLog input, Function f, ref int i)
        {
            return string.Format("{0}console.log('[{1}] {2}');", BlockConverter.Indent(i), f.Name, BlockConverter.Convert(input.Text));
        }
        private static string Convert(MyRegistry input, Function f, ref int i)
        {
            return string.Join("\n", new List<string> {
                    string.Format("{0}const reg = await get{1}Registry(namespace.{2}).catch(function (error) {{", BlockConverter.Indent(i), "Asset", input.Variable),
                    string.Format("{0}throw new Error('[{1}] An error occurred: ' + error);", BlockConverter.Indent(i + 1), f.Name),
                    string.Format("{0}}});", BlockConverter.Indent(i)),
                    string.Empty,
                    string.Format("{0}await reg.{1}(tx.{2}).catch(function (error) {{", BlockConverter.Indent(i), input.Action, input.Variable),
                    string.Format("{0}throw new Error('[{1}] An error occurred: ' + error);", BlockConverter.Indent(i + 1), f.Name),
                    string.Format("{0}}});", BlockConverter.Indent(i))
                });
        }

        //------------------------------------------------------------------------

        private static string Convert(MyTotalEcmrs input, Function f, ref int i)
        {
            return "Unidentified block provided.";
        }

        //------------------------------------------------------------------------

        private static string Convert(MyCreation input, Function f, ref int i)
        {
            return "Unidentified block provided.";
        }
        private static string Convert(MyIfError input, Function f, ref int i)
        {
            return "Unidentified block provided.";
        }
        private static string Convert(MyInput input, Function f, ref int i)
        {
            return "Unidentified block provided.";
        }
        private static string Convert(MyModification input, Function f, ref int i)
        {
            return "Unidentified block provided.";
        }
        private static string Convert(MyValidation input, Function f, ref int i)
        {
            return "Unidentified block provided.";
        }

        //------------------------------------------------------------------------

        private static string Convert(string input)
        {
            var alias_marker = "#alias:";
            var input_marker = "input.";

            var parts = input.Split(' ');
            for (int i = 0; i < parts.Count(); i++)
            {
                var part = parts[i];
                if (parts[i].StartsWith(alias_marker))
                {
                    var variable = part.Replace(alias_marker, string.Empty);
                    if(variable.StartsWith(input_marker))
                        variable.Replace(alias_marker, "tx.");

                    parts[i] = string.Format("' + **variable** + '", variable);
                    if (part.EndsWith("\n"))
                        parts[i] += "\n";
                }
                
            }
            return string.Join("\n", parts);
        }

        //private static string Convert(MyInput input, Function f, ref int i)
        //{
        //    i++;
        //    return DocConverter.Convert(f);
        //}

        //private static string Convert(MyRegistry input, Function f, ref int i)
        //{
        //    return string.Join("\n", new List<string> {
        //        string.Format("{0}const reg = await get{1}Registry(namespace.{2}).catch(function (error) {{", FunctionConverter.Indent(i), "Asset", input.Variable),
        //        string.Format("{0}throw new Error('[{1}] An error occurred: ' + error);", FunctionConverter.Indent(i + 1), f.Name),
        //        string.Format("{0}}});", FunctionConverter.Indent(i)),
        //        string.Empty,
        //        string.Format("{0}await reg.{1}(tx.{2}).catch(function (error) {{", FunctionConverter.Indent(i), input.Action, input.Variable),
        //        string.Format("{0}throw new Error('[{1}] An error occurred: ' + error);", FunctionConverter.Indent(i + 1), f.Name),
        //        string.Format("{0}}});", FunctionConverter.Indent(i))
        //    });
        //}

        //private static string Convert(MyTotalEcmrs input, ref int i)
        //{
        //    return string.Join("\n", new List<string> {
        //        string.Format("{0}var {1} = tx.{2}", FunctionConverter.Indent(i), input.Alias, input.Input),
        //        string.Format("{0}.map(function (ecmr) {{", FunctionConverter.Indent(i + 1)),
        //        string.Format("{0}return ecmr.goods.length;", FunctionConverter.Indent(i + 2)),
        //        string.Format("{0}}}).reduce(function (prev, curr) {{", FunctionConverter.Indent(i + 1)),
        //        string.Format("{0}return prev + curr;", FunctionConverter.Indent(i + 2)),
        //        string.Format("{0}}});", FunctionConverter.Indent(i))
        //    });
        //}

        ////private static string Convert(MySimpleIf input, ref int i)
        ////{
        ////    return string.Format("{0}if {1}(tx.{2} {3} tx.{4}) {{", 
        ////        FunctionConverter.Indent(i++), 
        ////        input.Condition.Invert ? "!" : string.Empty, 
        ////        input.Condition.LHS,
        ////        new VMconditionBase(null, null).COMPARE_DIC[input.Condition.Comparison], 
        ////        input.Condition.RHS);
        ////}

        //private static string Convert(MyLog input, Function f, ref int i)
        //{
        //    return string.Format("{0}console.log('[{1}] {2}');", FunctionConverter.Indent(i), f.Name, input.Text);
        //}

        //private static string Convert(MyError input, Function f, ref int i)
        //{
        //    return string.Format("{0}throw new Error('[{1}] {2}');", FunctionConverter.Indent(i), f.Name, input.Text);
        //}

        //private static string Convert(MyForeach input, ref int i)
        //{
        //    return string.Format("{0}tx.{1}.forEach(function ({2}) {{", FunctionConverter.Indent(i++));
        //}

        //private static string Convert(MyAssign input, ref int i)
        //{
        //    return string.Format("{0}{tx.1} = {tx.2};", FunctionConverter.Indent(i++), input.Variable, input.Value);
        //}

    }
}
