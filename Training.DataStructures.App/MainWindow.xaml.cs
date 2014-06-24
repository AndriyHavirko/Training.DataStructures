using System;
using System.Collections;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = new Lib.LinkedList<int>();
                var stack = new Lib.Stack<String>();
                
                list.Add(3);
                list.Add(5);
                list.Add(9);
                list.Add(2); 
                list.Add(1);

                for (int i = 0; i < 100000; i++)
                {
                    list.Add(i);
                    list.Sort();
                }
            }
            catch
            {

            }
        }
    }
}