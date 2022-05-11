using System;

namespace AJ4GRZ_HFT_2021221.Models
{
    public class StatReturnTypes
    {
        public class BrandsAvgPrice
        {
            #region Properties
            public string BrandName { get; set; }

            public int NumberOfNotebooks { get; set; }

            public double AvgPrice { get; set; }
            #endregion

            #region Overrides
            public override bool Equals(object obj)
            {
                return obj is BrandsAvgPrice price &&
                       BrandName == price.BrandName &&
                       NumberOfNotebooks == price.NumberOfNotebooks &&
                       AvgPrice == price.AvgPrice;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(BrandName, NumberOfNotebooks, AvgPrice);
            }

            public override string ToString()
            {
                return $"Brand: {BrandName}, Number of notebooks: {NumberOfNotebooks}, Average price: {AvgPrice}";
            }
            #endregion
        }

        public class MaxMinPrice
        {
            #region Properties
            public string Model { get; set; }
            public int MaxPrice { get; set; }
            public int MinPrice { get; set; }
            #endregion

            #region Overrides
            public override bool Equals(object obj)
            {
                return obj is MaxMinPrice price &&
                       Model == price.Model &&
                       MaxPrice == price.MaxPrice &&
                       MinPrice == price.MinPrice;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Model, MaxPrice, MinPrice);
            }

            public override string ToString()
            {
                return $"Model: {Model}, Max. price: {MaxPrice}, Min. price: {MinPrice}";
            }
            #endregion
        }

        public class HighEndNotebook
        {
            #region Properties
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Cpu { get; set; }
            public string Gpu { get; set; }
            public int Ram { get; set; }
            public int Storage { get; set; }
            public int Price { get; set; }
            #endregion

            #region Overrides
            public override bool Equals(object obj)
            {
                return obj is HighEndNotebook notebook &&
                       Brand == notebook.Brand &&
                       Model == notebook.Model &&
                       Cpu == notebook.Cpu &&
                       Gpu == notebook.Gpu &&
                       Ram == notebook.Ram &&
                       Storage == notebook.Storage &&
                       Price == notebook.Price;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Brand, Model, Cpu, Gpu, Ram, Storage, Price);
            }

            public override string ToString()
            {
                return $"Model: {Model}, Ram: {Ram}, Storage: {Storage}, Price: {Price}";
            }
            #endregion
        }
    }
}
