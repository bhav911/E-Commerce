
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
    
public partial class OwnerKYC
{

    public int kycID { get; set; }

    public Nullable<int> OwnerID { get; set; }

    public string panCard { get; set; }

    public string aadharCard { get; set; }

    public string passpostImage { get; set; }

    public string shopImage { get; set; }



    public virtual Owner Owner { get; set; }

}

}
