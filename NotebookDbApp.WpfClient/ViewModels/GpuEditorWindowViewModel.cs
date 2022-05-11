using AJ4GRZ_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotebookDbApp.WpfClient.ViewModels
{
    public class GpuEditorWindowViewModel : ObservableRecipient
    {
        public Gpu ActualGpu { get; set; }

        public void Setup(Gpu actualGpu)
        {
            ActualGpu = actualGpu;
        }

        public GpuEditorWindowViewModel()
        {

        }
    }
}
