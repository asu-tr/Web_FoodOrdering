namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuTablosu")]
    public partial class MenuTablosu
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string SellerName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string SellerSurname { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(25)]
        public string MenuName { get; set; }

        [StringLength(60)]
        public string Descriptionn { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "money")]
        public decimal Price { get; set; }
    }
}
