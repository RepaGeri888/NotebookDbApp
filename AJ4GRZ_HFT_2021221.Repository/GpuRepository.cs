using AJ4GRZ_HFT_2021221.Data;
using AJ4GRZ_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ4GRZ_HFT_2021221.Repository
{
    public class GpuRepository : IGpuRepository
    {
        readonly NotebookDbContext db;
        public GpuRepository(NotebookDbContext db)
        {
            this.db = db;
        }

        public void Create(Gpu gpu)
        {
            db.Gpus.Add(gpu);
            db.SaveChanges();
        }

        public Gpu Read(int id)
        {
            return db.Gpus.FirstOrDefault(g => g.Id == id);
        }

        public IQueryable<Gpu> ReadAll()
        {
            return db.Gpus;
        }

        public void Delete(int id)
        {
            var gpuToDelete = Read(id);
            db.Gpus.Remove(gpuToDelete);
            db.SaveChanges();
        }

        public void Update(Gpu gpu)
        {
            var gpuToUpdate = Read(gpu.Id);
            gpuToUpdate.Name = gpu.Name;
            gpuToUpdate.MemorySize = gpu.MemorySize;
            gpuToUpdate.BusWidth = gpu.BusWidth;
            db.SaveChanges();
        }
    }
}
