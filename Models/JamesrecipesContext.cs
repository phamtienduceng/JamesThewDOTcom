﻿using System;
using System.Collections.Generic;
using JamesRecipes.Models;
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

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<AnonymousContestEntry> AnonymousContestEntries { get; set; }

    public virtual DbSet<AnonymousRecipe> AnonymousRecipes { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<CategoriesBook> CategoriesBooks { get; set; }

    public virtual DbSet<CategoriesRecipe> CategoriesRecipes { get; set; }

    public virtual DbSet<CategoriesTip> CategoriesTips { get; set; }

    public virtual DbSet<CombinedContestScore> CombinedContestScores { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }
    public virtual DbSet<Contest> Contests { get; set; }

    public virtual DbSet<ContestEntry> ContestEntries { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ViewAnonymousContact> ViewAnonymousContacts { get; set; }

    public virtual DbSet<ViewHomepage> ViewHomepages { get; set; }
    
    public virtual DbSet<ViewUserRole> ViewUserRoles { get; set; }

    public virtual DbSet<ViewRecipeManagement> ViewRecipeManagements { get; set; }

    public virtual DbSet<ViewTipManagement> ViewTipManagements { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=jamesrecipes;User=sa;Password=Abc@1234;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId).HasName("PK__Announce__9DE4455493D8CADC");

            entity.Property(e => e.AnnouncementId).HasColumnName("AnnouncementID");
            entity.Property(e => e.AnonymousWinnerId).HasColumnName("AnonymousWinnerID");
            entity.Property(e => e.ContestId).HasColumnName("ContestID");
            entity.Property(e => e.DatePost)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.WinnerId).HasColumnName("WinnerID");

            entity.HasOne(d => d.Contest).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.ContestId)
                .HasConstraintName("FK__Announcem__Conte__2A164134");
        });

        modelBuilder.Entity<AnonymousContestEntry>(entity =>
        {
            entity.HasKey(e => e.AnonymousEntryId).HasName("PK__Anonymou__D2784E4E3BAE43DE");

            entity.Property(e => e.AnonymousEntryId).HasColumnName("AnonymousEntryID");
            entity.Property(e => e.AnonymousRecipeId).HasColumnName("AnonymousRecipeID");
            entity.Property(e => e.ContestId).HasColumnName("ContestID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);

            //entity.HasOne(d => d.AnonymousRecipe).WithMany(p => p.AnonymousContestEntries)
            //    .HasForeignKey(d => d.AnonymousRecipeId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Anonymous__Anony__2739D489");

            //entity.HasOne(d => d.Contest).WithMany(p => p.AnonymousContestEntries)
            //    .HasForeignKey(d => d.ContestId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Anonymous__Conte__2645B050");
        });

        modelBuilder.Entity<AnonymousRecipe>(entity =>
        {
            entity.HasKey(e => e.AnonymousRecipeId).HasName("PK__Anonymou__E622E8066B6A597D");

            entity.Property(e => e.AnonymousRecipeId).HasColumnName("AnonymousRecipeID");
            entity.Property(e => e.AnonymousId).HasColumnName("AnonymousID");
            entity.Property(e => e.AnonymousName).HasMaxLength(100);
            entity.Property(e => e.ContactEmail).HasMaxLength(255);
            entity.Property(e => e.ContactPhone).HasMaxLength(20);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);

            //entity.HasOne(d => d.Contest).WithMany(p => p.AnonymousRecipes)
            //    .HasForeignKey(d => d.ContestId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Anonymous__Conte__22751F6C");
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

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Books_Category");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7B7CFFB0B75");

            entity.ToTable("Cart");

            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__UserId__6E01572D");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => e.CartDetailId).HasName("PK__CartDeta__01B6A6D4DA3141C8");

            entity.ToTable("CartDetail");

            entity.Property(e => e.CartDetailId).HasColumnName("CartDetailID");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Book).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__CartDetai__BookI__71D1E811");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__CartDetai__CartI__70DDC3D8");
        });

        modelBuilder.Entity<CategoriesBook>(entity =>
        {
            entity.HasKey(e => e.CategoryBookId).HasName("PK__Categori__69DE50466D35DD5C");

            entity.Property(e => e.CategoryName).HasMaxLength(40);
        });


        modelBuilder.Entity<CategoriesRecipe>(entity =>
        {
            entity.HasKey(e => e.CategoryRecipeId).HasName("PK__Categori__4A40FF8776C18B7B");

            entity.Property(e => e.CategoryRecipeId).HasColumnName("CategoryRecipeID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(255);
        });

        modelBuilder.Entity<CategoriesTip>(entity =>
        {
            entity.HasKey(e => e.CategoryTipId).HasName("PK__Categori__EFD0B85465F209CB");

            entity.Property(e => e.CategoryTipId).HasColumnName("CategoryTipID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(255);
        });

        modelBuilder.Entity<CombinedContestScore>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CombinedContestScores");

            entity.Property(e => e.EntryId).HasColumnName("EntryID");
        });
        
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Contact__5C66259B278E1C0C");

            entity.ToTable("Contact");

            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Message).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Contest>(entity =>
        {
            entity.HasKey(e => e.ContestId).HasName("PK__Contests__87DE08FAB3C16407");

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
                .HasConstraintName("FK__Contests__AdminI__4E88ABD4");
        });

        modelBuilder.Entity<ContestEntry>(entity =>
        {
            entity.HasKey(e => e.EntryId).HasName("PK__ContestE__F57BD2D71B818EC4");

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
                .HasConstraintName("FK__ContestEn__Conte__534D60F1");

            entity.HasOne(d => d.Recipe).WithMany(p => p.ContestEntries)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__ContestEn__Recip__5535A963");

            entity.HasOne(d => d.User).WithMany(p => p.ContestEntries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ContestEn__UserI__5441852A");
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.HasKey(e => e.Faqid).HasName("PK__FAQ__4B89D1E24DD645D9");

            entity.ToTable("FAQ");

            entity.Property(e => e.Faqid).HasColumnName("FAQID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6B5FE7047");

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
                .HasConstraintName("FK__Feedback__Recipe__619B8048");

            entity.HasOne(d => d.Tip).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TipId)
                .HasConstraintName("FK__Feedback__TipID__60A75C0F");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__UserID__5DCAEF64");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF6345BEFA");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__74AE54BC");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30C350E575E");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__OrderDeta__BookI__797309D9");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__787EE5A0");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipes__FDD988D0FD9B638D");

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
                .HasConstraintName("FK__Recipes__Categor__4222D4EF");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Recipes__UserID__412EB0B6");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A548CBB47");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasKey(e => e.TipId).HasName("PK__Tips__2DB1A1A8EC336062");

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
                .HasConstraintName("FK__Tips__CategoryTi__48CFD27E");

            entity.HasOne(d => d.User).WithMany(p => p.Tips)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Tips__UserID__47DBAE45");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC8EDD51DB");

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
                .HasConstraintName("FK__Users__RoleID__398D8EEE");
        });

        modelBuilder.Entity<ViewAnonymousContact>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewAnonymousContacts");

            entity.Property(e => e.AnonymousId).HasColumnName("AnonymousID");
            entity.Property(e => e.AnonymousRecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AnonymousRecipeID");
            entity.Property(e => e.ContactEmail).HasMaxLength(255);
            entity.Property(e => e.ContactPhone).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);
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
        
        modelBuilder.Entity<ViewUserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_UserRole");

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
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
