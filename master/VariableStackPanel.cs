using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace master
{
    class VariableStackPanel : StackPanel
    {
        //public string VariableName
        //{
        //    get { return this.name.Text; }
        //    set { this.name.Text = value; }
        //}
        /*key = display name, value = actual type*/
        enum Types { String, Integer, Double, Long, Date, Time, DateTime, Boolean, Enum };
        private Dictionary<string, Types> availableTypes;

        private Button delete;
        private ComboBox type;
        private StackPanel panel;

        public VariableStackPanel() : base()
        {
            this.Orientation = Orientation.Horizontal;
            this.Margin = new Thickness(2);
            this.availableTypes = new Dictionary<string, Types>
            {
                { "Text", Types.String },
                { "Number", Types.Integer },
                { "Date",  Types.Date },
                { "Time",  Types.Time },
                { "Date & Time",  Types.DateTime },
                { "Choice",  Types.Enum },
            };

            this.delete = this.ButtonDelete();
            this.panel = this.StackPanel();
            this.type = this.ComboBoxTypes();
            
            this.Children.Add(this.delete);
            this.Children.Add(this.type);
            this.Children.Add(this.panel);
        }

        private Button ButtonDelete()
        {
            Button button = new Button()
            {
                Content = "X"
            };
            button.Click += this.RemoveSelf;
            return button;
        }

        private ComboBox ComboBoxTypes()
        {
            ComboBox comboBox = new ComboBox()
            {
                SelectedIndex = 0,
                Width = 150
            };
            comboBox.SelectionChanged += this.SelectionChanged;
            foreach (String s in this.availableTypes.Keys)
            {
                comboBox.Items.Add(new ComboBoxItem()
                {
                    Content = s
                });
            }
            return comboBox;
        }

        private StackPanel StackPanel()
        {
            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            return stackPanel;
        }


        private void RemoveSelf(object sender, RoutedEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox control = sender as ComboBox;
            ComboBoxItem selectedItem = control.SelectedItem as ComboBoxItem;
            this.TypeToWPF(selectedItem.Content.ToString());
        }

        private void TypeToWPF(String type)
        {
            this.panel.Children.Clear();
            switch (this.availableTypes[type])
            {
                case Types.String:
                    this.panel.Children.Add(new TextBox()
                    {
                        Text = "Variable1"
                    });
                    break;
                case Types.Integer:
                    this.panel.Children.Add(new IntegerUpDown()
                    {
                        Value = 0
                    });
                    break;
                case Types.Double:
                    this.panel.Children.Add(new DoubleUpDown()
                    {
                        Value = 0
                    });
                    break;
                case Types.Date:
                    this.panel.Children.Add(new DatePicker()
                    {
                        SelectedDate = DateTime.Today
                    });
                    break;
                case Types.Time:
                    this.panel.Children.Add(new TimePicker()
                    {
                        Value = DateTime.Now
                    });
                    break;
                case Types.DateTime:
                    this.panel.Children.Add(new DateTimePicker()
                    {
                        Value = DateTime.Now
                    });
                    break;
                case Types.Enum:
                    ComboBox comboBox = new ComboBox()
                    {
                        SelectedIndex = 0,
                        Width = 150
                    };
                    foreach (String s in new string[] { "Apple", "Banana", "Grape" })
                    {
                        comboBox.Items.Add(new ComboBoxItem()
                        {
                            Content = s
                        });
                    }
                    this.panel.Children.Add(comboBox);
                    break;


                default:
                    break;
            }


        }

    }
}
