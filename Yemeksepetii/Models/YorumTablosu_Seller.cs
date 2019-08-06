namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class YorumTablosu_Seller
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommentID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string UserSurname { get; set; }
    }
}
