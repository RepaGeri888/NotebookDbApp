using AJ4GRZ_HFT_2021221.Models;
using System.Linq;

namespace AJ4GRZ_HFT_2021221.Repository
{
    public interface ICpuRepository
    {
        void Create(Cpu cpu);
        void Delete(int id);
        Cpu Read(int id);
        IQueryable<Cpu> ReadAll();
        void Update(Cpu cpu);
    }
}