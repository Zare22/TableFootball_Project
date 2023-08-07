using System.Windows;
using System.Windows.Controls;
using TableFootball.Pages;
using TableFootball.ViewModels;

namespace TableFootball
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Frame.Navigate(new HomePage(new LeagueViewModel()) { Frame = Frame });
        }
    }
}
