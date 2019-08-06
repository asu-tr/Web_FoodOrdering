namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sehirler")]
    public partial class Sehirler
    {
        [Key]
        [StringLength(15)]
        public string City { get; set; }
    }
}
