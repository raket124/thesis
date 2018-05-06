using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using master.Models;
using System.Text.RegularExpressions;

namespace master.FileReaders
{
    abstract class Rbase
    {
        protected readonly Regex regexNewLine = new Regex(@"\r\n|\r|\n");
        protected readonly Regex regexComment = new Regex(@"^[*|/|\\].*");


        //protected Dictionary<string>
        protected List<string> fileList;
        protected string extension;

        public Rbase(string extension)
        {
            this.fileList = new List<string>();
            this.extension = extension;
        }

        //public List<Object> ProcessFiles()
        //{
        //    List<Object> returnList = new List<object>();
        //    foreach (string file in this.fileList)
        //    {
        //        returnList.Add(this.ProcessFile());
        //    }
        //    return returnList;
        //}

        public abstract Mfile ProcessFile(int index);

        protected IEnumerable<string> GetFileIterator(int index)
        {
            return File.ReadLines(this.fileList[index]);
        }

        public void AddFiles(List<string> files)
        {
            foreach (string file in files)
                this.AddFile(file);
        }

        public void AddFile(string file)
        {
            if (this.fileList.Contains(file))
                return; //Skip existing files

            string extension = Path.GetExtension(file);
            if (!extension.Equals(this.extension))
                throw new FileFormatException(
                    string.Format("This filereader does not support {0}\nOnly {1} files are supported.", extension, this.extension));

            this.fileList.Add(file);
        }

        public void RemoveFile(int index)
        {
            if (index < this.fileList.Count)
                this.fileList.RemoveAt(index);
        }

        public void RemoveFile(string file)
        {
            this.fileList.Remove(file);
        }
    }
}
