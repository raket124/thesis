using master.Models.Contract;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.Models.Contract.Block.Blocks.Custom;
using master.Models.Contract.Block.Combinations;
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
        public static string Convert(Function input_function)
        {
            var function = input_function.Clone() as Function; 
            if (function.Blocks.Count == 0)
                return "No block to visualize";

            var errors = FunctionConverter.Validator(function);
            if (errors.Count() > 0)
                return string.Join("\n", errors);

            var output = new List<string>();
            var indent = 0;
            foreach (Base block in function.Blocks)
            {
                output.Add(BlockConverter.ConvertBlock(block, function, ref indent));
                output.Add(string.Empty);
            }
                

            output.Insert(1 , "\tconst factory = getFactory();");
            output.Insert(1 , "\tconst me = getCurrentParticipant();");


            return string.Join("\n", output);
        }

        private static string Validator(Function f)
        {
            var errors = new List<string>();

            Type t = f.Blocks.Last().GetType();

            var inputBlockCount = f.Blocks.Where(b => b.GetType() == typeof(MyInput)).Count();
            if (inputBlockCount > 1)
                errors.Add("The number of input blocks is invalid");
            if(inputBlockCount == 1 && f.Blocks.First().GetType() != typeof(MyInput))
                errors.Add("Input needs to be the first defined block");
            if (f.Blocks.Last().GetType() == typeof(MyIf) ||
                f.Blocks.Last().GetType() == typeof(MyForeach) ||
                f.Blocks.Last().GetType() == typeof(MyElse))
                errors.Add("Last block cannot define an empty scope");
            if(errors.Count == 0 && !FunctionConverter.RestructureBlocks(f))
                errors.Add("Failed to calculate blockscope");
            return string.Join("\n", errors);
        }

        private static bool RestructureBlocks(Function f)
        {
            if (f.Blocks.First().GetType() != typeof(MyInput))
                f.Blocks.Insert(0, new MyInput());

            var pairs = new List<Tuple<int, int, Type>>();
            for (int i = 0; i < f.Blocks.Count; i++)
            {
                Base b = f.Blocks[i];

                if (b.GetType() == typeof(MyIf) ||
                    b.GetType() == typeof(MyForeach) ||
                    b.GetType() == typeof(MyInput))
                    pairs.Add(Tuple.Create(i + 1, -1, b.GetType())); //Create new pair

                if(b.GetType() == typeof(MyElse))
                {
                    var open_if_pairs = from p in pairs
                                        where p.Item2 == -1 && p.Item3 == typeof(MyIf)
                                        select p;
                    var indexof = pairs.IndexOf(open_if_pairs.Last());
                    if (open_if_pairs.Count() == 0)
                        return false; //No pairs that can be closed

                    pairs[indexof] = Tuple.Create(pairs[indexof].Item1, i - 1, pairs[indexof].Item3);

                    pairs.Insert(i - 1, Tuple.Create(i + 1, -1, pairs[indexof].Item3));
                }

                if (b.GetType() == typeof(MyEnd))
                {
                    var open_pairs = from p in pairs
                                     where p.Item2 == -1
                                     select p;
                    if (open_pairs.Count() == 0)
                        return false; //No pairs that can be closed

                    var indexof = pairs.IndexOf(open_pairs.Last());

                    pairs[indexof] = Tuple.Create(pairs[indexof].Item1, i - 1, pairs[indexof].Item3); //Close pair
                }
            }

            var remaining_pairs = (from p in pairs
                               where p.Item2 == -1 && p.Item3 != typeof(MyInput)
                               select p).ToList();
            for (int i = remaining_pairs.Count - 1; i >= 0; i--)
                f.Blocks.Insert(remaining_pairs[i].Item1 + 1, new MyEnd());

            f.Blocks.Add(new MyEnd());

            return true;
        }
    }
}
