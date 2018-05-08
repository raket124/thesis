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

            var outputFile = new Mfile("dummie value")
            {
                Namespace = this.ExtractNameSpace(ref input)
            };

            var headerContent = this.SplitFile(input);



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

        protected List<Masset> SplitFile(string input)
        {
            var openIndexes = input.IndexesOf("{");
            var closeIndexes = input.IndexesOf("}");
            openIndexes.Insert(0, 0); //first iteration
            closeIndexes.Insert(0, -1); //first iteration

            if (openIndexes.Count != closeIndexes.Count || openIndexes.Count == 1)
                throw new Exception("input is invalid!");

            var output = new List<Masset>();
            var brackets = openIndexes.Zip(closeIndexes, (o, c) => new { open = o, close = c }).ToList();
            for (int i = 1; i < brackets.Count(); i++)
            {
                int topStart = brackets[i - 1].close + 1;
                int topLenght = brackets[i].open - brackets[i - 1].close - 1;
                int contentStart = brackets[i].open + 1;
                int contentLenght = brackets[i].close - brackets[i].open - 1;

                var top = input.Substring(topStart, topLenght);
                var content = input.Substring(contentStart, contentLenght);

                var item = new Masset("dummy value");
                this.ClassifyTop(top, item);
                this.ClassifyVars(content, item);
                output.Add(item);
            }
            return output;
        }

        protected void ClassifyTop(string input, Masset output)
        {
            var lines = input.SplitCleanTrim();
            var docLines = new List<string>();
            var headLines = new List<string>();
            foreach (string rawLine in lines)
            {
                string line = rawLine;
                if (this.regexComment.Match(line).Success)
                {
                    do
                    {
                        line = line.Remove(0, 1);
                    } while (this.regexComment.Match(line).Success);
                    docLines.Add(line.Trim());
                }
                else
                    headLines.Add(line);
            }

            docLines.Trim(string.Empty);

            var comment = String.Join("\r\n", docLines);
            var header = String.Join(" ", headLines);

            output.Docs = String.Join("\r\n", docLines);
            this.ClassifyHeader(header, output);
        }

        protected void ClassifyHeader(string input, Masset output)
        {

        }

        protected List<string> ClassifyVars(string input, Masset output)
        {
            var indexList = new List<int>();
            indexList.AddRange(input.IndexesOf(" o "));
            indexList.AddRange(input.IndexesOf(" --> "));
            indexList.Add(input.Length);
            indexList.Sort();


            var temp = new List<string>();
            for (int i = 0; i < indexList.Count() - 1; i++)
            {
                int start = indexList[i];
                int lenght = indexList[i + 1] - indexList[i];

                var section = input.Substring(start, lenght);
                section = string.Join(" ", section.SplitCleanTrim());

                this.ClassifyVar(section);
            }
            return null;
        }

        protected object ClassifyVar(string input)
        {
            string[] sections = input.Split(' ');

            string identifier = sections[0];
            string type = sections[1];
            string name = sections[2];
            string optional = string.Empty;
            string other = string.Empty;

            if (sections.Length > 3)
                optional = sections[3];
            if (sections.Length > 4)
                other = string.Join(" ", sections.ToList().GetRange(4, sections.Length)); //Just incase

            

            return null;
        }
    }
}
