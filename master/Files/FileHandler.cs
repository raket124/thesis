using master.Models;
using master.ViewModels;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

namespace master.Files
{
    class FileHandler
    {
        //TODO replace parent and model code to a more solid solution

        protected VMmain parent;

        protected readonly string defaultFileName = "bcd model";
        protected OpenFileDialog openFileDialog;
        protected SaveFileDialog saveFileDialog;
        public MasterModel Model
        {
            get { return this.parent.Model.Root; }
            set { this.parent.Model = new VMmasterModel(value); }
        }
        protected string currentFile;

        protected readonly DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MasterModel));

        public FileHandler(VMmain parent)
        {
            this.parent = parent;

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

            this.New();
        }

        public void New()
        {
            this.currentFile = string.Empty;
            this.Model = new MasterModel();
        }

        public void Demo()
        {
            this.currentFile = string.Empty;

            //TODO replace with more decent code
            var temp = new MasterModel();
            temp.SetupDemo();
            this.Model = temp;
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
            this.Model = this.JsonToObject(File.ReadAllText(file));
        }

        public void Save()
        {
            if (this.currentFile == string.Empty)
                this.SaveAs();
            else
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
            File.WriteAllText(file, this.ObjectToJson(this.Model));
        }

        private string ObjectToJson(MasterModel input)
        {
            var memory = new MemoryStream();
            serializer.WriteObject(memory, input);
            var json = memory.ToArray();
            memory.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        private MasterModel JsonToObject(string input)
        {
            var memory = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var output = serializer.ReadObject(memory) as MasterModel;
            memory.Close();
            return output;
        }
    }
}
