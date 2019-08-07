namespace Yemeksepetii.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommentAnswers
    {
        [Key]
        public int AnswerID { get; set; }

        public int CommentID { get; set; }

        [StringLength(150)]
        public string Answer { get; set; }
    }
}
