﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Art_GalleryEntities : DbContext
    {
        public Art_GalleryEntities()
            : base("name=Art_GalleryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Artwork> Artworks { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<New> News { get; set; }
        public virtual DbSet<Payment_gateways> Payment_gateways { get; set; }
        public virtual DbSet<Purcher_order> Purcher_order { get; set; }
        public virtual DbSet<Rel_Artwork_Auctions> Rel_Artwork_Auctions { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Shipping> Shippings { get; set; }
        public virtual DbSet<Status> Status { get; set; }
    }
}
