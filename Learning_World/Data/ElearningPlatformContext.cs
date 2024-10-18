using System;
using System.Collections.Generic;
using Learning_World.Models;
using Microsoft.EntityFrameworkCore;

namespace Learning_World.Data;

public partial class ElearningPlatformContext : DbContext
{
    public ElearningPlatformContext()
    {
    }

    public ElearningPlatformContext(DbContextOptions<ElearningPlatformContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Payment> Payments { get; set; }


    public virtual DbSet<AdminLog> AdminLogs { get; set; }
    public virtual DbSet<LessonCompletion> LessonCompletions{ get; set; }

	public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonQuiz> LessonQuizzes { get; set; }

    public virtual DbSet<LessonText> LessonTexts { get; set; }

    public virtual DbSet<LessonType> LessonTypes { get; set; }

    public virtual DbSet<LessonVideo> LessonVideos { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<Progress> Progresses { get; set; }

    public virtual DbSet<QuizAnswer> QuizAnswers { get; set; }

    public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ELearningPlatform;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

		modelBuilder.Entity<LessonCompletion>()
			   .HasKey(lc => new { lc.UserId, lc.LessonID });


		modelBuilder.Entity<AdminLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AdminLog__5E5499A8D60897DA");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.ActionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ActionDescription).HasColumnType("ntext");
            entity.Property(e => e.ActionType).HasMaxLength(100);
            entity.Property(e => e.AdminId).HasColumnName("AdminID");

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminLogs)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__AdminLogs__Admin__6C190EBB");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BF9BC6B60");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0C7D1F38C").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__BBF8A7E1134083D6");

            entity.Property(e => e.CertificateId).HasColumnName("CertificateID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.IssueDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Certifica__Cours__68487DD7");

            entity.HasOne(d => d.User).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Certifica__UserI__6754599E");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D7187554E0382");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.AverageRating).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.DifficultyLevel).HasMaxLength(20);
            entity.Property(e => e.LastUpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PublicationStatus).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasMany(d => d.Categories).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CourseCat__Categ__35BCFE0A"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CourseCat__Cours__34C8D9D1"),
                    j =>
                    {
                        j.HasKey("CourseId", "CategoryId").HasName("PK__CourseCa__68BDE225DC553264");
                        j.ToTable("CourseCategories");
                        j.IndexerProperty<int>("CourseId").HasColumnName("CourseID");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("CategoryID");
                    });

            entity.HasMany(d => d.Users).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseInstructor",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CourseIns__UserI__398D8EEE"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CourseIns__Cours__38996AB5"),
                    j =>
                    {
                        j.HasKey("CourseId", "UserId").HasName("PK__CourseIn__1855FD4D3A981531");
                        j.ToTable("CourseInstructors");
                        j.IndexerProperty<int>("CourseId").HasColumnName("CourseID");
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                    });
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F6877FBD79853B5");

            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Enrollmen__Cours__59063A47");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Enrollmen__UserI__5812160E");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lessons__B084ACB0C98561C0");

            entity.Property(e => e.LessonId).HasColumnName("LessonID");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.LessonTypeId).HasColumnName("LessonTypeID");
            entity.Property(e => e.PartId).HasColumnName("PartID");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.LessonType).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.LessonTypeId)
                .HasConstraintName("FK__Lessons__LessonT__45F365D3");

            entity.HasOne(d => d.Part).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__Lessons__PartID__44FF419A");
        });

        modelBuilder.Entity<LessonQuiz>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__LessonQu__B084ACB02DC267E3");

            entity.Property(e => e.LessonId)
                .ValueGeneratedNever()
                .HasColumnName("LessonID");

            entity.HasOne(d => d.Lesson).WithOne(p => p.LessonQuiz)
                .HasForeignKey<LessonQuiz>(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LessonQui__Lesso__4E88ABD4");
        });

        modelBuilder.Entity<LessonText>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__LessonTe__B084ACB0025CCEAC");

            entity.Property(e => e.LessonId)
                .ValueGeneratedNever()
                .HasColumnName("LessonID");
            entity.Property(e => e.Content).HasColumnType("ntext");

            entity.HasOne(d => d.Lesson).WithOne(p => p.LessonText)
                .HasForeignKey<LessonText>(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LessonTex__Lesso__4BAC3F29");
        });

        modelBuilder.Entity<LessonType>(entity =>
        {
            entity.HasKey(e => e.LessonTypeId).HasName("PK__LessonTy__D7FA802EA01B0CE9");

            entity.HasIndex(e => e.TypeName, "UQ__LessonTy__D4E7DFA82BE91D3F").IsUnique();

            entity.Property(e => e.LessonTypeId).HasColumnName("LessonTypeID");
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<LessonVideo>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__LessonVi__B084ACB0E0B00794");

            entity.Property(e => e.LessonId)
                .ValueGeneratedNever()
                .HasColumnName("LessonID");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(255)
                .HasColumnName("VideoURL");

            entity.HasOne(d => d.Lesson).WithOne(p => p.LessonVideo)
                .HasForeignKey<LessonVideo>(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LessonVid__Lesso__48CFD27E");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PK__Modules__2B74778781B5A0F0");

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Course).WithMany(p => p.Modules)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Modules__CourseI__3C69FB99");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.PartId).HasName("PK__Parts__7C3F0D304B161E31");

            entity.Property(e => e.PartId).HasColumnName("PartID");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Module).WithMany(p => p.Parts)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__Parts__ModuleID__3F466844");
        });

        modelBuilder.Entity<Progress>(entity =>
        {
            entity.HasKey(e => e.ProgressId).HasName("PK__Progress__BAE29C85AE882904");

            entity.HasIndex(e => new { e.UserId, e.PartId }, "UQ_UserPart").IsUnique();

            entity.Property(e => e.ProgressId).HasColumnName("ProgressID");
            entity.Property(e => e.CompletionDate).HasColumnType("datetime");
            entity.Property(e => e.PartId).HasColumnName("PartID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Part).WithMany(p => p.Progresses)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__Progresse__PartI__5DCAEF64");

            entity.HasOne(d => d.User).WithMany(p => p.Progresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Progresse__UserI__5CD6CB2B");
        });

        modelBuilder.Entity<QuizAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__QuizAnsw__D48250242E8B8C67");

            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.AnswerText).HasColumnType("ntext");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.QuizAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__QuizAnswe__Quest__5441852A");
        });

        modelBuilder.Entity<QuizQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__QuizQues__0DC06F8C8F8E6CC8");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.LessonId).HasColumnName("LessonID");
            entity.Property(e => e.QuestionText).HasColumnType("ntext");

            entity.HasOne(d => d.Lesson).WithMany(p => p.QuizQuestions)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("FK__QuizQuest__Lesso__5165187F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A76B48721");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160D9631DE4").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4BA4332E5C");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Transacti__Cours__6383C8BA");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Transacti__UserI__628FA481");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC78E15E92");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053488B33831").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__RoleI__2B3F6F97"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__UserI__2A4B4B5E"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF27604F1841E1CE");
                        j.ToTable("UserRoles");
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        j.IndexerProperty<int>("RoleId").HasColumnName("RoleID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
