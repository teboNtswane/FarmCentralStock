//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FarmCentralStock.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Farmer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Farmer()
        {
            this.Product_List = new HashSet<Product_List>();
        }
    
        public int Id { get; set; }
        public int Role_Id { get; set; }

        [Required(ErrorMessage = "* Name field is Required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Surname field is Required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "* Contact field is Required.")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "* Address field is Required.")]
        public string Address { get; set; }
    
        public virtual User_Role User_Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_List> Product_List { get; set; }
    }
}
