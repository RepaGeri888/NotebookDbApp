using AJ4GRZ_HFT_2021221.Models;
using System.Collections.Generic;

namespace AJ4GRZ_HFT_2021221.Logic
{
    public interface ICpuLogic
    {
        void Create(Cpu cpu);
        void Delete(int id);
        Cpu Read(int id);
        IEnumerable<Cpu> ReadAll();
        void Update(Cpu cpu);
    }
}