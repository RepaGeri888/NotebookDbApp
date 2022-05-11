using AJ4GRZ_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using NotebookDbApp.WpfClient.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NotebookDbApp.WpfClient.ViewModels
{
    internal class CpuWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        #region Cpus
        public RestCollection<Cpu> Cpus { get; set; }

        private Cpu selectedCpu;

        public Cpu SelectedCpu
        {
            get { return selectedCpu; }
            set
            {
                if (value != null)
                {
                    selectedCpu = new Cpu()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        ClockSpeed = value.ClockSpeed,
                        Cores = value.Cores,
                        Threads = value.Threads
                    };
                    OnPropertyChanged();
                    (DeleteCpuCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateCpuCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateCpuCommand { get; set; }
        public ICommand DeleteCpuCommand { get; set; }
        public ICommand UpdateCpuCommand { get; set; }
        #endregion

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public CpuWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Cpus = new RestCollection<Cpu>("http://localhost:32747/", "cpu", "hub");
                //var selectedGpus = Gpus.Where(b => b.Id == 2).FirstOrDefault();
                //;
                //;
                //Collection = SelectCollection<Notebook>(Notebooks);
                //;
                #region CpuCommands
                CreateCpuCommand = new RelayCommand(() =>
                {
                    if (SelectedCpu == null)
                    {
                        SelectedCpu = new();
                    }
                    bool? isSaved = new CpuEditorWindow(SelectedCpu).ShowDialog();
                    if ((bool)isSaved)
                    {
                        try
                        {
                            Cpus.Add(new Cpu()
                            {
                                Name = SelectedCpu.Name,
                                ClockSpeed = SelectedCpu.ClockSpeed,
                                Cores = SelectedCpu.Cores,
                                Threads = SelectedCpu.Threads,
                            });
                        }
                        catch (InvalidOperationException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                    }
                });

                UpdateCpuCommand = new RelayCommand(() =>
                {
                    try
                    {
                        new CpuEditorWindow(SelectedCpu).ShowDialog();
                        Cpus.Update(SelectedCpu);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                },
                () =>
                {
                    return SelectedCpu != null;
                }
                );

                DeleteCpuCommand = new RelayCommand(() =>
                {
                    Cpus.Delete(SelectedCpu.Id);
                },
                () =>
                {
                    return SelectedCpu != null;
                });
                #endregion
            }
        }
    }
}
