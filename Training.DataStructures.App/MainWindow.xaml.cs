using System;
using System.Collections;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = new Lib.LinkedList<int>();
                var stack = new Lib.Stack<String>();
                var arrlist = new Lib.ArrayList<String>();

                var random = new Random();
                int size = 10000000;

                for (int i = 0; i < size; i++)
                {
                    arrlist.Add(random.Next(size).ToString());
                }
                arrlist.MergeSort();
            }
            catch (Exception ex)
            {

            }
        }
    }
}