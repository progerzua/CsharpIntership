
// Just simple demo.

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
using System.Threading;

namespace Task2_withDemo_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string methodForTest(string message)
        {
            Thread.Sleep(3500);
            return "Hello " + message;
        }


        private void TestActionClick(object sender, RoutedEventArgs e)
        {
            Task task1 = Task.Run(() => methodForTest("test1"));
            Task task2 = task1.ContinueOnUI(() => { indication.Text = "Have a nice day!"; });
            indication.Text = "Working...";


        }

        private void TestActionTaskClick(object sender, RoutedEventArgs e)
        {
            var backgroundScheduler = TaskScheduler.Default;
            Task.Factory.StartNew((t) => methodForTest("test1"), backgroundScheduler).ContinueOnUI((t) => indication.Text = "Action<Task> says hello for you!");
            indication.Text = "Working...";
        }

        private void TestFuncTClick(object sender, RoutedEventArgs e)
        {
            Task<string> task1 = Task.Run<string>(() => methodForTest(" ...world?"));
            Task<string> task2 = task1.ContinueOnUI(() => indication.Text = task1.Result);
            indication.Text = "Working..."; 
        }

        private void TestFuncTaskTClick(object sender, RoutedEventArgs e)
        {
            Task<string> task1 = Task.Run<string>(() => methodForTest(" guys!"));
            Task<string> task2 = task1.ContinueOnUI((t) => indication.Text = task1.Result);
            indication.Text = "Working...";
        }
    }
}
