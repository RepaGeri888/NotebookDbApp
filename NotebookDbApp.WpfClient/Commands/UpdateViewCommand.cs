using NotebookDbApp.WpfClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotebookDbApp.WpfClient.Commands
{
    internal class UpdateViewCommand : ICommand
    {
        private MainWindowViewModel viewModel;

        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter.ToString() == "Notebook")
            {
                viewModel.SelectedViewModel = new NotebookWindowViewModel();
            }
            //else if (parameter.ToString() == "Brand")
            //{
            //    viewModel.SelectedViewModel = new GpuWindowViewModel();
            //}
            //else if (parameter.ToString() == "CPU")
            //{
            //    viewModel.SelectedViewModel = new GpuWindowViewModel();
            //}
            else if (parameter.ToString() == "GPU")
            {
                viewModel.SelectedViewModel = new GpuWindowViewModel();
            }
        }
    }
}
