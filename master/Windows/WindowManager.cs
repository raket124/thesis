﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace master.Windows
{
    class WindowManager
    {
        private MainWindow mainWindow;
        private GraphWindow graphWindow;
        private CodeWindow codeWindow;
        private TestWindow testWindow;

        public WindowManager(MainWindow main)
        {
            this.mainWindow = main;
            this.graphWindow = null;
            this.codeWindow = null;
            this.testWindow = null;
        }

        public void ShowGraphWindow()
        {
            if (this.WindowExist(this.graphWindow))
                this.graphWindow.Focus();
            else
            {
                this.graphWindow = new GraphWindow();
                this.graphWindow.Show();
            }
        }

        public void ShowCodeWindow()
        {
            if (this.WindowExist(this.codeWindow))
                this.codeWindow.Focus();
            else
            {
                this.codeWindow = new CodeWindow();
                this.codeWindow.Show();
            }
        }

        public void ShowTestWindow()
        {
            if (this.WindowExist(this.testWindow))
                this.testWindow.Focus();
            else
            {
                this.testWindow = new TestWindow();
                this.testWindow.Show();
            }
        }

        public void CloseMainWindow()
        {
            this.mainWindow.Close();
        }

        private bool WindowExist(Window window)
        {
            return window != null && window.IsLoaded;
        }
    }
}