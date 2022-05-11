using AJ4GRZ_HFT_2021221.Logic;
using AJ4GRZ_HFT_2021221.Models;
using AJ4GRZ_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static AJ4GRZ_HFT_2021221.Logic.NotebookLogic;
using static AJ4GRZ_HFT_2021221.Models.StatReturnTypes;

namespace AJ4GRZ_HFT_2021221.Test
{
    [TestFixture]
    public class Tester
    {
        GpuLogic gl;
        CpuLogic cl;
        NotebookLogic nl;
        BrandLogic bl;

        #region CRUD method tests
        [Test]
        [TestCase(24, TestName = "Create w/ positive => Successful")]
        [TestCase(0, typeof(InvalidOperationException), TestName = "Create w/ zero => Throws Exception")]
        [TestCase(-24, typeof(InvalidOperationException), TestName = "Create w/ negative => Throws exception")]
        public void CreateGpu_TestIntValues(int memorySize, Type exception = null)
        {
            //ARRANGE
            Mock<IGpuRepository> mockedGpuRepo = new();
            List<Gpu> gpus = new();
            gl = new(mockedGpuRepo.Object);
            Gpu gpu = new() { Id = 1, Name = "NVidia-RTX-3090", MemorySize = memorySize, BusWidth = 384 };

            //ACT & ASSERT
            if (exception != null)
            {
                Assert.That(delegate { gl.Create(gpu); }, Throws.InvalidOperationException);
            }
            else Assert.That(delegate { gl.Create(gpu); }, Throws.Nothing);
        }

        [Test]
        public void ReadAllCpusTest()
        {
            //ARRANGE
            Mock<ICpuRepository> mockedCpuRepo = new();
            List<Cpu> cpus = new()
            {
                new Cpu() { Id = 7},
                new Cpu() { Id = 8},
                new Cpu() {Id = 9 },
                new Cpu() {Id = 10 },
                new Cpu() {Id = 11 }
            };

            mockedCpuRepo.Setup(z => z.ReadAll()).Returns(cpus.AsQueryable());

            List<Cpu> expectedCpus = new()
            {
                new Cpu() {Id = 7 },
                new Cpu() {Id = 8 },
                new Cpu() {Id = 9 },
                new Cpu() {Id = 10 },
                new Cpu() {Id = 11 }
            };

            cl = new(mockedCpuRepo.Object);

            //ACT
            var result = cl.ReadAll();

            //ASSERT
            Assert.That(result, Is.EquivalentTo(expectedCpus));
        }

        [Test]
        [TestCase(4, TestName = "Read on existing Id => Successtful")]
        [TestCase(-5, typeof(InvalidOperationException), TestName = "Read on negative => Thorws Exception")]
        [TestCase(132, typeof(InvalidOperationException), TestName = "Read on non existing Id => Thorws Exception")]
        public void ReadOneNotebookTest(int id, Type exception = null)
        {
            //ARRANGE
            Mock<INotebookRepository> mockedNotebookRepo = new();
            List<Notebook> notebooks = new()
            {
                new Notebook() { Id = 1 },
                new Notebook() { Id = 2 },
                new Notebook() { Id = 3 },
                new Notebook() { Id = 4 },
                new Notebook() { Id = 5 },
                new Notebook() { Id = 6 }
            };

            mockedNotebookRepo.Setup(z => z.ReadAll()).Returns(notebooks.AsQueryable());
            mockedNotebookRepo.Setup(z => z.Read(It.IsAny<int>())).Returns(notebooks.Where(z => z.Id == id).FirstOrDefault());
            nl = new(mockedNotebookRepo.Object);
            Notebook expectedNotebook = new() { Id = id };

            //ACT & ASSERT
            if (exception != null)
            {
                Assert.That(delegate { nl.Read(id); }, Throws.InvalidOperationException);
            }
            else
            {
                Assert.That(delegate { nl.Read(id); }, Throws.Nothing);
                var result = nl.Read(id);
                Assert.That(result, Is.EqualTo(expectedNotebook));
            }
        }

        [Test]
        [TestCase("Apple", TestName = "Update w/ str => Successful")]
        [TestCase("Apple", typeof(InvalidOperationException), 5, TestName = "Update on non existing id => Throws Exception")]
        [TestCase("", typeof(InvalidOperationException), TestName = "Update w/ empty str => Throws Exception")]
        [TestCase("Alkjdpoqwpoeipdofsédlfkowepfjrgibhgnefppwtutbpbwetvpcrpoipo", typeof(InvalidOperationException), 5, TestName = "Update w/ too long str => Throws Exception")]
        public void UpdateBrand_TestStringValues(string name, Type exception = null, int id = 1)
        {
            //ARRANGE
            Mock<IBrandRepository> mockedBrandRepo = new();
            List<Brand> brands = new()
            {
                new() { Id = 2, Name = "Blahblah" },
                new() { Id = 1, Name = "Something" },
                new() { Id = 3, Name = "Awee" }
            };

            mockedBrandRepo.Setup(z => z.Read(It.IsAny<int>())).Returns(brands.Where(z => z.Id == id).FirstOrDefault());
            bl = new(mockedBrandRepo.Object);
            Brand brandToUpdate = new() { Id = 1, Name = name };

            //ACT & ASSERT
            if (exception != null)
            {
                Assert.That(delegate { bl.Update(brandToUpdate); }, Throws.InvalidOperationException);

            }
            else Assert.That(delegate { bl.Update(brandToUpdate); }, Throws.Nothing);
        }

        #endregion

        #region Non-CRUD method tests
        [Test] 
        public void BestGpuTest()
        {
            //ARRANGE
            Mock<IGpuRepository> mockedGpuRepo = new();

            List<Gpu> gpus = new()
            {
                new Gpu() { Name = "First", MemorySize = 20, BusWidth = 512},
                new Gpu() { Name = "Second", MemorySize = 10, BusWidth = 256 },
                new Gpu() { Name = "Thirds", MemorySize = 5, BusWidth = 192 }
            };
            Gpu expectedGpu = gpus[0];

            mockedGpuRepo.Setup(z => z.ReadAll()).Returns(gpus.AsQueryable());
            gl = new(mockedGpuRepo.Object);

            //ACT
            var result = gl.BestGpu().FirstOrDefault();

            //ASSERT
            Assert.That(result, Is.EquivalentTo(expectedGpu.Name));
        }

        [Test]
        public void BrandsAvgPricesTest()
        {
            //ARRANGE
            Mock<INotebookRepository> mockedNotebookRepo = new();

            Brand msi = new() { Name = "MSI" };
            Brand gygabite = new() { Name = "GYGABITE" };
            Brand lenovo = new() { Name = "LENOVO" };

            List<Notebook> notebooks = new()
            {
                new Notebook() { Brand = msi, Price = 2299 },
                new Notebook() { Brand = msi, Price = 1759 },
                new Notebook() { Brand = gygabite, Price = 1795 },
                new Notebook() { Brand = gygabite, Price = 4133 },
                new Notebook() { Brand = gygabite, Price = 2036 },
                new Notebook() { Brand = gygabite, Price = 1649 },
                new Notebook() { Brand = lenovo, Price = 1114 }
            };
            List<BrandsAvgPrice> expectedBrandsAvgPrice = new()
            {
                new BrandsAvgPrice() { BrandName = "MSI", NumberOfNotebooks = 2, AvgPrice = 2029 },
                new BrandsAvgPrice() { BrandName = "GYGABITE", NumberOfNotebooks = 4, AvgPrice = 2403.25 },
                new BrandsAvgPrice() { BrandName = "LENOVO", NumberOfNotebooks = 1, AvgPrice = 1114 }
            };
            
            mockedNotebookRepo.Setup(z => z.ReadAll()).Returns(notebooks.AsQueryable());
            nl = new(mockedNotebookRepo.Object);

            //ACT
            var result = nl.BrandsAvgPrices();

            //ASSERT
            Assert.That(result, Is.EquivalentTo(expectedBrandsAvgPrice));
        }

        [Test]
        public void NewGenNotebooksTest()
        {
            //ARRANGE
            Mock<INotebookRepository> mockedNotebookRepo = new();

            Brand Acer = new() { Name = "ACER" };
            Brand Asus = new() { Name = "ASUS" };

            Cpu I7_10870H = new() { Name = "Intel Core i7-10870H" };
            Cpu I7_11370H = new() { Name = "Intel Core i7-11370H" };
            Cpu R7_5800H = new() { Name = "AMD Ryzen 7-5800H" };
            Cpu R7_4800H = new() { Name = "AMD Ryzen 7-4800H" };

            Gpu Rtx_3060 = new() { Name = "NVidia-RTX-3060" };
            Gpu Rtx_2070 = new() { Name = "NVidia-RTX-2070" };

            List<Notebook> notebooks = new()
            {
                new Notebook() { Brand = Asus, Model = "New Gen 1", Cpu = I7_11370H, Gpu = Rtx_3060},
                new Notebook() { Brand = Asus, Model = "Old Gen 1", Cpu = I7_10870H, Gpu = Rtx_2070 },
                new Notebook() { Brand = Acer, Model = "Old Gen 2", Cpu = R7_5800H, Gpu = Rtx_2070 },
                new Notebook() { Brand = Acer, Model = "New Gen 2", Cpu = R7_5800H, Gpu = Rtx_3060 },
                new Notebook() { Brand = Acer, Model = "Old Gen 3", Cpu = R7_4800H, Gpu = Rtx_3060 }
            };

            List<KeyValuePair<string, string>> expectedNotebooks = new()
            {
                new KeyValuePair<string, string>(Asus.Name, "New Gen 1"),
                new KeyValuePair<string, string>(Acer.Name, "New Gen 2")
            };

            mockedNotebookRepo.Setup(z => z.ReadAll()).Returns(notebooks.AsQueryable());
            nl = new(mockedNotebookRepo.Object);

            //ACT
            var result = nl.NewGenNotebooks();

            //ASSERT
            Assert.That(result, Is.EquivalentTo(expectedNotebooks));
        }

        [Test]
        public void ThreeMostPopularCpusTest()
        {
            //ARRANGE
            Mock<INotebookRepository> mockedNotebookRepo = new();

            Cpu First = new() { Name = "First" };
            Cpu Second = new() { Name = "Second" };
            Cpu Third = new() {Name = "Third" };
            Cpu NotPopular1 = new() { Name = "NotPopular1" };
            Cpu NotPopular2 = new() { Name = "NotPopular2" };
            Cpu NotPopular3 = new() { Name = "NotPopular3" };

            List<Notebook> notebooks = new()
            {
                new Notebook() { Cpu = First },
                new Notebook() { Cpu = First },
                new Notebook() { Cpu = First },
                new Notebook() { Cpu = First },
                new Notebook() { Cpu = First },
                new Notebook() { Cpu = First },
                new Notebook() { Cpu = Second },
                new Notebook() { Cpu = Second },
                new Notebook() { Cpu = Second },
                new Notebook() { Cpu = Second },
                new Notebook() { Cpu = Second },
                new Notebook() { Cpu = Third },
                new Notebook() { Cpu = Third },
                new Notebook() { Cpu = Third },
                new Notebook() { Cpu = Third },
                new Notebook() { Cpu = NotPopular1 },
                new Notebook() { Cpu = NotPopular1 },
                new Notebook() { Cpu = NotPopular2 },
                new Notebook() { Cpu = NotPopular3 },
                new Notebook() { Cpu = NotPopular3 }
            };

            List<KeyValuePair<string, int>> expectedCpus = new()
            {
                new KeyValuePair<string, int>(First.Name, 6),
                new KeyValuePair<string, int>(Second.Name, 5),
                new KeyValuePair<string, int>(Third.Name, 4)
            };

            mockedNotebookRepo.Setup(z => z.ReadAll()).Returns(notebooks.AsQueryable());
            nl = new(mockedNotebookRepo.Object);

            //ACT
            var result = nl.ThreeMostPopularCpus();

            //ASSERT
            Assert.That(result, Is.EquivalentTo(expectedCpus));
        }

        [Test]
        public void HighEndNotebooksTest()
        {
            //ARRANGE
            Mock<INotebookRepository> mockedNotebookRepo = new();

            Brand Acer = new() { Name = "ACER" };
            Brand Asus = new() { Name = "ASUS" };

            Cpu I9_10980HK = new() { Name = "Intel Core I9-10980HK", Cores = 8 };
            Cpu I7_11370H = new() { Name = "Intel Core i7-11370H", Cores = 4 };
            Cpu R7_5800H = new() { Name = "AMD Ryzen 7-5800H", Cores = 8 };

            Gpu Rtx_3080 = new() { Name = "NVidia-RTX-3080", MemorySize = 10 };
            Gpu Rtx_3060 = new() { Name = "NVidia-RTX-3060", MemorySize = 8 };
            Gpu Rtx_2060 = new() { Name = "NVidia-RTX-2060", MemorySize = 6 };

            List<Notebook> notebooks = new()
            {
                new Notebook() { Brand = Asus, Model = "HighEnd1", Cpu = R7_5800H, Gpu = Rtx_3060, Ram = 16, Price = 2100, Storage = 1000 },
                new Notebook() { Brand = Asus, Model = "HighEnd2", Cpu = I9_10980HK, Gpu = Rtx_3080, Ram = 32, Price = 2800, Storage = 1000 },
                new Notebook() { Brand = Acer, Model = "MidEnd1", Cpu = R7_5800H, Gpu = Rtx_3060, Ram = 8, Price = 1100, Storage = 512 },
                new Notebook() { Brand = Acer, Model = "MidEnd2", Cpu = I9_10980HK, Gpu = Rtx_2060, Ram = 16, Price = 1200, Storage = 512 },
                new Notebook() { Brand = Acer, Model = "MidEnd3", Cpu = I7_11370H, Gpu = Rtx_3060, Ram = 16, Price = 1300, Storage = 512 }
            };

            List<HighEndNotebook> expectedNotebooks = new()
            {
                new HighEndNotebook() { Brand = Asus.Name, Model = "HighEnd1", Cpu = R7_5800H.Name, Gpu = Rtx_3060.Name, Ram = 16, Price = 2100, Storage = 1000 },
                new HighEndNotebook() { Brand = Asus.Name, Model = "HighEnd2", Cpu = I9_10980HK.Name, Gpu = Rtx_3080.Name, Ram = 32, Price = 2800, Storage = 1000 }
            };

            mockedNotebookRepo.Setup(z => z.ReadAll()).Returns(notebooks.AsQueryable());
            nl = new(mockedNotebookRepo.Object);

            //ACT
            var result = nl.HighEndNotebooks();

            //ASSERT
            Assert.That(result, Is.EquivalentTo(expectedNotebooks));
        }

        [Test]
        public void MostAndLeastExpensiveNotebooksByModelsTest()
        {
            //ARRANGE
            Mock<INotebookRepository> mockedNotebookRepo = new();

            List<Notebook> notebooks = new()
            {
                new Notebook() { Model = "Model1", Price = 1000 },
                new Notebook() { Model = "Model1", Price = 500 },
                new Notebook() { Model = "Model1", Price = 1500 },
                new Notebook() { Model = "Model1", Price = 2000 },
                new Notebook() { Model = "Model2", Price = 3000 },
                new Notebook() { Model = "Model2", Price = 2000 },
                new Notebook() { Model = "Model2", Price = 1500 },
                new Notebook() { Model = "Model3", Price = 700 },
                new Notebook() { Model = "Model3", Price = 1200 },
                new Notebook() { Model = "Model3", Price = 2400 },
                new Notebook() { Model = "Model3", Price = 1300 },
                new Notebook() { Model = "Model3", Price = 1700 },
            };

            List<MaxMinPrice> expectedNotebooks = new()
            {
                new MaxMinPrice() { Model = "Model1", MaxPrice = 2000, MinPrice = 500 },
                new MaxMinPrice() { Model = "Model2", MaxPrice = 3000, MinPrice = 1500 },
                new MaxMinPrice() { Model = "Model3", MaxPrice = 2400, MinPrice = 700 }
            };

            mockedNotebookRepo.Setup(z => z.ReadAll()).Returns(notebooks.AsQueryable());
            nl = new(mockedNotebookRepo.Object);

            //ACT
            var result = nl.MostAndLeastExpensiveNotebooksByModels();

            //ASSERT
            Assert.That(result, Is.EquivalentTo(expectedNotebooks));
        }
        #endregion
    }
}
