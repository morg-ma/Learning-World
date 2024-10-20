﻿// <auto-generated />
using System;
using Learning_World.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Learning_World.Migrations
{
    [DbContext(typeof(ElearningPlatformContext))]
    partial class ElearningPlatformContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseCategory", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.HasKey("CourseId", "CategoryId")
                        .HasName("PK__CourseCa__68BDE225DC553264");

                    b.HasIndex("CategoryId");

                    b.ToTable("CourseCategories", (string)null);
                });

            modelBuilder.Entity("CourseInstructor", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("CourseId", "UserId")
                        .HasName("PK__CourseIn__1855FD4D3A981531");

                    b.HasIndex("UserId");

                    b.ToTable("CourseInstructors", (string)null);
                });

            modelBuilder.Entity("Learning_World.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId")
                        .HasName("PK__Categori__19093A2BF9BC6B60");

                    b.HasIndex(new[] { "CategoryName" }, "UQ__Categori__8517B2E0C7D1F38C")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Learning_World.Models.Certificate", b =>
                {
                    b.Property<int>("CertificateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CertificateID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateId"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<DateTime>("IssueDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("CertificateId")
                        .HasName("PK__Certific__BBF8A7E1134083D6");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("Learning_World.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<decimal?>("AverageRating")
                        .HasColumnType("decimal(3, 2)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("MaxEnrollment")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PublicationStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("CourseId")
                        .HasName("PK__Courses__C92D7187554E0382");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Learning_World.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EnrollmentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrollmentId"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<DateTime>("EnrollmentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("PaymentID")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("EnrollmentId")
                        .HasName("PK__Enrollme__7F6877FBD79853B5");

                    b.HasIndex("CourseId");

                    b.HasIndex("PaymentID")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("Learning_World.Models.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LessonID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LessonId"));

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<int?>("LessonTypeId")
                        .HasColumnType("int")
                        .HasColumnName("LessonTypeID");

                    b.Property<int>("OrderInPart")
                        .HasColumnType("int");

                    b.Property<int?>("PartId")
                        .HasColumnType("int")
                        .HasColumnName("PartID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("LessonId")
                        .HasName("PK__Lessons__B084ACB0C98561C0");

                    b.HasIndex("LessonTypeId");

                    b.HasIndex("PartId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Learning_World.Models.LessonCompletion", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("LessonID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "LessonID");

                    b.HasIndex("LessonID");

                    b.HasIndex("ModuleId");

                    b.ToTable("LessonCompletions");
                });

            modelBuilder.Entity("Learning_World.Models.LessonQuiz", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("int")
                        .HasColumnName("LessonID");

                    b.Property<int>("PassingScore")
                        .HasColumnType("int");

                    b.HasKey("LessonId")
                        .HasName("PK__LessonQu__B084ACB02DC267E3");

                    b.ToTable("LessonQuizzes");
                });

            modelBuilder.Entity("Learning_World.Models.LessonText", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("int")
                        .HasColumnName("LessonID");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.HasKey("LessonId")
                        .HasName("PK__LessonTe__B084ACB0025CCEAC");

                    b.ToTable("LessonTexts");
                });

            modelBuilder.Entity("Learning_World.Models.LessonType", b =>
                {
                    b.Property<int>("LessonTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LessonTypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LessonTypeId"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("LessonTypeId")
                        .HasName("PK__LessonTy__D7FA802EA01B0CE9");

                    b.HasIndex(new[] { "TypeName" }, "UQ__LessonTy__D4E7DFA82BE91D3F")
                        .IsUnique();

                    b.ToTable("LessonTypes");
                });

            modelBuilder.Entity("Learning_World.Models.LessonVideo", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("int")
                        .HasColumnName("LessonID");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("VideoURL");

                    b.HasKey("LessonId")
                        .HasName("PK__LessonVi__B084ACB0E0B00794");

                    b.ToTable("LessonVideos");
                });

            modelBuilder.Entity("Learning_World.Models.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ModuleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModuleId"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<int>("OrderInCourse")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ModuleId")
                        .HasName("PK__Modules__2B74778781B5A0F0");

                    b.HasIndex("CourseId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Learning_World.Models.Part", b =>
                {
                    b.Property<int>("PartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PartID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PartId"));

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int")
                        .HasColumnName("ModuleID");

                    b.Property<int>("OrderInModule")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("PartId")
                        .HasName("PK__Parts__7C3F0D304B161E31");

                    b.HasIndex("ModuleId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("Learning_World.Models.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentID"));

                    b.Property<string>("CVC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpiryDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Learning_World.Models.QuizAnswer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AnswerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnswerId"));

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int")
                        .HasColumnName("QuestionID");

                    b.HasKey("AnswerId")
                        .HasName("PK__QuizAnsw__D48250242E8B8C67");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuizAnswers");
                });

            modelBuilder.Entity("Learning_World.Models.QuizQuestion", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("QuestionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionId"));

                    b.Property<int?>("LessonId")
                        .HasColumnType("int")
                        .HasColumnName("LessonID");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.HasKey("QuestionId")
                        .HasName("PK__QuizQues__0DC06F8C8F8E6CC8");

                    b.HasIndex("LessonId");

                    b.ToTable("QuizQuestions");
                });

            modelBuilder.Entity("Learning_World.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CourseCategory", b =>
                {
                    b.HasOne("Learning_World.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK__CourseCat__Categ__35BCFE0A");

                    b.HasOne("Learning_World.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .IsRequired()
                        .HasConstraintName("FK__CourseCat__Cours__34C8D9D1");
                });

            modelBuilder.Entity("CourseInstructor", b =>
                {
                    b.HasOne("Learning_World.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .IsRequired()
                        .HasConstraintName("FK__CourseIns__Cours__38996AB5");

                    b.HasOne("Learning_World.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__CourseIns__UserI__398D8EEE");
                });

            modelBuilder.Entity("Learning_World.Models.Certificate", b =>
                {
                    b.HasOne("Learning_World.Models.Course", "Course")
                        .WithMany("Certificates")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK__Certifica__Cours__68487DD7");

                    b.HasOne("Learning_World.Models.User", "User")
                        .WithMany("Certificates")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Certifica__UserI__6754599E");

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Learning_World.Models.Enrollment", b =>
                {
                    b.HasOne("Learning_World.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK__Enrollmen__Cours__59063A47");

                    b.HasOne("Learning_World.Models.Payment", "Payment")
                        .WithOne("Enrollment")
                        .HasForeignKey("Learning_World.Models.Enrollment", "PaymentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning_World.Models.User", "User")
                        .WithMany("Enrollments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Enrollmen__UserI__5812160E");

                    b.Navigation("Course");

                    b.Navigation("Payment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Learning_World.Models.Lesson", b =>
                {
                    b.HasOne("Learning_World.Models.LessonType", "LessonType")
                        .WithMany("Lessons")
                        .HasForeignKey("LessonTypeId")
                        .HasConstraintName("FK__Lessons__LessonT__45F365D3");

                    b.HasOne("Learning_World.Models.Part", "Part")
                        .WithMany("Lessons")
                        .HasForeignKey("PartId")
                        .HasConstraintName("FK__Lessons__PartID__44FF419A");

                    b.Navigation("LessonType");

                    b.Navigation("Part");
                });

            modelBuilder.Entity("Learning_World.Models.LessonCompletion", b =>
                {
                    b.HasOne("Learning_World.Models.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning_World.Models.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning_World.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Module");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Learning_World.Models.LessonQuiz", b =>
                {
                    b.HasOne("Learning_World.Models.Lesson", "Lesson")
                        .WithOne("LessonQuiz")
                        .HasForeignKey("Learning_World.Models.LessonQuiz", "LessonId")
                        .IsRequired()
                        .HasConstraintName("FK__LessonQui__Lesso__4E88ABD4");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("Learning_World.Models.LessonText", b =>
                {
                    b.HasOne("Learning_World.Models.Lesson", "Lesson")
                        .WithOne("LessonText")
                        .HasForeignKey("Learning_World.Models.LessonText", "LessonId")
                        .IsRequired()
                        .HasConstraintName("FK__LessonTex__Lesso__4BAC3F29");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("Learning_World.Models.LessonVideo", b =>
                {
                    b.HasOne("Learning_World.Models.Lesson", "Lesson")
                        .WithOne("LessonVideo")
                        .HasForeignKey("Learning_World.Models.LessonVideo", "LessonId")
                        .IsRequired()
                        .HasConstraintName("FK__LessonVid__Lesso__48CFD27E");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("Learning_World.Models.Module", b =>
                {
                    b.HasOne("Learning_World.Models.Course", "Course")
                        .WithMany("Modules")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK__Modules__CourseI__3C69FB99");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Learning_World.Models.Part", b =>
                {
                    b.HasOne("Learning_World.Models.Module", "Module")
                        .WithMany("Parts")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("FK__Parts__ModuleID__3F466844");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("Learning_World.Models.QuizAnswer", b =>
                {
                    b.HasOne("Learning_World.Models.QuizQuestion", "Question")
                        .WithMany("QuizAnswers")
                        .HasForeignKey("QuestionId")
                        .HasConstraintName("FK__QuizAnswe__Quest__5441852A");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Learning_World.Models.QuizQuestion", b =>
                {
                    b.HasOne("Learning_World.Models.LessonQuiz", "Lesson")
                        .WithMany("QuizQuestions")
                        .HasForeignKey("LessonId")
                        .HasConstraintName("FK__QuizQuest__Lesso__5165187F");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Learning_World.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Learning_World.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning_World.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Learning_World.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Learning_World.Models.Course", b =>
                {
                    b.Navigation("Certificates");

                    b.Navigation("Enrollments");

                    b.Navigation("Modules");
                });

            modelBuilder.Entity("Learning_World.Models.Lesson", b =>
                {
                    b.Navigation("LessonQuiz");

                    b.Navigation("LessonText");

                    b.Navigation("LessonVideo");
                });

            modelBuilder.Entity("Learning_World.Models.LessonQuiz", b =>
                {
                    b.Navigation("QuizQuestions");
                });

            modelBuilder.Entity("Learning_World.Models.LessonType", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("Learning_World.Models.Module", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("Learning_World.Models.Part", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("Learning_World.Models.Payment", b =>
                {
                    b.Navigation("Enrollment")
                        .IsRequired();
                });

            modelBuilder.Entity("Learning_World.Models.QuizQuestion", b =>
                {
                    b.Navigation("QuizAnswers");
                });

            modelBuilder.Entity("Learning_World.Models.User", b =>
                {
                    b.Navigation("Certificates");

                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
