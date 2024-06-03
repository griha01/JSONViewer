using JSONViewerProject.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace JSONViewerProject
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
