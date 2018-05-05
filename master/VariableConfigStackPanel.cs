using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace master
{
    class VariableConfigStackPanel : StackPanel
    {
        public VariableConfigStackPanel() : base()
        {
            this.Children.Add(new VariableStackPanel());
            this.Children.Add(new VariableStackPanel());
            this.Children.Add(new VariableStackPanel());
            this.Children.Add(new VariableStackPanel());


            Button add = new Button()
            {
                Content = "+"
            };
            add.Click += AddNew;

            this.Children.Add(add);
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            this.Children.Insert(this.Children.Count - 1, new VariableStackPanel());
        }
    }
}
