using AJ4GRZ_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotebookDbApp.WpfClient.ViewModels
{
    public class NotebookEditorWindowViewModel : ObservableRecipient
    {
        public Notebook ActualNotebook { get; set; }

        public void Setup(Notebook actualNotebook)
        {
            ActualNotebook = actualNotebook;
        }

        public NotebookEditorWindowViewModel()
        {

        }
    }
}
