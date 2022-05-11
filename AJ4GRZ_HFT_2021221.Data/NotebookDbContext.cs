using AJ4GRZ_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AJ4GRZ_HFT_2021221.Data
{
    public class NotebookDbContext : DbContext
    {
        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Cpu> Cpus { get; set; }

        public virtual DbSet<Gpu> Gpus { get; set; }

        public virtual DbSet<Notebook> Notebooks { get; set; }

        public NotebookDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NotebookDB.mdf;Integrated Security=True";
                builder.UseLazyLoadingProxies().UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notebook>(entity =>
            {
                entity.HasOne(notebook => notebook.Brand)
                .WithMany(brand => brand.Notebooks)
                .HasForeignKey(notebook => notebook.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Notebook>(entity =>
            {
                entity.HasOne(notebook => notebook.Cpu)
                .WithMany(cpu => cpu.Notebooks)
                .HasForeignKey(notebook => notebook.CpuId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Notebook>(entity =>
            {
                entity.HasOne(notebook => notebook.Gpu)
                .WithMany(gpu => gpu.Notebooks)
                .HasForeignKey(notebook => notebook.GpuId
                )
                .OnDelete(DeleteBehavior.Cascade);
            });

            Brand Asus = new Brand() { Id = 1, Name = "ASUS" };
            Brand Acer = new Brand() { Id = 2, Name = "ACER" };
            Brand Msi = new Brand() { Id = 3, Name = "MSI" };
            Brand Lenovo = new Brand() { Id = 4, Name = "LENOVO" };
            Brand Hp = new Brand() { Id = 5, Name = "HP" };
            Brand Dell = new Brand() { Id = 6, Name = "DELL" };
            Brand Razer = new Brand() { Id = 7, Name = "RAZER" };
            Brand GigaByte = new Brand() { Id = 8, Name = "GYGABITE" };

            Cpu I7_10750H = new Cpu() { Id = 1, Name = "Intel Core i7-10750H", Cores = 6, Threads = 12, ClockSpeed = 2600 };
            Cpu I7_10870H = new Cpu() { Id = 2, Name = "Intel Core i7-10870H", Cores = 8, Threads = 16, ClockSpeed = 2200 };
            Cpu I7_11370H = new Cpu() { Id = 3, Name = "Intel Core i7-11370H", Cores = 4, Threads = 8, ClockSpeed = 4800 };
            Cpu I9_10980HK = new Cpu() { Id = 4, Name = "Intel Core I9-10980HK", Cores = 8, Threads = 16, ClockSpeed = 2400 };
            Cpu I5_10300H = new Cpu() { Id = 5, Name = "Intel Core i5-10300H", Cores = 4, Threads = 8, ClockSpeed = 2500 };
            Cpu R7_5800H = new Cpu() { Id = 6, Name = "AMD Ryzen 7-5800H", Cores = 8, Threads = 16, ClockSpeed = 3200 };
            Cpu R5_5600H = new Cpu() { Id = 7, Name = "AMD Ryzen 5-5600H", Cores = 6, Threads = 12, ClockSpeed = 3300 };
            Cpu R9_4900HS = new Cpu() { Id = 8, Name = "AMD Ryzen 9-4900HS", Cores = 8, Threads = 16, ClockSpeed = 3000 };
            Cpu R9_5900HS = new Cpu() { Id = 9, Name = "AMD Ryzen 9-5900HS", Cores = 8, Threads = 16, ClockSpeed = 3000 };
            Cpu R7_4800H = new Cpu() { Id = 10, Name = "AMD Ryzen 7-4800H", Cores = 8, Threads = 16, ClockSpeed = 2900 };
            Cpu I7_11800H = new Cpu() { Id = 11, Name = "Intel Core i7-11800H", Cores = 8, Threads = 16, ClockSpeed = 2300 };
            Cpu I7_1165G7 = new Cpu() { Id = 12, Name = "Intel Core i7-1165G7", Cores = 4, Threads = 8, ClockSpeed = 2800 };

            Gpu Rtx_3080 = new Gpu() { Id = 1, Name = "NVidia-RTX-3080", MemorySize = 10, BusWidth = 320 };
            Gpu Rtx_3070 = new Gpu() { Id = 2, Name = "NVidia-RTX-3070", MemorySize = 8, BusWidth = 256 };
            Gpu Rtx_3060 = new Gpu() { Id = 3, Name = "NVidia-RTX-3060", MemorySize = 12, BusWidth = 192 };
            Gpu Rtx_2080 = new Gpu() { Id = 4, Name = "NVidia-RTX-2080", MemorySize = 8, BusWidth = 256 };
            Gpu Rtx_2070 = new Gpu() { Id = 5, Name = "NVidia-RTX-2070", MemorySize = 8, BusWidth = 256 };
            Gpu Rtx_2060 = new Gpu() { Id = 6, Name = "NVidia-RTX-2060", MemorySize = 6, BusWidth = 192 };
            Gpu Rtx_2070_Super = new Gpu() { Id = 7, Name = "NVidia-RTX-2070-Super", MemorySize = 8, BusWidth = 256 };
            Gpu Gtx_1660_TI = new Gpu() { Id = 8, Name = "NVidia-GTX-1660-TI", MemorySize = 8, BusWidth = 256 };

            Notebook Tuf_Gaming_A15_v1 = new Notebook() { Id = 1, BrandId = 1, Model = "TUF Gaming A15", CpuId = 10, GpuId = 8, Ram = 16, Storage = 512, Price = 1182 };
            Notebook Rog_Strix_Scar_v1 = new Notebook() { Id = 2, BrandId = 1, Model = "ROG Strix Scar", CpuId = 4, GpuId = 4, Ram = 32, Storage = 1000, Price = 1924 };
            Notebook Tuf_Dash_F15_v1 = new Notebook() { Id = 3, BrandId = 1, Model = "TUF Dash F15", CpuId = 3, GpuId = 2, Ram = 32, Storage = 2000, Price = 2137 };
            Notebook Legion_5_Pro = new Notebook() { Id = 4, BrandId = 4, Model = "Legion 5 Pro", CpuId = 7, GpuId = 3, Ram = 16, Storage = 512, Price = 1493 };
            Notebook Rog_Strix_G15 = new Notebook() { Id = 5, BrandId = 1, Model = "ROG Strix G15", CpuId = 2, GpuId = 7, Ram = 32, Storage = 2000, Price = 1999 };
            Notebook Rog_Zephyrus_G14_v1 = new Notebook() { Id = 6, BrandId = 1, Model = "ROG Zephyrus G14", CpuId = 8, GpuId = 6, Ram = 16, Storage = 1000, Price = 1657 };
            Notebook Nitro_5_v1 = new Notebook() { Id = 7, BrandId = 2, Model = "Nitro 5", CpuId = 1, GpuId = 8, Ram = 16, Storage = 512, Price = 1179 };
            Notebook Predator_Heilos_300_v1 = new Notebook() { Id = 8, BrandId = 2, Model = "Predator Helios 300", CpuId = 1, GpuId = 6, Ram = 16, Storage = 1000, Price = 1665 };
            Notebook Predator_Triton_300_v1 = new Notebook() { Id = 9, BrandId = 2, Model = "Predator Triton 300", CpuId = 1, GpuId = 7, Ram = 16, Storage = 1000, Price = 2076 };
            Notebook Gf65_Thin = new Notebook() { Id = 10, BrandId = 3, Model = "GF65 Thin", CpuId = 1, GpuId = 6, Ram = 16, Storage = 1000, Price = 1193 };
            Notebook Gs66_Stealth = new Notebook() { Id = 11, BrandId = 3, Model = "GS66 Stealth", CpuId = 4, GpuId = 5, Ram = 32, Storage = 1000, Price = 2492 };
            Notebook Ge66_Raider = new Notebook() { Id = 12, BrandId = 3, Model = "GE66 Raider", CpuId = 2, GpuId = 3, Ram = 32, Storage = 2000, Price = 2581 };
            Notebook Pavilion = new Notebook() { Id = 13, BrandId = 5, Model = "Pavilion", CpuId = 10, GpuId = 8, Ram = 16, Storage = 1000, Price = 1111 };
            Notebook Omen = new Notebook() { Id = 14, BrandId = 5, Model = "Omen", CpuId = 6, GpuId = 2, Ram = 16, Storage = 1000, Price = 1668 };
            Notebook G15 = new Notebook() { Id = 15, BrandId = 6, Model = "G15", CpuId = 2, GpuId = 3, Ram = 16, Storage = 1000, Price = 1499 };
            Notebook Alienware_M15 = new Notebook() { Id = 16, BrandId = 6, Model = "Alienware M15", CpuId = 9, GpuId = 2, Ram = 32, Storage = 1000, Price = 2399 };
            Notebook Blade_15 = new Notebook() { Id = 17, BrandId = 7, Model = "Blade 15", CpuId = 1, GpuId = 2, Ram = 16, Storage = 512, Price = 2299 };
            Notebook Blade_Stealth_13 = new Notebook() { Id = 18, BrandId = 7, Model = "Blade Stealth 13", CpuId = 12, GpuId = 8, Ram = 16, Storage = 512, Price = 1759 };
            Notebook Aero_15 = new Notebook() { Id = 19, BrandId = 8, Model = "Aero 15", CpuId = 11, GpuId = 3, Ram = 16, Storage = 1000, Price = 1795 };
            Notebook Aorus_17 = new Notebook() { Id = 20, BrandId = 8, Model = "Aorus 17", CpuId = 11, GpuId = 1, Ram = 32, Storage = 1512, Price = 4133 };
            Notebook Aorus_15P = new Notebook() { Id = 21, BrandId = 8, Model = "Aorus 15P", CpuId = 11, GpuId = 2, Ram = 16, Storage = 1000, Price = 2036 };
            Notebook Aorus_15G = new Notebook() { Id = 22, BrandId = 8, Model = "Aorus 15G", CpuId = 2, GpuId = 2, Ram = 32, Storage = 512, Price = 1649 };
            Notebook Legion_Y740S = new Notebook() { Id = 23, BrandId = 4, Model = "Legion Y740S", CpuId = 5, GpuId = 6, Ram = 16, Storage = 512, Price = 1114 };
            Notebook Tuf_Gaming_A15_v2 = new Notebook() { Id = 24, BrandId = 1, Model = "TUF Gaming A15", CpuId = 6, GpuId = 2, Ram = 32, Storage = 1000, Price = 2124 };
            Notebook Tuf_Gaming_A15_v3 = new Notebook() { Id = 25, BrandId = 1, Model = "TUF Gaming A15", CpuId = 6, GpuId = 3, Ram = 32, Storage = 1000, Price = 1805 };
            Notebook Tuf_Gaming_A15_v4 = new Notebook() { Id = 26, BrandId = 1, Model = "TUF Gaming A15", CpuId = 2, GpuId = 6, Ram = 16, Storage = 512, Price = 1311 };
            Notebook Tuf_Gaming_A15_v5 = new Notebook() { Id = 27, BrandId = 1, Model = "TUF Gaming A15", CpuId = 2, GpuId = 8, Ram = 16, Storage = 512, Price = 1217 };
            Notebook Tuf_Gaming_A15_v6 = new Notebook() { Id = 28, BrandId = 1, Model = "TUF Gaming A15", CpuId = 5, GpuId = 8, Ram = 8, Storage = 512, Price = 1063 };
            Notebook Rog_Strix_Scar_v2 = new Notebook() { Id = 29, BrandId = 1, Model = "ROG Strix Scar", CpuId = 2, GpuId = 4, Ram = 32, Storage = 1000, Price = 1899 };
            Notebook Tuf_Dash_F15_v2 = new Notebook() { Id = 30, BrandId = 1, Model = "TUF Dash F15", CpuId = 3, GpuId = 3, Ram = 32, Storage = 1000, Price = 1694 };
            Notebook Legion_5 = new Notebook() { Id = 31, BrandId = 4, Model = "Legion 5", CpuId = 1, GpuId = 8, Ram = 16, Storage = 1000, Price = 1338 };
            Notebook Rog_Zephyrus_G14_v2 = new Notebook() { Id = 32, BrandId = 1, Model = "ROG Zephyrus G14", CpuId = 8, GpuId = 8, Ram = 8, Storage = 512, Price = 1424 };
            Notebook Rog_Zephyrus_G14_v3 = new Notebook() { Id = 33, BrandId = 1, Model = "ROG Zephyrus G14", CpuId = 9, GpuId = 3, Ram = 32, Storage = 1000, Price = 2194 };
            Notebook Nitro_5_v2 = new Notebook() { Id = 34, BrandId = 2, Model = "Nitro 5", CpuId = 5, GpuId = 8, Ram = 8, Storage = 512, Price = 1055 };
            Notebook Nitro_5_v3 = new Notebook() { Id = 35, BrandId = 2, Model = "Nitro 5", CpuId = 1, GpuId = 6, Ram = 16, Storage = 1000, Price = 1267 };
            Notebook Nitro_5_v4 = new Notebook() { Id = 36, BrandId = 2, Model = "Nitro 5", CpuId = 6, GpuId = 3, Ram = 16, Storage = 1000, Price = 1577 };
            Notebook Predator_Heilos_300_v2 = new Notebook() { Id = 37, BrandId = 2, Model = "Predator Helios 300", CpuId = 1, GpuId = 7, Ram = 16, Storage = 1000, Price = 2133 };
            Notebook Predator_Heilos_300_v3 = new Notebook() { Id = 38, BrandId = 2, Model = "Predator Helios 300", CpuId = 5, GpuId = 6, Ram = 16, Storage = 512, Price = 1521 };
            Notebook Predator_Heilos_300_v4 = new Notebook() { Id = 39, BrandId = 2, Model = "Predator Helios 300", CpuId = 2, GpuId = 1, Ram = 32, Storage = 1000, Price = 2416 };
            Notebook Predator_Heilos_300_v5 = new Notebook() { Id = 40, BrandId = 2, Model = "Predator Helios 300", CpuId = 1, GpuId = 2, Ram = 32, Storage = 1000, Price = 1869 };
            Notebook Predator_Triton_300_v2 = new Notebook() { Id = 41, BrandId = 2, Model = "Predator Triton 300", CpuId = 3, GpuId = 3, Ram = 16, Storage = 512, Price = 1527 };
            Notebook Predator_Triton_300_v3 = new Notebook() { Id = 42, BrandId = 2, Model = "Predator Triton 300", CpuId = 1, GpuId = 6, Ram = 16, Storage = 1000, Price = 1832 };
            Notebook Predator_Triton_300_v4 = new Notebook() { Id = 43, BrandId = 2, Model = "Predator Triton 300", CpuId = 1, GpuId = 7, Ram = 16, Storage = 2000, Price = 2249 };


            modelBuilder.Entity<Brand>().HasData(Asus, Acer, Dell, Hp, Lenovo, Msi, Razer, GigaByte);
            modelBuilder.Entity<Gpu>().HasData(Rtx_2060, Rtx_2070, Rtx_2070_Super, Rtx_2080, Rtx_3060, Rtx_3070, Rtx_3080, Gtx_1660_TI);
            modelBuilder.Entity<Cpu>().HasData(I7_10750H, I7_10870H, I7_11370H, I7_1165G7, I7_11800H, I9_10980HK, R5_5600H, R7_4800H, R7_5800H, R9_4900HS, R9_5900HS, I5_10300H);
            modelBuilder.Entity<Notebook>().HasData(Aero_15, Alienware_M15, Aorus_15G, Aorus_15P, Aorus_17, Blade_15, Blade_Stealth_13, G15, Ge66_Raider, Gf65_Thin, Gs66_Stealth, Legion_5_Pro, Legion_Y740S, Nitro_5_v1, Nitro_5_v2, Nitro_5_v3, Nitro_5_v4, Omen, Pavilion, Predator_Heilos_300_v1, Predator_Heilos_300_v2, Predator_Heilos_300_v3, Predator_Heilos_300_v4, Predator_Heilos_300_v5, Predator_Triton_300_v1, Predator_Triton_300_v2, Predator_Triton_300_v3, Predator_Triton_300_v4, Rog_Strix_G15, Rog_Strix_Scar_v1, Rog_Strix_Scar_v2, Rog_Zephyrus_G14_v1, Rog_Zephyrus_G14_v2, Rog_Zephyrus_G14_v3, Tuf_Dash_F15_v1, Tuf_Dash_F15_v2, Tuf_Gaming_A15_v1, Tuf_Gaming_A15_v2, Tuf_Gaming_A15_v3, Tuf_Gaming_A15_v4, Tuf_Gaming_A15_v5, Tuf_Gaming_A15_v6);
        }
    }
}
