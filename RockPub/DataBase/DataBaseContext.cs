﻿using Microsoft.EntityFrameworkCore;
using RockPub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishOrder> DishOrders { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Server=DESKTOP-KIV92L3;Database=SportSectionsIHE;Trusted_Connection=True;Encrypt=False;");
            optionsBuilder.UseSqlServer("Server=DESKTOP-I75L3P7;Database=SportSectionsIHE;Trusted_Connection=True;Encrypt=False;");
            // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SportSectionsIHE;Trusted_Connection=True;");
        }
    }
}