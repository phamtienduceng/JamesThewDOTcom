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

    public virtual DbSet<AnonymousContestEntry> AnonymousContestEntries { get; set; }

    public virtual DbSet<AnonymousRecipe> AnonymousRecipes { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<CategoriesBook> CategoriesBooks { get; set; }

    public virtual DbSet<CategoriesRecipe> CategoriesRecipes { get; set; }

    public virtual DbSet<CategoriesTip> CategoriesTips { get; set; }

    public virtual DbSet<CombinedContestScore> CombinedContestScores { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Contest> Contests { get; set; }

    public virtual DbSet<ContestEntry> ContestEntries { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ViewAnonymousContact> ViewAnonymousContacts { get; set; }
    public virtual DbSet<ViewUserRole> ViewUserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.,1500;Database=s20;User=sa;Password=12345678;TrustServerCertificate=True");

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

            /*entity.HasOne(d => d.WinnerNavigation).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.Winner)
                .HasConstraintName("FK__Announcem__Winne__440B1D61");*/
        });

        modelBuilder.Entity<AnonymousContestEntry>(entity =>
        {
            entity.HasKey(e => e.AnonymousEntryId).HasName("PK__Anonymou__D2784E4E7BF58BF3");

            entity.Property(e => e.AnonymousEntryId).HasColumnName("AnonymousEntryID");
            entity.Property(e => e.AnonymousRecipeId).HasColumnName("AnonymousRecipeID");
            entity.Property(e => e.ContestId).HasColumnName("ContestID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);

            /*entity.HasOne(d => d.AnonymousRecipe).WithMany(p => p.AnonymousContestEntries)
                .HasForeignKey(d => d.AnonymousRecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Anonymous__Anony__477199F1");

            entity.HasOne(d => d.Contest).WithMany(p => p.AnonymousContestEntries)
                .HasForeignKey(d => d.ContestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Anonymous__Conte__467D75B8");*/
        });

        modelBuilder.Entity<AnonymousRecipe>(entity =>
        {
            entity.HasKey(e => e.AnonymousRecipeId).HasName("PK__Anonymou__E622E806478F0579");

            entity.Property(e => e.AnonymousRecipeId).HasColumnName("AnonymousRecipeID");
            entity.Property(e => e.AnonymousId).HasColumnName("AnonymousID");
            entity.Property(e => e.ContactEmail).HasMaxLength(255);
            entity.Property(e => e.ContactPhone).HasMaxLength(20);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C227E41EB1AA");

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
        
        modelBuilder.Entity<CategoriesBook>(entity =>
        {
            entity.HasKey(e => e.CategoryBookId).HasName("PK__Categori__69DE50466D35DD5C");

            entity.Property(e => e.CategoryName).HasMaxLength(40);
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
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");
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
                .HasConstraintName("FK__ContestEn__Recip__05D8E0BE");

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
                .HasConstraintName("FK__Feedback__Recipe__08B54D69");

            entity.HasOne(d => d.Tip).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TipId)
                .HasConstraintName("FK__Feedback__TipID__5629CD9C");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__UserID__534D60F1");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.MembershipId).HasName("PK__Membersh__92A7859947D62D54");

            entity.ToTable("Membership");

            entity.Property(e => e.MembershipId).HasColumnName("MembershipID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Membershi__UserI__1B9317B3");
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
            entity.Property(e => e.Rating).HasColumnName("rating");
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
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
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
