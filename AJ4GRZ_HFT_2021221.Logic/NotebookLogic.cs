using AJ4GRZ_HFT_2021221.Models;
using AJ4GRZ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static AJ4GRZ_HFT_2021221.Models.StatReturnTypes;

namespace AJ4GRZ_HFT_2021221.Logic
{
    public class NotebookLogic : INotebookLogic
    {
        readonly INotebookRepository notebookRepo;

        public NotebookLogic(INotebookRepository notebookRepo)
        {
            this.notebookRepo = notebookRepo;
        }

        #region CRUD methods
        public void Create(Notebook notebook)
        {
            ValueChecker.IsCorrect(notebook);
            notebookRepo.Create(notebook);
        }

        public Notebook Read(int id)
        {
            var notebook = notebookRepo.Read(id);
            ValueChecker.Exists(notebook);
            return notebook;
        }

        public IEnumerable<Notebook> ReadAll()
        {
            return notebookRepo.ReadAll();
        }

        public void Delete(int id)
        {
            var notebook = notebookRepo.Read(id);
            ValueChecker.Exists(notebook);
            notebookRepo.Delete(id);
        }

        public void Update(Notebook notebook)
        {
            var notebookToUpdate = notebookRepo.Read(notebook.Id);
            ValueChecker.Exists(notebookToUpdate);
            ValueChecker.IsCorrect(notebook);
            notebookRepo.Update(notebook);
        }
        #endregion

        #region Non-CRUD methods

        public IEnumerable<BrandsAvgPrice> BrandsAvgPrices()
        {
            return from x in notebookRepo.ReadAll()
                   group x by x.Brand.Name into g
                   select new BrandsAvgPrice
                   {
                       BrandName = g.Key,
                       NumberOfNotebooks = g.Count(),
                       AvgPrice = Math.Round((double)g.Average(p => p.Price), 2)
                   };
        }

        public IEnumerable<KeyValuePair<string, string>> NewGenNotebooks()
        {
            return from x in notebookRepo.ReadAll()
                   where x.Gpu.Name.Contains("-30") &&
                   (x.Cpu.Name.Contains("-11") || x.Cpu.Name.Contains("-5"))
                   group x by new { x.Brand.Name, x.Model } into g
                   select new KeyValuePair<string, string>(g.Key.Name, g.Key.Model);
        }

        public IEnumerable<KeyValuePair<string, int>> ThreeMostPopularCpus()
        {
            return (from x in notebookRepo.ReadAll()
                   group x by x.Cpu.Name into g
                   orderby g.Count() descending
                   select new KeyValuePair<string, int>(g.Key, g.Count())).Take(3);
        }

        public IEnumerable<HighEndNotebook> HighEndNotebooks()
        {
            return from x in notebookRepo.ReadAll()
                   where x.Ram >= 16 && x.Cpu.Cores >= 8 && x.Gpu.MemorySize >= 8
                   select new HighEndNotebook
                   {
                       Brand = x.Brand.Name,
                       Cpu = x.Cpu.Name,
                       Gpu = x.Gpu.Name,
                       Model = x.Model,
                       Price = x.Price,
                       Ram = x.Ram,
                       Storage = x.Storage
                   };
        }

        public IEnumerable<MaxMinPrice> MostAndLeastExpensiveNotebooksByModels()
        {
            return from x in notebookRepo.ReadAll()
                   group x by x.Model into g
                   orderby g.Max(t => t.Price) descending
                   select new MaxMinPrice
                   {
                       Model = g.Key,
                       MaxPrice = g.Max(t => t.Price),
                       MinPrice = g.Min(t => t.Price)
                   };
        }
        #endregion
    }
}
