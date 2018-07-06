using master.Basis;
using master.Windows;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels.Windows
{
    class VMcode : MyBindableBase
    {
        private CodeWindow parent;

        private Dictionary<string, Dictionary<string, string>> code;
        public Dictionary<string, Dictionary<string, string>> Code
        {
            get { return this.code; }
        }
        private string selected;
        public string Selected
        {
            get { return this.selected; }
            set
            {
                this.selected = value;
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged("File");
            }
        }

        public DelegateCommand<object> CommandSelectionChanged { get; private set; }

        public VMcode(CodeWindow parent) : base()
        {
            this.parent = parent;
            this.selected = "XXX";

            code = new Dictionary<string, Dictionary<string, string>>
            {
                { "XXX", new Dictionary<string, string>()
                    {
                        { "ecmr1", "code5" },
                        { "ecmr2", "code6" },
                        { "ecmr3", "code7" },
                        { "ecmr4", "code8" },
                    }
                },
                { "YYY", new Dictionary<string, string>()
                    {
                        { "ecmr16", "code50" }
                    }
                }
            };

            this.CommandSelectionChanged = new DelegateCommand<object>(this.Select);
        }

        private void Select(object input)
        {
            this.Selected = input as string;
        }

        public IList<string> Files
        {
            get { return this.code.Keys.ToList(); }
        }

        public Dictionary<string, string> File
        {
            get { return this.code[this.Selected]; }
        }
    }
}
