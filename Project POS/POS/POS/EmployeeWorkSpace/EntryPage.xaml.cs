﻿using System.Windows.Controls;
using POS.Repository.Interfaces;
using System.Windows;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for Entry.xaml
    /// </summary>
    public partial class Entry : Page
    {

        public Entry()
        {
            InitializeComponent();

            Unloaded += (sender, args) =>
            {
                ((MainWindow)Window.GetWindow(this)).currentTable = null;
                ((MainWindow)Window.GetWindow(this)).currentChair = null;
            };
        }

       
    }
}
