using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AJ4GRZ_HFT_2021221.Models
{
    public class Gpu
    {
        #region Table definition
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Id]
        public int Id { get; set; }

        [Text]
        public string Name { get; set; }

        [Number]
        public int MemorySize { get; set; }

        [Number]
        public int BusWidth { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Notebook> Notebooks { get; set; }

        public Gpu()
        {
            Notebooks = new HashSet<Notebook>();
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj is Gpu gpu &&
                   Id == gpu.Id &&
                   Name == gpu.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, MemorySize: {MemorySize}, BusWidth: {BusWidth}";
        }
        #endregion
    }
}
