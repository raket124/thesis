using master.Basis;
using master.Files;
using master.Windows;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace master.ViewModels.Views
{
    public class VMmenu : MyBindableBase
    {
        private WindowManager windowManager;
        private FileHandler fileHandler;

        public ICommand FileNew { get; private set; }
        public ICommand FileDemo { get; private set; }
        public ICommand FileOpen { get; private set; }
        public ICommand FileSave { get; private set; }
        public ICommand FileSaveAs { get; private set; }
        public ICommand FileExit { get; private set; }

        public ICommand CodeWindow { get; private set; }
        public ICommand GraphWindow { get; private set; }
        public ICommand TestWindow { get; private set; }

        public VMmenu(WindowManager windowManager, FileHandler fileHandler)
        {
            this.windowManager = windowManager;
            this.fileHandler = fileHandler;

            this.FileNew = new DelegateCommand(this.fileHandler.New);
            this.FileDemo = new DelegateCommand(this.fileHandler.Demo);
            this.FileOpen = new DelegateCommand(this.fileHandler.Open);
            this.FileSave = new DelegateCommand(this.fileHandler.Save);
            this.FileSaveAs = new DelegateCommand(this.fileHandler.SaveAs);
            this.FileExit = new DelegateCommand(this.windowManager.CloseMainWindow);

            this.CodeWindow = new DelegateCommand(this.windowManager.ShowCodeWindow);
            this.GraphWindow = new DelegateCommand(this.windowManager.ShowGraphWindow);
            this.TestWindow = new DelegateCommand(this.windowManager.ShowTestWindow);
        }
    }
}
