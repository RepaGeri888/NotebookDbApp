using AJ4GRZ_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotebookDbApp.WpfClient.ViewModels
{
    public class BrandEditorWindowViewModel
    {
        public Brand ActualBrand { get; set; }

        public void Setup(Brand actualBrand)
        {
            ActualBrand = actualBrand;
        }

        public BrandEditorWindowViewModel()
        {

        }
    }
}
