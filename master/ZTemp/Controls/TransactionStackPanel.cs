using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace master.Windows.Controls
{
    class TransactionStackPanel : StackPanel
    {
        public TransactionStackPanel() : base()
        {
            Button add = new Button()
            {
                Content = "+"
            };
            add.Click += AddNew;
            this.Children.Add(add);
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            //this.Children.Insert(this.Children.Count - 1, new T());
        }
    }
}
