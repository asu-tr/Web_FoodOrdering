namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SunulanYemekler")]
    public partial class SunulanYemekler
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServeID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(25)]
        public string ProductName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerID { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(30)]
        public string SellerName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(20)]
        public string SellerSurname { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "money")]
        public decimal Price { get; set; }
    }
}
