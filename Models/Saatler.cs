//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RandevuSistemiNew.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Saatler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Saatler()
        {
            this.DoktorMusaitligi = new HashSet<DoktorMusaitligi>();
        }
    
        public int ID { get; set; }
        public Nullable<bool> C09_00_09_30 { get; set; }
        public Nullable<bool> C09_30_10_00 { get; set; }
        public Nullable<bool> C10_00_10_30 { get; set; }
        public Nullable<bool> C10_30_11_00 { get; set; }
        public Nullable<bool> C11_00_11_30 { get; set; }
        public Nullable<bool> C11_30_12_00 { get; set; }
        public Nullable<bool> C12_00_12_30 { get; set; }
        public Nullable<bool> C12_30_13_00 { get; set; }
        public Nullable<bool> C13_00_13_30 { get; set; }
        public Nullable<bool> C13_30_14_00 { get; set; }
        public Nullable<bool> C14_00_14_30 { get; set; }
        public Nullable<bool> C14_30_15_00 { get; set; }
        public Nullable<bool> C15_00_15_30 { get; set; }
        public Nullable<bool> C15_30_16_00 { get; set; }
        public Nullable<bool> C16_00_16_30 { get; set; }
        public Nullable<bool> C16_30_17_00 { get; set; }
        public Nullable<bool> C17_00_17_30 { get; set; }
        public Nullable<bool> C17_30_18_00 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoktorMusaitligi> DoktorMusaitligi { get; set; }
    }
}
