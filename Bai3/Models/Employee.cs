namespace Bai3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(40)]
        public string NameEmpl { get; set; }

        [StringLength(5)]
        public string Gender { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        public int? Deptid { get; set; }

        public virtual Deparment Deparment { get; set; }
    }
}
