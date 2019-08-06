namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YorumTablosu")]
    public partial class YorumTablosu
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommentID { get; set; }

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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrdererID { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(30)]
        public string OrdererName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(20)]
        public string OrdererSurname { get; set; }

        [StringLength(20)]
        public string CommentTitle { get; set; }

        [StringLength(150)]
        public string Comment { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SpeedScore { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceScore { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TasteScore { get; set; }
    }
}
