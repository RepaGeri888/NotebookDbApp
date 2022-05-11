using AJ4GRZ_HFT_2021221.Models;
using System.Collections.Generic;

namespace AJ4GRZ_HFT_2021221.Logic
{
    public interface IGpuLogic
    {
        IEnumerable<string> BestGpu();
        void Create(Gpu gpu);
        void Delete(int id);
        Gpu Read(int id);
        IEnumerable<Gpu> ReadAll();
        void Update(Gpu gpu);
    }
}