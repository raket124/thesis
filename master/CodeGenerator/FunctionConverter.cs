using master.Models.Contract;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Blocks.Custom;
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
            //var x = new FunctionConverter();

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
            if (b.GetType() == typeof(MyUseRegistry))
                return FunctionConverter.Convert(b as MyUseRegistry, f, ref i);
            if (b.GetType() == typeof(MyTotalEcmrs))
                return FunctionConverter.Convert(b as MyTotalEcmrs, ref i);
            return string.Empty;
        }

        private static string Convert(MyInput input, Function f, ref int i)
        {
            i++;
            return DocConverter.Convert(f);
        }

        private static string Convert(MyUseRegistry input, Function f, ref int i)
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
                string.Format("{0}var totalEcmrs = tx.{1}", FunctionConverter.Indent(i), input.Input),
                string.Format("{0}.map(function (ecmr) {{", FunctionConverter.Indent(i + 1)),
                string.Format("{0}return ecmr.goods.length;", FunctionConverter.Indent(i + 2)),
                string.Format("{0}}}).reduce(function (prev, curr) {{", FunctionConverter.Indent(i + 1)),
                string.Format("{0}return prev + curr;", FunctionConverter.Indent(i + 2)),
                string.Format("{0}}});", FunctionConverter.Indent(i))
            });
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
