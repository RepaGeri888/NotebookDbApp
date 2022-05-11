using AJ4GRZ_HFT_2021221.Models;
using AJ4GRZ_HFT_2021221.Repository;
using System.Collections.Generic;

namespace AJ4GRZ_HFT_2021221.Logic
{
    public class CpuLogic : ICpuLogic
    {
        readonly ICpuRepository cpuRepo;

        public CpuLogic(ICpuRepository cpuRepo)
        {
            this.cpuRepo = cpuRepo;
        }

        #region CRUD methods
        public void Create(Cpu cpu)
        {
            ValueChecker.IsCorrect(cpu);
            cpuRepo.Create(cpu);
        }

        public Cpu Read(int id)
        {
            var cpu = cpuRepo.Read(id);
            ValueChecker.Exists(cpu);
            return cpu;
        }

        public IEnumerable<Cpu> ReadAll()
        {
            return cpuRepo.ReadAll();
        }

        public void Delete(int id)
        {
            var cpu = cpuRepo.Read(id);
            ValueChecker.Exists(cpu);
            cpuRepo.Delete(id);
        }

        public void Update(Cpu cpu)
        {
            var cpuToUpdate = cpuRepo.Read(cpu.Id);
            ValueChecker.Exists(cpuToUpdate);
            ValueChecker.IsCorrect(cpu);
            cpuRepo.Update(cpu);
        }
        #endregion
    }
}
