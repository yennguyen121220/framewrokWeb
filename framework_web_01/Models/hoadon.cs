//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace framework_web_01.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class hoadon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hoadon()
        {
            this.cthds = new HashSet<cthd>();
        }
    
        public int mahd { get; set; }
        public string tendangnhap { get; set; }
        public Nullable<double> tien { get; set; }
        public string sdt { get; set; }
        public string diachi { get; set; }
        public string hoten { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cthd> cthds { get; set; }
        public virtual taikhoan taikhoan { get; set; }
    }
}
