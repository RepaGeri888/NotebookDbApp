using AJ4GRZ_HFT_2021221.Models;
using System.Linq;

namespace AJ4GRZ_HFT_2021221.Repository
{
    public interface IGpuRepository
    {
        void Create(Gpu gpu);
        void Delete(int id);
        Gpu Read(int id);
        IQueryable<Gpu> ReadAll();
        void Update(Gpu gpu);
    }
}