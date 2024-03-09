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
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-EV8RQ78\\SQLEXPRESS;Database=ProjectPRN;uid=sa;pwd=123456;encrypt=true;trustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccId)
                    .HasName("PK__account__9A20D554344D621F");

                entity.ToTable("account");

                entity.HasIndex(e => e.PhoneNumber, "UQ__account__A1936A6BCC79D6D1")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__account__AB6E61646705F08B")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__account__F3DBC5727CAFDA73")
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

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProId)
                    .HasName("PK__product__335E4CA613EC701F");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
