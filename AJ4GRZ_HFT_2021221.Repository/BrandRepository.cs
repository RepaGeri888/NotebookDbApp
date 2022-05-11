using AJ4GRZ_HFT_2021221.Data;
using AJ4GRZ_HFT_2021221.Models;
using System;
using System.Linq;

namespace AJ4GRZ_HFT_2021221.Repository
{
    public class BrandRepository : IBrandRepository
    {
        readonly NotebookDbContext db;
        public BrandRepository(NotebookDbContext db)
        {
            this.db = db;
        }

        public void Create(Brand brand)
        {
            db.Brands.Add(brand);
            db.SaveChanges();
        }

        public Brand Read(int id)
        {
            return db.Brands.FirstOrDefault(b => b.Id == id);
        }

        public IQueryable<Brand> ReadAll()
        {
            return db.Brands;
        }

        public void Delete(int id)
        {
            var brandToDelete = Read(id);
            db.Brands.Remove(brandToDelete);
            db.SaveChanges();
        }

        public void Update(Brand brand)
        {
            var brandToUpdate = Read(brand.Id);
            brandToUpdate.Name = brand.Name;
            db.SaveChanges();
        }
    }
}
