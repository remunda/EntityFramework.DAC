﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;

//TODO: fix formating

namespace SampleProject.Entities
{

    public interface IEntity
    {
    }


    public class SampleContext : DbContext
    {
        public SampleContext()
            : base("name=SampleContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }

    }


    internal sealed class Configuration : DbMigrationsConfiguration<SampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }

    public partial class Article : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }

        public User CreatedBy { get; set; }

    }

    public partial class User : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? LastLogin { get; set; }

    }


}
