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
    
    public partial class UserRoleMapping
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Role_Id { get; set; }
    
        public virtual User User { get; set; }
        public virtual User_Role User_Role { get; set; }
    }
}
