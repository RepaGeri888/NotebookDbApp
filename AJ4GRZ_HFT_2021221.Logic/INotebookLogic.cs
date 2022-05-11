using AJ4GRZ_HFT_2021221.Models;
using System.Collections.Generic;
using static AJ4GRZ_HFT_2021221.Models.StatReturnTypes;

namespace AJ4GRZ_HFT_2021221.Logic
{
    public interface INotebookLogic
    {
        IEnumerable<KeyValuePair<string, int>> ThreeMostPopularCpus();
        void Create(Notebook notebook);
        void Delete(int id);
        IEnumerable<KeyValuePair<string, string>> NewGenNotebooks();
        Notebook Read(int id);
        IEnumerable<Notebook> ReadAll();
        void Update(Notebook notebook);
        IEnumerable<BrandsAvgPrice> BrandsAvgPrices();
        IEnumerable<HighEndNotebook> HighEndNotebooks();
        IEnumerable<MaxMinPrice> MostAndLeastExpensiveNotebooksByModels();
    }
}