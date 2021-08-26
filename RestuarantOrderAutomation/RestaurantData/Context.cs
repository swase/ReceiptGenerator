using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace RestaurantData
{
    public class Context : DbContext
    {
        //public Context() : base("name=RestaurantDBConnStr") { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=RestaurantData;Trusted_Connection=True;");
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RestaurantData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("RestaurantData.OrderDetail", b =>
            {
                b.Property<int>("OrderItemID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");
     

                b.Property<int?>("OrderID")
                    .HasColumnType("int");

                b.Property<int>("ProductID")
                    .HasColumnType("int");

                b.Property<int>("Quantity")
                    .HasColumnType("int");

                b.HasKey("OrderItemID");

                b.HasIndex("OrderID");

                b.HasIndex("ProductID").IsUnique(false);

                b.ToTable("OrderDetails");


            });

            modelBuilder.Entity("RestaurantData.Product", b =>
            {
                b.Property<int>("ProductID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<double>("Discount")
                    .HasColumnType("float");

                b.Property<int?>("OrderDetailOrderItemID")
                    .HasColumnType("int");

                b.Property<double>("Price")
                    .HasColumnType("float");

                b.Property<string>("ProductName")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("ProductID");

                b.ToTable("Products");
            });





        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }


    }
}
