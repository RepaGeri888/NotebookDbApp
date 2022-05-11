using AJ4GRZ_HFT_2021221.Models;
using System.Linq;

namespace AJ4GRZ_HFT_2021221.Repository
{
    public interface IBrandRepository
    {
        void Create(Brand brand);
        void Delete(int id);
        Brand Read(int id);
        IQueryable<Brand> ReadAll();
        void Update(Brand brand);
    }
}