using System;
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

namespace TestUserTimeEditer
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

        private void btnStartClick(object sender, RoutedEventArgs e)
        {

            SolidColorBrush brush_task1 = new SolidColorBrush(Colors.Red);
            var task1 = Task.Run(() =>
            {
                userRunTime1.Delay(brush_task1);
            });

            SolidColorBrush brush_task2 = new SolidColorBrush(Colors.Green);
            var task2 = Task.Run(() =>
            {
                userRunTime2.Delay(brush_task2);
            });

            SolidColorBrush brush_task3 = new SolidColorBrush(Colors.Blue);
            var task3 = Task.Run(() =>
            {
                userRunTime3.Delay(brush_task3);
            });
        }
    }
}
