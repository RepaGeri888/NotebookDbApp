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
    public class GpuWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        #region Gpus
        public RestCollection<Gpu> Gpus { get; set; }
        private Gpu selectedGpu;

        public Gpu SelectedGpu
        {
            get { return selectedGpu; }
            set
            {
                if (value != null)
                {
                    selectedGpu = new Gpu()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        BusWidth = value.BusWidth,
                        MemorySize = value.MemorySize
                    };
                    OnPropertyChanged();
                    (DeleteGpuCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateGpuCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateGpuCommand { get; set; }
        public ICommand DeleteGpuCommand { get; set; }
        public ICommand UpdateGpuCommand { get; set; }
        #endregion

        //#region Cpus
        //public RestCollection<Cpu> Cpus { get; set; }

        //private Cpu selectedCpu;

        //public Cpu SelectedCpu
        //{
        //    get { return selectedCpu; }
        //    set {
        //        if (value != null)
        //        {
        //            SelectedCpu = new Cpu()
        //            {
        //                Id = value.Id,
        //                Name = value.Name,
        //                ClockSpeed = value.ClockSpeed,
        //                Cores = value.Cores,
        //                Threads = value.Threads,
        //                Notebooks = value.Notebooks
        //            };
        //            OnPropertyChanged();
        //            (DeleteCpuCommand as RelayCommand).NotifyCanExecuteChanged();
        //        }
        //    }
        //}

        //public ICommand CreateCpuCommand { get; set; }
        //public ICommand DeleteCpuCommand { get; set; }
        //public ICommand UpdateCpuCommand { get; set; }
        //#endregion

        //#region Brands
        //public RestCollection<Brand> Brands { get; set; }
        //private Brand selectedBrand;

        //public Brand SelectedBrand
        //{
        //    get { return selectedBrand; }
        //    set { SetProperty(ref selectedBrand, value); }
        //}

        //public ICommand CreateBrandCommand { get; set; }
        //public ICommand DeleteBrandCommand { get; set; }
        //public ICommand UpdateBrandCommand { get; set; }
        //#endregion

        //#region Gpus
        //public RestCollection<Gpu> Gpus { get; set; }
        //private Gpu selectedGpu;

        //public Gpu SelectedGpu
        //{
        //    get { return selectedGpu; }
        //    set { SetProperty(ref selectedGpu, value); }
        //}

        //public ICommand CreateGpuCommand { get; set; }
        //public ICommand DeleteGpuCommand { get; set; }
        //public ICommand UpdateGpuCommand { get; set; }
        //#endregion


        //public RestCollection<WindowItem<Object>> SelectCollection<T>(RestCollection<T> collection)
        //{
        //    //WindowItem<T> windowItem = new WindowItem<T>(collection);
        //    //RestCollection<WindowItem<T>> alma = collection as RestCollection<WindowItem<T>>;
        //    RestCollection<WindowItem<Object>> Collection = collection as RestCollection<WindowItem<Object>>;
        //    return Collection;
        //}


        //public ICommand SelectGpusCommand { get; set; }
        //public ICommand SelectNotebooksCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public GpuWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Gpus = new RestCollection<Gpu>("http://localhost:32747/", "gpu", "hub");

                #region GpuCommands
                CreateGpuCommand = new RelayCommand(() =>
                {
                    if (SelectedGpu == null)
                    {
                        SelectedGpu = new();
                    }
                    bool? isSaved = new GpuEditorWindow(SelectedGpu).ShowDialog();
                    if ((bool)isSaved)
                    {
                        try
                        {
                            Gpus.Add(new Gpu()
                            {
                                Name = SelectedGpu.Name,
                                BusWidth = SelectedGpu.BusWidth,
                                MemorySize = SelectedGpu.MemorySize
                            });
                        }
                        catch (InvalidOperationException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                    }
                });

                UpdateGpuCommand = new RelayCommand(() =>
                {
                    try
                    {
                        new GpuEditorWindow(SelectedGpu).ShowDialog();
                        //var window = new MainWindow();
                        //foreach (var item in window.lBox.Items)
                        //{
                        //    if (item is Label l)
                        //    {
                        //        var property = l.GetBindingExpression(Label.ContentProperty);
                        //    }
                        //}
                        ;
                        Gpus.Update(SelectedGpu);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                },
                () =>
                {
                    return SelectedGpu != null;
                });

                DeleteGpuCommand = new RelayCommand(() =>
                {
                    Gpus.Delete(SelectedGpu.Id);
                },
                () =>
                {
                    return SelectedGpu != null;
                });
                //SelectedNotebook = new Notebook();
                #endregion

                //#region GpuCommands
                //CreateGpuCommand = new RelayCommand(() =>
                //{
                //    if (SelectedGpu == null)
                //    {
                //        SelectedGpu = new();
                //    }
                //    try
                //    {
                //        Gpus.Add(new Gpu()
                //        {
                //            Name = SelectedGpu.Name,
                //            BusWidth = SelectedGpu.BusWidth,
                //            MemorySize = SelectedGpu.MemorySize
                //        });
                //    }
                //    catch (InvalidOperationException ex)
                //    {
                //        ErrorMessage = ex.Message;
                //    }
                //});

                //UpdateGpuCommand = new RelayCommand(() =>
                //{
                //    try
                //    {
                //        Gpus.Update(SelectedGpu);
                //    }
                //    catch (ArgumentException ex)
                //    {
                //        ErrorMessage = ex.Message;
                //    }

                //});

                //DeleteGpuCommand = new RelayCommand(() =>
                //{
                //    Gpus.Delete(SelectedGpu.Id);
                //},
                //() =>
                //{
                //    return SelectedGpu != null;
                //});

                //SelectGpusCommand = new RelayCommand(() =>
                //    Collection = SelectCollection<Gpu>(Gpus)
                //);
                //#endregion
            }
        }
    }
}
