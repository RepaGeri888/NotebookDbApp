using AJ4GRZ_HFT_2021221.Data;
using AJ4GRZ_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ4GRZ_HFT_2021221.Repository
{
    public class CpuRepository : ICpuRepository
    {
        readonly NotebookDbContext db;
        public CpuRepository(NotebookDbContext db)
        {
            this.db = db;
        }

        public void Create(Cpu cpu)
        {
            db.Cpus.Add(cpu);
            db.SaveChanges();
        }

        public Cpu Read(int id)
        {
            return db.Cpus.FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<Cpu> ReadAll()
        {
            return db.Cpus;
        }

        public void Delete(int id)
        {
            var cpuToDelete = Read(id);
            db.Cpus.Remove(cpuToDelete);
            db.SaveChanges();
        }

        public void Update(Cpu cpu)
        {
            var cpuToUpdate = Read(cpu.Id);
            cpuToUpdate.Name = cpu.Name;
            cpuToUpdate.Cores = cpu.Cores;
            cpuToUpdate.Threads = cpu.Threads;
            cpuToUpdate.ClockSpeed = cpu.ClockSpeed;
            db.SaveChanges();
        }
    }
}
