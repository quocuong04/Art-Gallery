//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Art_Gallery.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment_gateways
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment_gateways()
        {
            this.Shippings = new HashSet<Shipping>();
        }
    
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> OrderId { get; set; }
    
        public virtual Purcher_order Purcher_order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}