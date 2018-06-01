using master.Files;
using master.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMmain
    {
        private VMmenu menu;
        private FileHandler fileHandler;
        private WindowManager windowManager;

        public VMmain(MainWindow mainWindow)
        {
            this.fileHandler = new FileHandler();
            this.windowManager = new WindowManager(mainWindow);
            this.menu = new VMmenu(windowManager, fileHandler);

        }

        public VMmenu Menu
        {
            get { return this.menu; }
        }
    }
}
