﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Property(u => u.Id)
                .HasMaxLength(100);

            builder.Entity<IdentityRole>()
                .Property(r => r.Id)
                .HasMaxLength(100);

            // These properties are also used in keys
            builder.Entity<IdentityUserLogin<string>>()
                .Property(l => l.LoginProvider)
                .HasMaxLength(100);

            builder.Entity<IdentityUserLogin<string>>()
                .Property(l => l.ProviderKey)
                .HasMaxLength(100);

            builder.Entity<IdentityUserToken<string>>()
                .Property(t => t.LoginProvider)
                .HasMaxLength(100);

            builder.Entity<IdentityUserToken<string>>()
                .Property(t => t.Name)
                .HasMaxLength(100);
        }
    }
}
