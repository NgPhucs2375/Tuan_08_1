namespace BAI2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHUDE")]
    public partial class CHUDE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MACD { get; set; }

        [StringLength(40)]
        public string TENCHUDE { get; set; }
    }
}
