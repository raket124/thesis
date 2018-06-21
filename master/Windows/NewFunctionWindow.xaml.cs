using master.Models.Contract;
using master.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for NewFunctionWindow.xaml
    /// </summary>
    public partial class NewFunctionWindow : Window
    {
        //private Function function;
        ////public Function Function
        ////{
        ////    get { return this.function; }
        ////}
        //private List<string> participants;
        //public List<string> Participants
        //{
        //    get { return this.participants; }
        //    set { this.participants = value; }
        //}
        //private List<string> names;

        public NewFunctionWindow()
        {
            InitializeComponent();

            //this.function = new Function(string.Empty, Function.ACCESSIBILITY.Public);
            //this.participants = existingParticipants;
            //this.names = existingNames;

            //this.DataContext = this;
        }

        //public string FunctionName
        //{
        //    get { return this.function.Name; }
        //    set
        //    {
        //        this.function.Name = value;
        //        this.NotifyPropertyChanged();
        //    }
        //}

        //public Function.ACCESSIBILITY Accessibility
        //{
        //    get { return this.function.Access; }
        //    set
        //    {
        //        this.function.Access = value;
        //        this.NotifyPropertyChanged();
        //        this.NotifyPropertyChanged("EnableUserSelect");
        //    }
        //}

        //public IList<Function.ACCESSIBILITY> Accessibilities
        //{
        //    get { return EnumUtil.EnumToList<Function.ACCESSIBILITY>(); }
        //}

        //public bool EnableUserSelect
        //{
        //    get
        //    {
        //        switch(this.function.Access)
        //        {
        //            case Function.ACCESSIBILITY.Public:
        //            case Function.ACCESSIBILITY.Private:
        //                return false;
        //            case Function.ACCESSIBILITY.Controlled:
        //                return true;
        //            default:
        //                throw new Exception("This should not happen.");
        //        }
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}
