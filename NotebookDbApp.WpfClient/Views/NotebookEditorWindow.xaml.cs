using AJ4GRZ_HFT_2021221.Models;
using NotebookDbApp.WpfClient.ViewModels;
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
using System.Windows.Shapes;

namespace NotebookDbApp.WpfClient.Views
{
    /// <summary>
    /// Interaction logic for NotebookEditorWindow.xaml
    /// </summary>
    public partial class NotebookEditorWindow : Window
    {
        public NotebookEditorWindow(Notebook notebook)
        {
            InitializeComponent();
            var vm = new NotebookEditorWindowViewModel();
            vm.Setup(notebook);
            this.DataContext = vm;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in stack.Children)
            {
                if (item is TextBox t)
                {
                    t.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
