using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
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

using Training.DataStructures.Lib;


namespace Training.DataStructures.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Lib.LinkedList<String> items;

        public MainWindow()
        {
            InitializeComponent();
            items = new Lib.LinkedList<String>();
            try
            {
                DbExporter.ReadFromDbIntoCollection(items, ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            }
            catch (Exception exception)
            {
                ErrorTextLable.Content = exception.Message;
            }
        }

        private void AddNewItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                items.Add(NewItemTextBox.Text);
                NewItemTextBox.Text = String.Empty;
            }
            catch (Exception exception)
            {
                ErrorTextLable.Content = exception.Message;
            }
        }

        private void SavaToDbButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DbExporter.SaveLinkeListToDb(items, ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            }
            catch (Exception exception)
            {
                ErrorTextLable.Content = exception.Message;
            }
        }
    }
}