﻿using master.Models;
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
    public partial class TestWindow : Window
    {
        //private Dmodel doc;
        //readonly VariableTreeViewModel _familyTree;

        public TestWindow()
        {
            InitializeComponent();

            //this.doc = Mfile.KoopmanCTO();
            //var temp = this.doc.GetReferenceTable();


            //_familyTree = new VariableTreeViewModel(rootVariable);
            //base.DataContext = _familyTree;
        }
    }
}
