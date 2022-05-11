using AJ4GRZ_HFT_2021221.Models;
using AJ4GRZ_HFT_2021221.Repository;
using System.Collections.Generic;

namespace AJ4GRZ_HFT_2021221.Logic
{
    public class BrandLogic : IBrandLogic
    {
        readonly IBrandRepository brandRepo;

        public BrandLogic(IBrandRepository brandRepo)
        {
            this.brandRepo = brandRepo;
        }

        #region CRUD methods
        public void Create(Brand brand)
        {
            ValueChecker.IsCorrect(brand);
            brandRepo.Create(brand);
        }

        public Brand Read(int id)
        {
            var brand = brandRepo.Read(id);
            ValueChecker.Exists(brand);
            return brand;
        }

        public IEnumerable<Brand> ReadAll()
        {
            return brandRepo.ReadAll();
        }

        public void Delete(int id)
        {
            var brand = brandRepo.Read(id);
            ValueChecker.Exists(brand);
            brandRepo.Delete(id);
        }

        public void Update(Brand brand)
        {
            var brandToUpdate = brandRepo.Read(brand.Id);
            ValueChecker.Exists(brandToUpdate);
            ValueChecker.IsCorrect(brand);
            brandRepo.Update(brand);
        }
        #endregion
    }
}
