using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using NotebookDbApp.WpfClient.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotebookDbApp.WpfClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private ObservableRecipient selectedViewModel;

        public ObservableRecipient SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                if (value != null)
                {
                    selectedViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainWindowViewModel()
        {
            SelectedViewModel = new NotebookWindowViewModel();
            //UpdateViewCommand = new UpdateViewCommand(this);
            UpdateViewCommand = new RelayCommand<object>((parameter) =>
            {
                if (parameter.ToString() == "Notebook")
                {
                    SelectedViewModel = new NotebookWindowViewModel();
                }
                else if (parameter.ToString() == "Brand")
                {
                    SelectedViewModel = new BrandWindowViewModel();
                }
                else if (parameter.ToString() == "CPU")
                {
                    SelectedViewModel = new CpuWindowViewModel();
                }
                else if (parameter.ToString() == "GPU")
                {
                    SelectedViewModel = new GpuWindowViewModel();
                }
            });
        }
    }
}
