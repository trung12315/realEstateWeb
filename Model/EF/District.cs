namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("District")]
    public partial class District
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string districtid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(191)]
        public string name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string provinceid { get; set; }
    }
}
