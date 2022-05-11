using AJ4GRZ_HFT_2021221.Data;
using AJ4GRZ_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ4GRZ_HFT_2021221.Repository
{
    public class NotebookRepository : INotebookRepository
    {
        readonly NotebookDbContext db;
        public NotebookRepository(NotebookDbContext db)
        {
            this.db = db;
        }

        public void Create(Notebook notebook)
        {
            db.Notebooks.Add(notebook);
            db.SaveChanges();
        }

        public Notebook Read(int id)
        {
            return db.Notebooks.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Notebook> ReadAll()
        {
            return db.Notebooks;
        }

        public void Delete(int id)
        {
            var notebookToDelete = Read(id);
            db.Notebooks.Remove(notebookToDelete);
            db.SaveChanges();
        }

        public void Update(Notebook notebook)
        {
            var notebookToUpdate = Read(notebook.Id);
            notebookToUpdate.Model = notebook.Model;
            notebookToUpdate.Price = notebook.Price;
            notebookToUpdate.Ram = notebook.Ram;
            notebookToUpdate.Storage = notebook.Storage;
            notebookToUpdate.BrandId = notebook.BrandId;
            notebookToUpdate.CpuId = notebook.CpuId;
            notebookToUpdate.GpuId = notebook.GpuId;
            db.SaveChanges();
        }
    }
}
