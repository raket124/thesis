using master.Files;
using master.Windows;
using master.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.ViewModels
{
    class VMmain
    {
        private FileHandler fileHandler;
        private WindowManager windowManager;

        private VMmenu menu;
        private VMdataModel model;


        private ObservableCollection<VMBbase> list1;
        private ObservableCollection<VMBbase> list2;

        public VMmain(MainWindow mainWindow)
        {
            this.fileHandler = new FileHandler();
            this.windowManager = new WindowManager(mainWindow);

            this.menu = new VMmenu(windowManager, fileHandler);
            this.model = new VMdataModel(this.fileHandler.Model);


            this.list1 = new ObservableCollection<VMBbase>();
            this.list2 = new ObservableCollection<VMBbase>
            {
                new Y("Y"),
                new Z("Z")
            };
        }

        public VMmenu Menu
        {
            get { return this.menu; }
        }

        public VMdataModel Model
        {
            get { return this.model; }
        }

        public ObservableCollection<VMBbase> List1
        {
            get { return this.list1; }
        }

        public ObservableCollection<VMBbase> List2
        {
            get { return this.list2; }
        }
    }
}
