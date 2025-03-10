﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using FashionShop.Models.common;
using FashionShop.Models.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FashionShop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<ReviewProduct> Reviews { get; set; }
        public DbSet<Statistical> Statisticals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Adv> Advs { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<SystemSetting> SystemSetting { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Subcribe> Subcribe { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}