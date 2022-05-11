using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AJ4GRZ_HFT_2021221.Models
{
    public class Cpu
    {
        #region Table definiton
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Id]
        public int Id { get; set; }

        [Text]
        public string Name { get; set; }

        [Number]
        public int Cores { get; set; }

        [Number]
        public int Threads { get; set; }

        [Number]
        public int ClockSpeed { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Notebook> Notebooks { get; set; }

        public Cpu()
        {
            Notebooks = new HashSet<Notebook>();
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj is Cpu cpu &&
                   Id == cpu.Id &&
                   Name == cpu.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Cores: {Cores}, Threads: {Threads}, ClockSpeed: {ClockSpeed}";
        }
        #endregion
    }
}
