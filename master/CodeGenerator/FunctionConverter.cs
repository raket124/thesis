using master.Models.Contract;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Blocks.Custom;
using master.ViewModels.Contract.Block.Blocks;
using master.ViewModels.Contract.Block.Conditioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.CodeGenerator
{
    class FunctionConverter
    {
        public static string Convert(Function function)
        {
            if (!FunctionConverter.Validator(function))
                return "Invalid block or block combination.";

            var output = new List<string>();
            var indent = 0;
            foreach (Base block in function.Blocks)
            {
                output.Add(FunctionConverter.ConvertBlock(block, function, ref indent));
            }
            output.Add("}");

            return string.Join("\n", output);
        }

        private static string ConvertBlock(Base b, Function f, ref int i)
        {
            if (b.GetType() == typeof(MyInput))
                return FunctionConverter.Convert(b as MyInput, f, ref i);
            if (b.GetType() == typeof(MyRegistry))
                return FunctionConverter.Convert(b as MyRegistry, f, ref i);
            if (b.GetType() == typeof(MyTotalEcmrs))
                return FunctionConverter.Convert(b as MyTotalEcmrs, ref i);
            if (b.GetType() == typeof(MySimpleIf))
                return FunctionConverter.Convert(b as MySimpleIf, ref i);
            if (b.GetType() == typeof(MyElse))
                return FunctionConverter.Convert(b as MyElse, ref i);
            if (b.GetType() == typeof(MyEnd))
                return FunctionConverter.Convert(b as MyEnd, ref i);
            if (b.GetType() == typeof(MyLog))
                return FunctionConverter.Convert(b as MyLog, f, ref i);
            if (b.GetType() == typeof(MyError))
                return FunctionConverter.Convert(b as MyError, f, ref i);
            return string.Empty;
        }

        private static string Convert(MyInput input, Function f, ref int i)
        {
            i++;
            return DocConverter.Convert(f);
        }

        private static string Convert(MyRegistry input, Function f, ref int i)
        {
            return string.Join("\n", new List<string> {
                string.Format("{0}const reg = await get{1}Registry(namespace.{2}).catch(function (error) {{", FunctionConverter.Indent(i), "Asset", input.Alias),
                string.Format("{0}throw new Error('[{1}] An error occurred: ' + error);", FunctionConverter.Indent(i + 1), f.Name),
                string.Format("{0}}});", FunctionConverter.Indent(i)),
                string.Empty,
                string.Format("{0}await reg.{1}(tx.{2}).catch(function (error) {{", FunctionConverter.Indent(i), input.Action, input.Alias),
                string.Format("{0}throw new Error('[{1}] An error occurred: ' + error);", FunctionConverter.Indent(i + 1), f.Name),
                string.Format("{0}}});", FunctionConverter.Indent(i))
            });
        }

        private static string Convert(MyTotalEcmrs input, ref int i)
        {
            return string.Join("\n", new List<string> {
                string.Format("{0}var {1} = tx.{2}", FunctionConverter.Indent(i), input.Alias, input.Input),
                string.Format("{0}.map(function (ecmr) {{", FunctionConverter.Indent(i + 1)),
                string.Format("{0}return ecmr.goods.length;", FunctionConverter.Indent(i + 2)),
                string.Format("{0}}}).reduce(function (prev, curr) {{", FunctionConverter.Indent(i + 1)),
                string.Format("{0}return prev + curr;", FunctionConverter.Indent(i + 2)),
                string.Format("{0}}});", FunctionConverter.Indent(i))
            });
        }

        private static string Convert(MySimpleIf input, ref int i)
        {
            return string.Format("{0}if {1}(tx.{2} {3} tx.{4}) {{", 
                FunctionConverter.Indent(i++), 
                input.Condition.Invert ? "!" : string.Empty, 
                input.Condition.LHS,
                new VMconditionBase(null, null).COMPARE_DIC[input.Condition.Comparison], 
                input.Condition.RHS);
        }

        private static string Convert(MyElse input, ref int i)
        {
            return string.Format("{0}}}else {{", FunctionConverter.Indent(i - 1));
        }

        private static string Convert(MyEnd input, ref int i)
        {
            return string.Format("{0}}}", FunctionConverter.Indent(--i));
        }

        private static string Convert(MyLog input, Function f, ref int i)
        {
            return string.Format("{0}console.log('[{1}] {2}')", FunctionConverter.Indent(i), f.Name, input.Text);
        }

        private static string Convert(MyError input, Function f, ref int i)
        {
            return string.Format("{0}throw new Error('[{1}] {2}')", FunctionConverter.Indent(i), f.Name, input.Text);
        }


        private static bool Validator(Function f)
        {
            if (f.Blocks.Where(b => b.GetType() == typeof(MyInput)).Count() != 1)
                return false;


            return true;
        }

        private static string Indent(int indent)
        {
            return new String('\t', indent);
        }
    }
}
