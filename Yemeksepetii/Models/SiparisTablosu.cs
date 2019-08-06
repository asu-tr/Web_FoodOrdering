namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiparisTablosu")]
    public partial class SiparisTablosu
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime OrderTime { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string SellerName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string SellerSurname { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrdererID { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(30)]
        public string OrdererName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(20)]
        public string OrdererSurname { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(15)]
        public string Status { get; set; }
    }
}
