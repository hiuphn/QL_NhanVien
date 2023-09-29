namespace Baitapbuoi6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phongban")]
    public partial class Phongban
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phongban()
        {
            Nhanvien = new HashSet<Nhanvien>();
        }

        [Key]
        [StringLength(2)]
        public string MaPB { get; set; }

        [Required]
        [StringLength(30)]
        public string TenPB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Nhanvien> Nhanvien { get; set; }
    }
}
