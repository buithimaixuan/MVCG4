using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MVCG4.Models
{
    public partial class ProjectPRNContext : DbContext
    {
        public ProjectPRNContext()
        {
        }

        public ProjectPRNContext(DbContextOptions<ProjectPRNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ImportProduct> ImportProducts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-5B3AKMH\\SQLEXPRESS;Database=ProjectPRN;uid=sa;pwd=khoa31102003;encrypt=true;trustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccId)
                    .HasName("PK__account__9A20D55461D5AB34");

                entity.ToTable("account");

                entity.HasIndex(e => e.PhoneNumber, "UQ__account__A1936A6B71B06CF3")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__account__AB6E616480E3A749")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__account__F3DBC5721D0BA3F4")
                    .IsUnique();

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("fullname");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.AccId, e.ProId });

                entity.ToTable("cart");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.ProId).HasColumnName("pro_id");

                entity.Property(e => e.CartPrice)
                    .HasColumnType("decimal(10, 1)")
                    .HasColumnName("cart_price");

                entity.Property(e => e.ProQuantity).HasColumnName("pro_quantity");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("categories");

                entity.Property(e => e.CatDescription)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("cat_description");

                entity.Property(e => e.CatId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("cat_id");

                entity.Property(e => e.CatName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("cat_name");

                entity.Property(e => e.TypeCategories)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("typeCategories");
            });

            modelBuilder.Entity<ImportProduct>(entity =>
            {
                entity.HasKey(e => e.ImpId)
                    .HasName("PK__ImportPr__7B898045D929B4B8");

                entity.ToTable("ImportProduct");

                entity.Property(e => e.ImpId).HasColumnName("imp_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("create_date");

                entity.Property(e => e.ProId).HasColumnName("pro_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.SupId).HasColumnName("sup_id");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OId)
                    .HasName("PK__orders__904BC20E9CAF2DA0");

                entity.ToTable("orders");

                entity.Property(e => e.OId).HasColumnName("o_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("address");

                entity.Property(e => e.CusId).HasColumnName("cus_id");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.ODate)
                    .HasColumnType("date")
                    .HasColumnName("o_date");

                entity.Property(e => e.Payment)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("payment");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10, 1)")
                    .HasColumnName("total_price");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OId, e.ProId });

                entity.ToTable("order_detail");

                entity.Property(e => e.OId).HasColumnName("o_id");

                entity.Property(e => e.ProId).HasColumnName("pro_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProId)
                    .HasName("PK__product__335E4CA698CA8A36");

                entity.ToTable("product");

                entity.Property(e => e.ProId).HasColumnName("pro_id");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .HasColumnName("brand");

                entity.Property(e => e.CatId).HasColumnName("cat_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("create_date");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(10, 1)")
                    .HasColumnName("discount");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("origin");

                entity.Property(e => e.ProDescription)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("pro_description");

                entity.Property(e => e.ProImage)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pro_image");

                entity.Property(e => e.ProName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("pro_name");

                entity.Property(e => e.ProPrice)
                    .HasColumnType("decimal(10, 1)")
                    .HasColumnName("pro_price");

                entity.Property(e => e.ProQuantity).HasColumnName("pro_quantity");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupId)
                    .HasName("PK__supplier__FB8F785F46C69712");

                entity.ToTable("supplier");

                entity.Property(e => e.SupId).HasColumnName("sup_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("address");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("company_name");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.ProId).HasColumnName("pro_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
