using master.Models;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

namespace master.Files
{
    class FileHandler
    {
        protected readonly string defaultFileName = "bcd model";
        protected OpenFileDialog openFileDialog;
        protected SaveFileDialog saveFileDialog;
        protected DataModel model;
        protected string currentFile;

        protected readonly DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DataModel));

        public FileHandler()
        {
            this.openFileDialog = new OpenFileDialog()
            {
                Filter = "Blockchain definition (*.bcd)|*.bcd"
            };
            this.saveFileDialog = new SaveFileDialog()
            {
                AddExtension = true,
                DefaultExt = ".bcd",
                FileName = this.defaultFileName
            };

            this.model = new DataModel();
            this.currentFile = string.Empty;
        }

        public void New()
        {
            this.currentFile = string.Empty;
            this.model = new DataModel();
        }

        public void Demo()
        {
            this.currentFile = string.Empty;
            //TODO create demo datamodel from code, instead of empty model
            this.model = new DataModel();
        }

        public void Open()
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.currentFile = this.openFileDialog.FileName;
                this.Open(this.currentFile);
            }
        }

        private void Open(string file)
        {
            //TODO try catch invalid files
            this.model = this.JsonToObject(File.ReadAllText(file));
        }

        public void Save()
        {
            if (this.currentFile == string.Empty)
            {
                this.SaveAs();
                return;
            }
            this.Save(this.currentFile);
        }

        public void SaveAs()
        {
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.currentFile = this.saveFileDialog.FileName;
                this.Save(this.currentFile);
            }
        }

        private void Save(string file)
        {
            File.WriteAllText(file, this.ObjectToJson(this.model));
        }

        public DataModel Model
        {
            get { return this.model; }
        }

        private string ObjectToJson(DataModel input)
        {
            var memory = new MemoryStream();
            serializer.WriteObject(memory, input);
            var json = memory.ToArray();
            memory.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        private DataModel JsonToObject(string input)
        {
            var memory = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var output = serializer.ReadObject(memory) as DataModel;
            memory.Close();
            return output;
        }
    }
}
