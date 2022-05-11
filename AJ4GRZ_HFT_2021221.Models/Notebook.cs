using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AJ4GRZ_HFT_2021221.Models
{
    public class Notebook
    {
        #region Table definition
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Id]
        public int Id { get; set; }

        [Text]
        public string Model { get; set; }

        [Number]
        public int Ram { get; set; }

        [Number]
        public int Storage { get; set; }

        [Number]
        public int Price { get; set; }

        [NotMapped]
        public virtual Brand Brand { get; set; }

        [ForeignKey(nameof(Brand))]
        [Id]
        [Number]
        public int BrandId { get; set; }

        [NotMapped]
        public virtual  Gpu Gpu { get; set; }

        [ForeignKey(nameof(Gpu))]
        [Id]
        [Number]
        public int GpuId { get; set; }

        [NotMapped]
        public virtual Cpu Cpu { get; set; }

        [ForeignKey(nameof(Cpu))]
        [Id]
        [Number]
        public int CpuId { get; set; }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj is Notebook notebook &&
                   Id == notebook.Id &&
                   Model == notebook.Model &&
                   BrandId == notebook.BrandId &&
                   GpuId == notebook.GpuId &&
                   CpuId == notebook.CpuId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Model, BrandId, GpuId, CpuId);
        }

        public override string ToString()
        {
            return $"ID: {Id}, Brand: {Brand.Name}, Model: {Model}, CPU: {Cpu.Name}, GPU: {Gpu.Name}, Ram: {Ram}, Storage: {Storage}, Price: {Price}";
        }
        #endregion
    }
}
