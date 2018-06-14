using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace master.Windows
{
    public partial class GroupNameWindow : Window
    {
        public DelegateCommand<object> CommandOk { get; private set; }
        public DelegateCommand CommandTextChanged { get; private set; }

        private string question;
        public string Question
        {
            get { return this.question; }
        }
        private string answer;
        public string Answer
        {
            get { return this.answer; }
            set { this.answer = value; }
        }
        private List<string> existingNames;

        public GroupNameWindow(string question) : this(question, new List<string>()) { }

        public GroupNameWindow(string question, List<string> existingNames)
        {
            InitializeComponent();
            this.DataContext = this;

            this.question = question;
            this.existingNames = existingNames;
            this.answer = string.Empty;

            this.CommandOk = new DelegateCommand<object>(this.Ok, this.CanOk);
            this.CommandTextChanged = new DelegateCommand(this.TextChanged);
        }

        private void Ok(object input)
        {
            this.DialogResult = true;
        }

        private bool CanOk(object input)
        {
            return true;

            //TODO fix below
            //var value = input as string;
            //return value != null && 
            //       value != string.Empty && 
            //       !this.existingNames.Contains(value);
        }

        private void TextChanged()
        {
            CommandOk.RaiseCanExecuteChanged();
        }
    }
}
