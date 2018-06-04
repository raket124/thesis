using master.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private Dmodel doc;

        readonly VariableTreeViewModel _familyTree;

        public TestWindow()
        {
            InitializeComponent();

            //this.doc = Mfile.KoopmanCTO();
            //var temp = this.doc.GetReferenceTable();


            //_familyTree = new VariableTreeViewModel(rootVariable);
            //base.DataContext = _familyTree;
        }

        private void Test1()
        {
            var a = new Cvariable()
            {
                Name = "ecmr",
                Child = new Cvariable()
                {
                    Name = "goods[j]",
                    Child = new Cvariable()
                    {
                        Name = "vehicle",
                        Child = new Cvariable()
                        {
                            Name = "getIdentifier()"
                        }
                    }
                }
            };

            var b = new Cvariable()
            {
                Name = "transportOrder",
                Child = new Cvariable()
                {
                    Name = "goods[counter]",
                    Child = new Cvariable()
                    {
                        Name = "vehicle",
                        Child = new Cvariable()
                        {
                            Name = "getIdentifier()"
                        }
                    }
                }
            };

            var c = new CconditionSingle()
            {
                A = a,
                B = b,
                Compare = CconditionSingle.COMPARE.not_equal
            };

            var d = new CconditionGroup();
            d.Add(c);


            var output = new Ccondition()
            {
                Condition = d
            }.ToString();



            //if ((ecmr.goods[j].vehicle.getIdentifier() !== transportOrder.goods[counter].vehicle.getIdentifier()) ||
            //((ecmr.goods[j].vehicle.getIdentifier() === transportOrder.goods[counter].vehicle.getIdentifier()) &&
            //(!ecmr.goods[j].cancellation && typeof ecmr.goods[j].cancellation !== 'undefined')))
            //{
            //    return false;
            //}
        }

        private void Test2()
        {
            var a = new Cvariable()
            {
                Name = "ecmr",
                Child = new Cvariable()
                {
                    Name = "goods[j]",
                    Child = new Cvariable()
                    {
                        Name = "vehicle",
                        Child = new Cvariable()
                        {
                            Name = "getIdentifier()"
                        }
                    }
                }
            };
            var b = new Cvariable()
            {
                Name = "transportOrder",
                Child = new Cvariable()
                {
                    Name = "goods[counter]",
                    Child = new Cvariable()
                    {
                        Name = "vehicle",
                        Child = new Cvariable()
                        {
                            Name = "getIdentifier()"
                        }
                    }
                }
            };
            var c = new CconditionSingle()
            {
                A = a,
                B = b,
                Compare = CconditionSingle.COMPARE.not_equal
            };

            var aa = new Cvariable()
            {
                Name = "ecmr",
                Child = new Cvariable()
                {
                    Name = "goods[j]",
                    Child = new Cvariable()
                    {
                        Name = "vehicle",
                        Child = new Cvariable()
                        {
                            Name = "getIdentifier()"
                        }
                    }
                }
            };

            var bb = new Cvariable()
            {
                Name = "transportOrder",
                Child = new Cvariable()
                {
                    Name = "goods[counter]",
                    Child = new Cvariable()
                    {
                        Name = "vehicle",
                        Child = new Cvariable()
                        {
                            Name = "getIdentifier()"
                        }
                    }
                }
            };

            var cc = new CconditionSingle()
            {
                A = aa,
                B = bb,
                Compare = CconditionSingle.COMPARE.equal
            };

            var dd = new CconditionSingle()
            {
                A = aa,
                B = bb,
                Compare = CconditionSingle.COMPARE.equal
            };

            var ddd = new CconditionGroup();
            ddd.Add(dd);


            var d = new CconditionGroup();
            d.Add(c);
            d.Add(cc);
            d.Add(CconditionGroup.COMPARE.or);
            d.Add(ddd);
            d.Add(CconditionGroup.COMPARE.and);


            var output = new Ccondition()
            {
                Condition = d
            }.ToString();



            //if ((ecmr.goods[j].vehicle.getIdentifier() !== transportOrder.goods[counter].vehicle.getIdentifier()) ||
            //((ecmr.goods[j].vehicle.getIdentifier() === transportOrder.goods[counter].vehicle.getIdentifier()) &&
            //(!ecmr.goods[j].cancellation && typeof ecmr.goods[j].cancellation !== 'undefined')))
            //{
            //    return false;
            //}
        }
    }
}
