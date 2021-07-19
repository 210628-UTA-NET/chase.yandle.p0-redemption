using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DL.Entities
{
    public partial class DemoDbContext : DbContext
    {
        public DemoDbContext()
        {
        }

        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<System> Systems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.City)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Not Provided')");

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Not Provided')");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Not Provided')");

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('XXX-XXX-XXXX')");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('XX')");

                entity.Property(e => e.Street)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Not Provided')");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.GameId)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("GameID");

                entity.Property(e => e.GameName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Msrp)
                    .HasColumnType("money")
                    .HasColumnName("MSRP");

                entity.Property(e => e.OnSystem)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.HasOne(d => d.OnSystemNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.OnSystem)
                    .HasConstraintName("FK__GamesOnSy__OnSys__787EE5A0");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.Location, e.Product })
                    .HasName("PK__Inventor__CF775FF8F98C0027");

                entity.ToTable("Inventory");

                entity.Property(e => e.Location)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .HasMaxLength(61)
                    .IsUnicode(false);

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.Location)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Locat__0B91BA14");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.Product)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Produ__0C85DE4D");
            });

            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.ToTable("LineItem");

                entity.Property(e => e.LineItemId)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("LineItemID");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("OrderID");

                entity.Property(e => e.Product)
                    .HasMaxLength(61)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__LineItem__OrderI__14270015");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.Product)
                    .HasConstraintName("FK__LineItem__Produc__151B244E");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("LocationID");

                entity.Property(e => e.City)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("OrderID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.DestinationId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("DestinationID");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.SourceId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("SourceID");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__Customer__0F624AF8");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.OrderDestinations)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK__Orders__Destinat__10566F31");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.OrderSources)
                    .HasForeignKey(d => d.SourceId)
                    .HasConstraintName("FK__Orders__SourceID__114A936A");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .HasMaxLength(61)
                    .IsUnicode(false)
                    .HasColumnName("productID");

                entity.Property(e => e.GameId)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("GameID");

                entity.Property(e => e.SystemId)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("SystemID");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK__Products__GameID__04E4BC85");

                entity.HasOne(d => d.System)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SystemId)
                    .HasConstraintName("FK__Products__System__05D8E0BE");
            });

            modelBuilder.Entity<System>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Systems__737584F7DB29961E");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Msrp)
                    .HasColumnType("money")
                    .HasColumnName("MSRP");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
