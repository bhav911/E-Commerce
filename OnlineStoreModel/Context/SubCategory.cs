
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace OnlineStoreModel.Context
{

using System;
    using System.Collections.Generic;
    
public partial class SubCategory
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public SubCategory()
    {

        this.Products = new HashSet<Products>();

    }


    public int subCategoryID { get; set; }

    public int categoryID { get; set; }

    public string name { get; set; }

    public string description { get; set; }

    public int productCount { get; set; }



    public virtual Category Category { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Products> Products { get; set; }

}

}