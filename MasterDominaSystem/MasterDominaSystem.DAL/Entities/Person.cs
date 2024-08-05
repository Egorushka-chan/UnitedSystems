﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MasterDominaSystem.DAL.Interfaces;

namespace MasterDominaSystem.DAL.Entities
{
    public partial class Person : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Type {get; set; } // Не только люди могут одеваться
        public virtual ICollection<Physique> Physiques { get; set; } = [];
        
    }
}