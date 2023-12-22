using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using model.Models;

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

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

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

    public virtual DbSet<ViewHomepage> ViewHomepages { get; set; }
    public virtual DbSet<ViewUserRole> ViewUserRoles { get; set; }
    
    public virtual DbSet<ViewRecipeManagement> ViewRecipeManagements { get; set; }

    public virtual DbSet<ViewTipManagement> ViewTipManagements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.,1433;Database=jamesrecipes;User=sa;Password=Abc@1234;TrustServerCertificate=True");

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

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C227B9BE35FE");

            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Author).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Cart>(entity => 
        {
            entity.Property(e => e.Id).HasMaxLength(100);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(100);
            entity.Property(e => e.Quantity).HasMaxLength(100);
            entity.Property(e => e.CartId).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF67011B6B");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__4BAC3F29");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30CCFCB8105");
            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__OrderDeta__BookI__5070F446");
            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__4F7CD00D");
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
            entity.HasKey(e => e.RecipeId).HasName("PK__tmp_ms_x__FDD988D0C9C6AA59");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.CategoryRecipeId).HasColumnName("CategoryRecipeID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Rating)
                .HasDefaultValueSql("((2.5))")
                .HasColumnName("rating");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Timeneeds).HasColumnName("timeneeds");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VideoUrl).HasColumnName("videoUrl");

            entity.HasOne(d => d.CategoryRecipe).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CategoryRecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipes__Categor__07C12930");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Recipes__UserID__06CD04F7");
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
        
        modelBuilder.Entity<ViewHomepage>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewHomepage");

            entity.Property(e => e.AnnouncementImage).HasMaxLength(255);
            entity.Property(e => e.AnnouncementTitle).HasMaxLength(100);
            entity.Property(e => e.AnnouncementWinnerId).HasColumnName("AnnouncementWinnerID");
            entity.Property(e => e.ContestEndDate).HasColumnType("datetime");
            entity.Property(e => e.ContestImage).HasMaxLength(255);
            entity.Property(e => e.ContestStartDate).HasColumnType("datetime");
            entity.Property(e => e.ContestTitle).HasMaxLength(100);
            entity.Property(e => e.RecipeCategoryName).HasMaxLength(50);
            entity.Property(e => e.RecipeImage).HasMaxLength(255);
            entity.Property(e => e.RecipeTitle).HasMaxLength(100);
            entity.Property(e => e.TipCategoryName).HasMaxLength(50);
            entity.Property(e => e.TipImage).HasMaxLength(255);
            entity.Property(e => e.TipTitle).HasMaxLength(100);
        });

        modelBuilder.Entity<ViewUserRole>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("View_UserRole");

            entity.Property(e => e.Address).HasMaxLength(50);

            entity.Property(e => e.Avatar).HasMaxLength(255);

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.Property(e => e.Email).HasMaxLength(50);

            entity.Property(e => e.Idroles).HasColumnName("IDRoles");

            entity.Property(e => e.Password).HasMaxLength(100);

            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.Property(e => e.RoleName).HasMaxLength(50);

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.Property(e => e.Username).HasMaxLength(50);
        });
        
        modelBuilder.Entity<ViewRecipeManagement>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewRecipeManagement");

            entity.Property(e => e.RecipeCategoryName).HasMaxLength(50);
            entity.Property(e => e.RecipeCreatedAt).HasColumnType("datetime");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.RecipeTitle).HasMaxLength(100);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewTipManagement>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewTipManagement");

            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.TipCategoryName).HasMaxLength(50);
            entity.Property(e => e.TipCreatedAt).HasColumnType("datetime");
            entity.Property(e => e.TipId).HasColumnName("TipID");
            entity.Property(e => e.TipTitle).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
