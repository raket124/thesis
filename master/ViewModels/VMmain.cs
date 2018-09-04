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
using master.ViewModels.Windows.Views;
using master.Basis;
using master.ViewModels.Contract.Block;
using master.ViewModels.Contract.Block.Blocks;
using master.ViewModels.Contract.Block.Blocks.Custom;
using master.Models.Contract.Block.Blocks.Custom;
using master.ViewModels.Contract.Block.Combinations;
using master.Models.Contract.Block.Combinations;

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
            this.windowManager = new WindowManager(this.fileHandler.Model, mainWindow);

            this.menu = new VMmenu(windowManager, fileHandler);
            this.model = new VMmasterModel(this.fileHandler.Model);


            this.list2 = new ObservableCollection<VMbase>
            {
                new VMassign(new MyAssign(), null),
                new VMelse(new MyElse(), null),
                new VMend(new MyEnd(), null),
                new VMerror(new MyError(), null),
                new VMforeach(new MyForeach(), null),
                new VMif(new MyIf()
                {
                    Condition = new Models.Contract.Block.Conditioning.Condition()
                    {
                        Conditions = new ObservableCollection<Models.Contract.Block.Conditioning.ConditionBase>()
                        {
                            new Models.Contract.Block.Conditioning.ConditionBase()
                        },
                        Groups = new ObservableCollection<Models.Contract.Block.Conditioning.ConditionGroup>()
                        {
                            new Models.Contract.Block.Conditioning.ConditionGroup()
                            {
                                Conditions = new ObservableCollection<string>()
                                {
                                    string.Empty,
                                    string.Empty
                                },
                                Connectors = new ObservableCollection<Models.Contract.Block.Conditioning.ConditionGroup.COMPARE>()
                                {
                                    Models.Contract.Block.Conditioning.ConditionGroup.COMPARE.and
                                }
                            }
                        },
                        Value = new Models.Contract.Block.Conditioning.ConditionGroup()
                        {
                            Conditions = new ObservableCollection<string>()
                            {
                                string.Empty,
                                string.Empty
                            },
                            Connectors = new ObservableCollection<Models.Contract.Block.Conditioning.ConditionGroup.COMPARE>()
                            {
                                Models.Contract.Block.Conditioning.ConditionGroup.COMPARE.and
                            }
                        }
                    }
                }, null),
                new VMlog(new MyLog(), null),
                new VMuseRegistry(new MyRegistry(), null),

                new VMtotalEcmrs(new MyTotalEcmrs(), null),
                
                new VMcreation(new MyCreation(), null),
                new VMifError(new MyIfError(), null),
                new VMinput(new MyInput(), null),
                new VMmodification(new MyModification(), null),
                new VMvalidation(new MyValidation(), null),
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
