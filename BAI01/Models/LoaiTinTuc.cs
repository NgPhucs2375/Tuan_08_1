namespace BAI01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiTinTuc")]
    public partial class LoaiTinTuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaLoaiTinTuc { get; set; }

        [StringLength(30)]
        public string TenLoai { get; set; }

        public int? MaTinTuc { get; set; }

        public virtual TinTuc TinTuc { get; set; }
    }
}
