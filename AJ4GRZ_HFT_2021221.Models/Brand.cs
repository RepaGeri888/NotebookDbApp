using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AJ4GRZ_HFT_2021221.Models
{
    public class Brand
    {
        #region Table definiton
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Id]
        public int Id { get; set; }

        [Text]
        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Notebook> Notebooks { get; set; }

        public Brand()
        {
            Notebooks = new HashSet<Notebook>();
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj is Brand brand &&
                   Id == brand.Id &&
                   Name == brand.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
        #endregion
    }
}
