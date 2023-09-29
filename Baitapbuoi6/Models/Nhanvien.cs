namespace Baitapbuoi6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Nhanvien")]
    public partial class Nhanvien
    {
        [Key]
        [StringLength(6)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(20)]
        public string TenNV { get; set; }

        public DateTime Ngaysinh { get; set; }

        [StringLength(2)]
        public string MaPB { get; set; }

        public virtual Phongban Phongban { get; set; }
    }
}
