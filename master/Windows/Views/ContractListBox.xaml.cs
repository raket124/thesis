﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace master.Views
{
    /// <summary>
    /// Interaction logic for ContractListBox.xaml
    /// </summary>
    public partial class ContractListBox : UserControl
    {
        public ContractListBox()
        {
            InitializeComponent();
        }

        public ListBox ListBox
        {
            get { return this.listBox; }
            set { this.listBox = value; }
        }
    }
}