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
    internal class BrandWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        #region Cpus
        public RestCollection<Brand> Brands { get; set; }

        private Brand selectedBrand;

        public Brand SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                if (value != null)
                {
                    selectedBrand = new Brand()
                    {
                        Id = value.Id,
                        Name = value.Name
                    };
                    OnPropertyChanged();
                    (DeleteBrandCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateBrandCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateBrandCommand { get; set; }
        public ICommand DeleteBrandCommand { get; set; }
        public ICommand UpdateBrandCommand { get; set; }
        #endregion

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public BrandWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Brands = new RestCollection<Brand>("http://localhost:32747/", "brand", "hub");
                //var selectedGpus = Gpus.Where(b => b.Id == 2).FirstOrDefault();
                //;
                //;
                //Collection = SelectCollection<Notebook>(Notebooks);
                //;
                #region GpuCommands
                CreateBrandCommand = new RelayCommand(() =>
                {
                    if (SelectedBrand == null)
                    {
                        SelectedBrand = new();
                    }
                    bool? isSaved = new BrandEditorWindow(SelectedBrand).ShowDialog();
                    if ((bool)isSaved)
                    {
                        try
                        {
                            Brands.Add(new Brand()
                            {
                                Name = SelectedBrand.Name
                            });
                        }
                        catch (InvalidOperationException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                    }
                });

                UpdateBrandCommand = new RelayCommand(() =>
                {
                    try
                    {
                        new BrandEditorWindow(SelectedBrand).ShowDialog();
                        //var window = new MainWindow();
                        //foreach (var item in window.lBox.Items)
                        //{
                        //    if (item is Label l)
                        //    {
                        //        var property = l.GetBindingExpression(Label.ContentProperty);
                        //    }
                        //}
                        ;
                        Brands.Update(SelectedBrand);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                },
                () =>
                {
                    return SelectedBrand != null;
                });

                DeleteBrandCommand = new RelayCommand(() =>
                {
                    Brands.Delete(SelectedBrand.Id);
                },
                () =>
                {
                    return SelectedBrand != null;
                });
                #endregion
            }
        }
    }
}
