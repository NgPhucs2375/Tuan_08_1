namespace BAI2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            DONDATHANGs = new HashSet<DONDATHANG>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MAKH { get; set; }

        [StringLength(30)]
        public string HOTEN { get; set; }

        [StringLength(30)]
        public string TAIKHOAN { get; set; }

        [StringLength(30)]
        public string MATKHAU { get; set; }

        [StringLength(30)]
        public string EMAIL { get; set; }

        [StringLength(11)]
        public string DIENTHOAI_KH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYGIDOKHONGTHAY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONDATHANG> DONDATHANGs { get; set; }
    }
}
