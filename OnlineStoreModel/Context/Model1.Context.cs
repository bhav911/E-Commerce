﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class OnlineStoreEntities : DbContext
{
    public OnlineStoreEntities()
        : base("name=OnlineStoreEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Cities> Cities { get; set; }

    public virtual DbSet<Coupons> Coupons { get; set; }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<Owner> Owner { get; set; }

    public virtual DbSet<OwnerKYC> OwnerKYC { get; set; }

    public virtual DbSet<ProductImages> ProductImages { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<States> States { get; set; }

    public virtual DbSet<OrderDetails> OrderDetails { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<ADMINS> ADMINS { get; set; }

    public virtual DbSet<Rating> Rating { get; set; }

    public virtual DbSet<RatingDetails> RatingDetails { get; set; }

    public virtual DbSet<ProductRating> ProductRating { get; set; }

    public virtual DbSet<HelpfulReview> HelpfulReview { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<SubCategory> SubCategory { get; set; }

    public virtual DbSet<SubCategoryImage> SubCategoryImage { get; set; }

    public virtual DbSet<Cart> Cart { get; set; }

    public virtual DbSet<CartItems> CartItems { get; set; }

}

}

