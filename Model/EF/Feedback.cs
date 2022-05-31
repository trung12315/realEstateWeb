namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Adress { get; set; }

        [StringLength(250)]
        public string Detail { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public decimal? UpdateBy { get; set; }
    }
}
