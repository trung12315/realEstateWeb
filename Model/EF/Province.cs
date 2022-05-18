namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Province")]
    public partial class Province
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string provinceid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(191)]
        public string name { get; set; }
    }
}
