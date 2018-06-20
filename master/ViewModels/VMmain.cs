using master.Files;
using master.Windows;
using master.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using master.Models.Contract.Block;
using master.Models.Contract.Block.Blocks;
using master.ViewModels.Views;
using master.Basis;
using master.ViewModels.Contract.Block;
using master.ViewModels.Contract.Block.Blocks;
using master.ViewModels.Contract.Block.Conditioning;

namespace master.ViewModels
{
    class VMmain : MyBindableBase
    {
        private FileHandler fileHandler;
        private WindowManager windowManager;

        private VMmenu menu;
        private VMmasterModel model;
        public VMmasterModel Model
        {
            get { return this.model; }
            set
            {
                this.model = value;
                this.NotifyPropertyChanged();
            }
        }

        private ObservableCollection<VMbase> list2;

        public VMmain(MainWindow mainWindow)
        {
            this.fileHandler = new FileHandler(this);
            this.windowManager = new WindowManager(mainWindow);

            this.menu = new VMmenu(windowManager, fileHandler);
            this.model = new VMmasterModel(this.fileHandler.Model);


            this.list2 = new ObservableCollection<VMbase>
            {
                new VMassign(new MyAssign(null)),
                new VMinput(new MyInput(null){
                    Vars = new ObservableCollection<Variable>()
                    {
                        new Variable()
                    }
                }),
                new VMlog(new MyLog(null)),
                new VMif(null),
                new VMelseIf(null),
                new VMelse(null),
                new VMend(null),
                new VMuseRegistry(new MyUseRegistry(null))
            };
        }

        public VMmenu Menu
        {
            get { return this.menu; }
        }

        public ObservableCollection<VMbase> List2
        {
            get { return this.list2; }
        }
    }
}
