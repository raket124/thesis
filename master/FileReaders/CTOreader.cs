using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models;
using System.IO;
using System.Text.RegularExpressions;
using master.Util;

namespace master.FileReaders
{
    class CTOreader : Rbase
    {
        public CTOreader() : base(".cto")
        {
        }

        public override Mfile ProcessFile(int index)
        {
            var input = File.ReadAllText(this.fileList[index]);

            var outputFile = new Mfile()
            {
                Namespace = this.ExtractNameSpace(ref input)
            };

            List<Tuple<string, string>> headerContent = this.SplitFile(input);


            var openIndexes = new List<int>() { 0 }; //Account for the first iteration
            var closeIndexes = new List<int>() { -1 }; //Account for the first iteration

            for (int i = input.IndexOf('{'); i > -1; i = input.IndexOf('{', i + 1))
                openIndexes.Add(i);
            for (int i = input.IndexOf('}'); i > -1; i = input.IndexOf('}', i + 1))
                closeIndexes.Add(i);

            if (openIndexes.Count != closeIndexes.Count || openIndexes.Count == 1)
                throw new Exception("input is invalid!");

            var rawDataModels = new List<Tuple<string, string>>();
            var brackets = openIndexes.Zip(closeIndexes, (o, c) => new { open = o, close = c }).ToList();
            for (int i = 1; i < brackets.Count(); i++)
            {
                int headerStart = brackets[i - 1].close + 1;
                int headerLenght = brackets[i].open - brackets[i - 1].close - 1;
                int contentStart = brackets[i].open + 1;
                int contentLenght = brackets[i].close - brackets[i].open - 1;

                var header = input.Substring(headerStart, headerLenght);
                var content = input.Substring(contentStart, contentLenght);
                rawDataModels.Add(Tuple.Create<string, string>(header, content));

                ExtractVars(content, new List<string>());
            }

            foreach (Tuple<string, string> item in rawDataModels)
            {
                List<string> lines = Regex.Split(item.Item1, "\r\n|\r|\n").Where(s => s != String.Empty).ToList();

                var comments = new List<string>();
                var data = new List<string>();
                foreach (string line in lines)
                    this.ExtractComments(line, comments, data);

                string description = String.Join("\r\n", comments);


                outputFile.AddComponent(Tuple.Create<string, string>(description, String.Join(" ", data)));

                //if (comments.First() == string.Empty)
                //    comments.RemoveAt(0);

                //if (comments.Last() == string.Empty)
                //    comments.RemoveAt(comments.Count - 1);
            }

            return outputFile;
        }

        protected string ExtractNameSpace(ref string input)
        {
            string token = "namespace ";
            int tokenIndex = input.IndexOf(token);
            if (tokenIndex == -1)
                throw new Exception("Namespace is not defined, while being a requirement");

            int startIndex = tokenIndex + token.Length;
            while (input[startIndex] == ' ')
                startIndex++;
            
            int endIndex = startIndex + 1;
            while (input[endIndex] != ' ' && input[endIndex] != '\n' && input[endIndex] != '\r')
                endIndex++;

            string output = input.Substring(startIndex, endIndex - startIndex);
            input = input.Remove(tokenIndex, endIndex - tokenIndex);
            return output;
        }

        protected List<Tuple<string, string>> SplitFile(string input)
        {
            var openIndexes = input.IndexesOf("{");
            var closeIndexes = input.IndexesOf("}");
            openIndexes.Insert(0, 0); //first iteration
            closeIndexes.Insert(0, -1); //first iteration

            if (openIndexes.Count != closeIndexes.Count || openIndexes.Count == 1)
                throw new Exception("input is invalid!");

            var output = new List<Tuple<string, string>>();
            var brackets = openIndexes.Zip(closeIndexes, (o, c) => new { open = o, close = c }).ToList();
            for (int i = 1; i < brackets.Count(); i++)
            {
                int headerStart = brackets[i - 1].close + 1;
                int headerLenght = brackets[i].open - brackets[i - 1].close - 1;
                int contentStart = brackets[i].open + 1;
                int contentLenght = brackets[i].close - brackets[i].open - 1;

                var header = input.Substring(headerStart, headerLenght);
                var content = input.Substring(contentStart, contentLenght);


                Tuple<string, string> split = this.SplitHeader(header);



                //output.Add(Tuple.Create<string, string>(header, content));
            }
            return output;
        }

        protected Tuple<string, string> SplitHeader(string input)
        {
            var lineRegex = new Regex(@"^[*|/|\\].*");
            var newLineRegex = new Regex(@"\r\n|\r|\n");


            var lines = newLineRegex.Split(input).Where(s => s != String.Empty).ToList();
            var commentLines = new List<string>();
            var headerLines = new List<string>();
            foreach (string rawLine in lines)
            {
                var line = rawLine.Trim();
                if (lineRegex.Match(line).Success)
                {
                    do
                    {
                        line = line.Remove(0, 1);
                    } while (lineRegex.Match(line).Success);
                    commentLines.Add(input.Trim());
                }
                else
                    headerLines.Add(line);
            }

            var comment = String.Join("\r\n", commentLines);
            var header = String.Join(" ", headerLines);

            return Tuple.Create<string, string>(comment, header);

            var regex = new Regex(@"^[*|/|\\].*");

            //string input = line.Trim();

            //if (regex.Match(input).Success)
            //{
            //    do
            //    {
            //        input = input.Remove(0, 1);
            //    } while (regex.Match(input).Success);
            //    commentOutput.Add(input.Trim());
            //    return;
            //}
            //otherOutput.Add(input);
            return null;
        }

        protected string ConvertComments(string input)
        {
            var regex = new Regex(@"^[*|/|\\].*");

            //string input = line.Trim();

            //if (regex.Match(input).Success)
            //{
            //    do
            //    {
            //        input = input.Remove(0, 1);
            //    } while (regex.Match(input).Success);
            //    commentOutput.Add(input.Trim());
            //    return;
            //}
            //otherOutput.Add(input);

            return "";
        }

        protected List<string> ConvertVariables(string input)
        {
            return null;
        }

        protected void ExtractVars(string input, List<string> output)
        {
            var indexList = new List<int>();
            indexList.AddRange(input.IndexesOf(" 0 "));
            indexList.AddRange(input.IndexesOf(" --> "));
            indexList.Sort();

            for (int i = 1; i < indexList.Count(); i++)
            {
                int start = indexList[i - 1];
                int lenght = indexList[i] - indexList[i - 1];
                output.Add(input.Substring(start, lenght));
            }

        }

        protected void ExtractComments(string line, List<string> commentOutput, List<string> otherOutput)
        {
            Regex regex = new Regex(@"^[*|/|\\].*");
            string input = line.Trim();

            if (regex.Match(input).Success)
            {
                do
                {
                    input = input.Remove(0, 1);
                } while (regex.Match(input).Success);
                commentOutput.Add(input.Trim());
                return;
            }


            otherOutput.Add(input);
        }
    }
}
