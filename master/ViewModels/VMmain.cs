using master.Files;
using master.Windows;
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


        private ObservableCollection<X> list1;
        private ObservableCollection<X> list2;

        public VMmain(MainWindow mainWindow)
        {
            this.fileHandler = new FileHandler();
            this.windowManager = new WindowManager(mainWindow);

            this.menu = new VMmenu(windowManager, fileHandler);
            this.model = new VMdataModel(this.fileHandler.Model);


            this.list1 = new ObservableCollection<X>();
            this.list2 = new ObservableCollection<X>();


            this.list1.Add(new X("A"));
            this.list1.Add(new X("B"));
            this.list1.Add(new X("C"));

            this.list2.Add(new X("1"));
            this.list2.Add(new X("2"));
            this.list2.Add(new X("3"));
        }

        public VMmenu Menu
        {
            get { return this.menu; }
        }

        public VMdataModel Model
        {
            get { return this.model; }
        }

        public ObservableCollection<X> List1
        {
            get { return this.list1; }
        }

        public ObservableCollection<X> List2
        {
            get { return this.list2; }
        }
    }

    class X : ICloneable
    {
        private string x;

        public X(string input)
        {
            this.x = input;
        }

        public object Clone()
        {
            return new X(this.x);
        }

        public override string ToString()
        {
            return x;
        }
    }
}
