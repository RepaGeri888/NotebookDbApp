using AJ4GRZ_HFT_2021221.Models;
using AJ4GRZ_HFT_2021221.Repository;
using System.Collections.Generic;
using System.Linq;

namespace AJ4GRZ_HFT_2021221.Logic
{
    public class GpuLogic : IGpuLogic
    {
        readonly IGpuRepository gpuRepo;

        public GpuLogic(IGpuRepository gpuRepo)
        {
            this.gpuRepo = gpuRepo;
        }

        #region CRUD methods
        public void Create(Gpu gpu)
        {
            ValueChecker.IsCorrect(gpu);
            gpuRepo.Create(gpu);
        }

        public Gpu Read(int id)
        {
            var gpu = gpuRepo.Read(id);
            ValueChecker.Exists(gpu);
            return gpu;
        }

        public IEnumerable<Gpu> ReadAll()
        {
            return gpuRepo.ReadAll();
        }

        public void Delete(int id)
        {
            var gpu = gpuRepo.Read(id);
            ValueChecker.Exists(gpu);
            gpuRepo.Delete(id);
        }

        public void Update(Gpu gpu)
        {
            var gpuToUpdate = gpuRepo.Read(gpu.Id);
            ValueChecker.Exists(gpu);
            ValueChecker.IsCorrect(gpu);
            gpuRepo.Update(gpu);
        }
        #endregion

        #region Non-CRUD method
        public IEnumerable<string> BestGpu()
        {
            return (from x in gpuRepo.ReadAll()
                    let factor = x.MemorySize * 13 + x.BusWidth
                    orderby factor descending
                    select x.Name).Take(1);
        }
        #endregion
    }
}
