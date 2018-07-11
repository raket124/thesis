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
            var x = new FunctionConverter();

            if (!x.Validator(function))
                return "Invalid block or block combination.";

            var output = new List<string>();
            var indent = 0;
            foreach (Base block in function.Blocks)
            {
                output.Add(x.ConvertBlock(block, function, ref indent));
            }
            output.Add("}");

            return string.Join("\n", output);
        }

        private string ConvertBlock(Base b, Function f, ref int i)
        {
            if (b.GetType() == typeof(MyInput))
                return this.Convert(b as MyInput, f, ref i);
            if (b.GetType() == typeof(MyUseRegistry))
                return this.Convert(b as MyUseRegistry, f, ref i);

            return string.Empty;
        }

        private string Convert(MyInput input, Function f, ref int i)
        {
            i++;
            return DocConverter.Convert(f);
        }

        private string Convert(MyUseRegistry input, Function f, ref int i)
        {
            return string.Join("\n", new List<string> {
                string.Format("{0}const reg = await get{1}Registry(namespace.{2}).catch(function (error) {{", this.Indent(i), "Asset", input.Alias),
                string.Format("{0}throw new Error('[{1}] An error occurred: ' + error);", this.Indent(i + 1), f.Name),
                string.Format("{0}}});", this.Indent(i)),
                string.Empty,
                string.Format("{0}await reg.{1}(tx.{2}).catch(function (error) {{", this.Indent(i), input.Action, input.Alias),
                string.Format("{0}throw new Error('[{1}] An error occurred: ' + error);", this.Indent(i + 1), f.Name),
                string.Format("{0}}});", this.Indent(i))
            });
        }

        private string Convert(MyTotalEcmrs input, int i)
        {
            return string.Join("\n", new List<string> {
                string.Format("{0}var totalEcmrs = tx.{1}", this.Indent(i), input.Input),
                string.Format("{0}.map(function (ecmr) {{", this.Indent(i)),
                string.Format("{0}return ecmr.goods.length;", this.Indent(i + 1)),
                string.Format("{0}}}).reduce(function (prev, curr) {{", this.Indent(i)),
                string.Format("{0}return prev + curr;", this.Indent(i + 1)),
                string.Format("{0}}});", this.Indent(i))
            });
        }


        private bool Validator(Function f)
        {
            if (f.Blocks.Where(b => b.GetType() == typeof(MyInput)).Count() != 1)
                return false;


            return true;
        }

        private string Indent(int indent)
        {
            return new String('\t', indent);
        }
    }
}
