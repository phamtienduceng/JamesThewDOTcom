using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Models;

public partial class JamesrecipesContext : DbContext
{
    public JamesrecipesContext()
    {

    }

    public JamesrecipesContext(DbContextOptions<JamesrecipesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public DbSet<CartDetail> CartDetails { get; set; }

    public DbSet<Orders> Orders { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<OrderStatus> orderStatuses { get; set; }

    public virtual DbSet<CategoriesRecipe> CategoriesRecipes { get; set; }

    public virtual DbSet<CategoriesTip> CategoriesTips { get; set; }

    public virtual DbSet<Contest> Contests { get; set; }

    public virtual DbSet<ContestEntry> ContestEntries { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Contact> Contact { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-52MJDM8\\MSSQLSERVER01;Database=jamesrecipes;User=sa;Password=123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId).HasName("PK__Announce__9DE4455425844FA7");

            entity.Property(e => e.AnnouncementId).HasColumnName("AnnouncementID");
            entity.Property(e => e.ContestId).HasColumnName("ContestID");
            entity.Property(e => e.DatePost)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Contest).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.ContestId)
                .HasConstraintName("FK__Announcem__Conte__44FF419A");

            entity.HasOne(d => d.WinnerNavigation).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.Winner)
                .HasConstraintName("FK__Announcem__Winne__440B1D61");
        });
       
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Massage).HasMaxLength(255);
        });

        modelBuilder.Entity<CategoriesRecipe>(entity =>
        {
            entity.HasKey(e => e.CategoryRecipeId).HasName("PK__Categori__4A40FF87B32B2E37");

            entity.Property(e => e.CategoryRecipeId).HasColumnName("CategoryRecipeID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(255);
        });

        modelBuilder.Entity<CategoriesTip>(entity =>
        {
            entity.HasKey(e => e.CategoryTipId).HasName("PK__Categori__EFD0B854A3DDB008");

            entity.Property(e => e.CategoryTipId).HasColumnName("CategoryTipID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(255);
        });

        modelBuilder.Entity<Contest>(entity =>
        {
            entity.HasKey(e => e.ContestId).HasName("PK__Contests__87DE08FA63554D2E");

            entity.Property(e => e.ContestId).HasColumnName("ContestID");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Admin).WithMany(p => p.Contests)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__Contests__AdminI__398D8EEE");
        });

        modelBuilder.Entity<ContestEntry>(entity =>
        {
            entity.HasKey(e => e.EntryId).HasName("PK__ContestE__F57BD2D7EB377675");

            entity.Property(e => e.EntryId).HasColumnName("EntryID");
            entity.Property(e => e.ContestId).HasColumnName("ContestID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Contest).WithMany(p => p.ContestEntries)
                .HasForeignKey(d => d.ContestId)
                .HasConstraintName("FK__ContestEn__Conte__3E52440B");

            entity.HasOne(d => d.Recipe).WithMany(p => p.ContestEntries)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__ContestEn__Recip__403A8C7D");

            entity.HasOne(d => d.User).WithMany(p => p.ContestEntries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ContestEn__UserI__3F466844");
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.HasKey(e => e.Faqid).HasName("PK__FAQ__4B89D1E24EB2FCBE");

            entity.ToTable("FAQ");

            entity.Property(e => e.Faqid).HasColumnName("FAQID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6E4420FF8");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.TipId).HasColumnName("TipID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Feedback__Recipe__571DF1D5");

            entity.HasOne(d => d.Tip).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TipId)
                .HasConstraintName("FK__Feedback__TipID__5629CD9C");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__UserID__534D60F1");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipes__FDD988D0A97768C2");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.CategoryRecipeId).HasColumnName("CategoryRecipeID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.CategoryRecipe).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CategoryRecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipes__Categor__2F10007B");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Recipes__UserID__2E1BDC42");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A868D1970");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasKey(e => e.TipId).HasName("PK__Tips__2DB1A1A82C48F8F6");

            entity.Property(e => e.TipId).HasColumnName("TipID");
            entity.Property(e => e.CategoryTipId).HasColumnName("CategoryTipID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.CategoryTip).WithMany(p => p.Tips)
                .HasForeignKey(d => d.CategoryTipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tips__CategoryTi__34C8D9D1");

            entity.HasOne(d => d.User).WithMany(p => p.Tips)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Tips__UserID__33D4B598");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC8F8C24B2");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
