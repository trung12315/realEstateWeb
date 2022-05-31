namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ward")]
    public partial class ward
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string wardid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(191)]
        public string name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string districtid { get; set; }
    }
}
