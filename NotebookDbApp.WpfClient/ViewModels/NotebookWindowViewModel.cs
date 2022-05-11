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
    public class NotebookWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        //public RestCollection<WindowItem<Object>> Collection { get; set; }
        //private Object selectedItem;

        //public Object SelectedItem
        //{
        //    get { return selectedItem; }
        //    set {
        //        if (value != null)
        //        {
        //            selectedItem = new Object();
        //            OnPropertyChanged();
        //            //(DeleteNotebookCommand as RelayCommand).NotifyCanExecuteChanged();
        //        }
        //    }
        //}

        public RestCollection<Brand> Brands { get; set; }
        public RestCollection<Gpu> Gpus { get; set; }
        public RestCollection<Cpu> Cpus { get; set; }

        #region Notebooks
        public RestCollection<Notebook> Notebooks { get; set; }
        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                if (value != null)
                {
                    selectedNotebook = new Notebook()
                    {
                        Id = value.Id,
                        Brand = /*value.Brand, */Brands.Where(b => b.Id == value.BrandId).FirstOrDefault(),
                        BrandId = value.BrandId,
                        Model = value.Model,
                        Cpu = Cpus.Where(b => b.Id == value.CpuId).FirstOrDefault(),
                        CpuId = value.CpuId,
                        Gpu = Gpus.Where(b => b.Id == value.GpuId).FirstOrDefault(),
                        GpuId = value.GpuId,
                        Ram = value.Ram,
                        Storage = value.Storage,
                        Price = value.Price
                    };
                    OnPropertyChanged();
                    (DeleteNotebookCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateNotebookCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateNotebookCommand { get; set; }
        public ICommand DeleteNotebookCommand { get; set; }
        public ICommand UpdateNotebookCommand { get; set; }
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

        public NotebookWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Notebooks = new RestCollection<Notebook>("http://localhost:32747/", "notebook", "hub");
                Brands = new RestCollection<Brand>("http://localhost:32747/", "brand", "hub");
                Gpus = new RestCollection<Gpu>("http://localhost:32747/", "gpu", "hub");
                Cpus = new RestCollection<Cpu>("http://localhost:32747/", "cpu", "hub");
                //var selectedGpus = Gpus.Where(b => b.Id == 2).FirstOrDefault();
                //;
                //;
                //Collection = SelectCollection<Notebook>(Notebooks);
                //;
                #region NotebookCommands
                CreateNotebookCommand = new RelayCommand(() =>
                {
                    if (SelectedNotebook == null)
                    {
                        SelectedNotebook = new();
                    }
                    bool? isSaved = new NotebookEditorWindow(SelectedNotebook).ShowDialog();
                    if ((bool)isSaved)
                    {
                        try
                        {
                            Notebooks.Add(new Notebook()
                            {
                                BrandId = SelectedNotebook.BrandId,
                                //Brand = SelectedNotebook.Brand,
                                Model = SelectedNotebook.Model,
                                CpuId = SelectedNotebook.CpuId,
                                //Cpu = SelectedNotebook.Cpu,
                                GpuId = SelectedNotebook.GpuId,
                                //Gpu = SelectedNotebook.Gpu,
                                Ram = SelectedNotebook.Ram,
                                Storage = SelectedNotebook.Storage,
                                Price = SelectedNotebook.Price
                            }); ;
                        }
                        catch (InvalidOperationException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                    }
                    //Notebooks = new RestCollection<Notebook>("http://localhost:32747/", "notebook", "hub");
                });

                UpdateNotebookCommand = new RelayCommand(() =>
                {
                    try
                    {
                        new NotebookEditorWindow(SelectedNotebook).ShowDialog();
                        //var window = new MainWindow();
                        //foreach (var item in window.lBox.Items)
                        //{
                        //    if (item is Label l)
                        //    {
                        //        var property = l.GetBindingExpression(Label.ContentProperty);
                        //    }
                        //}
                        ;
                        Notebooks.Update(SelectedNotebook);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                },
                () =>
                {
                    return SelectedNotebook != null;
                });

                DeleteNotebookCommand = new RelayCommand(() =>
                {
                    Notebooks.Delete(SelectedNotebook.Id);
                },
                () =>
                {
                    return SelectedNotebook != null;
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
