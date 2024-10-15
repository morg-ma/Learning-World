using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning_World.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__19093A2B74F24E96", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    DifficultyLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    PublicationStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaxEnrollment = table.Column<int>(type: "int", nullable: true),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Courses__C92D7187A258A338", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "LessonTypes",
                columns: table => new
                {
                    LessonTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LessonTy__D7FA802E98E37A50", x => x.LessonTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE3A453B49CD", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC8F4E8754", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "CourseCategories",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourseCa__68BDE22500306A5E", x => new { x.CourseID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK__CourseCat__Categ__35BCFE0A",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__CourseCat__Cours__34C8D9D1",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    OrderInCourse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Modules__2B7477876AB6520C", x => x.ModuleID);
                    table.ForeignKey(
                        name: "FK__Modules__CourseI__3C69FB99",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                });

            migrationBuilder.CreateTable(
                name: "AdminLogs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminID = table.Column<int>(type: "int", nullable: true),
                    ActionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActionDescription = table.Column<string>(type: "ntext", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AdminLog__5E5499A8812D5CFC", x => x.LogID);
                    table.ForeignKey(
                        name: "FK__AdminLogs__Admin__68487DD7",
                        column: x => x.AdminID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    CertificateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Certific__BBF8A7E110A3059F", x => x.CertificateID);
                    table.ForeignKey(
                        name: "FK__Certifica__Cours__6477ECF3",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                    table.ForeignKey(
                        name: "FK__Certifica__UserI__6383C8BA",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "CourseInstructors",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourseIn__1855FD4D7825CFB5", x => new { x.CourseID, x.UserID });
                    table.ForeignKey(
                        name: "FK__CourseIns__Cours__38996AB5",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                    table.ForeignKey(
                        name: "FK__CourseIns__UserI__398D8EEE",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Enrollme__7F6877FB0DFF4221", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK__Enrollmen__Cours__5629CD9C",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                    table.ForeignKey(
                        name: "FK__Enrollmen__UserI__5535A963",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__55433A4B5371056C", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK__Transacti__Cours__5FB337D6",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                    table.ForeignKey(
                        name: "FK__Transacti__UserI__5EBF139D",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__AF27604F5A56D66B", x => new { x.UserID, x.RoleID });
                    table.ForeignKey(
                        name: "FK__UserRoles__RoleI__2B3F6F97",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                    table.ForeignKey(
                        name: "FK__UserRoles__UserI__2A4B4B5E",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleID = table.Column<int>(type: "int", nullable: true),
                    LessonTypeID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    OrderInModule = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lessons__B084ACB0A31A6376", x => x.LessonID);
                    table.ForeignKey(
                        name: "FK__Lessons__LessonT__4316F928",
                        column: x => x.LessonTypeID,
                        principalTable: "LessonTypes",
                        principalColumn: "LessonTypeID");
                    table.ForeignKey(
                        name: "FK__Lessons__ModuleI__4222D4EF",
                        column: x => x.ModuleID,
                        principalTable: "Modules",
                        principalColumn: "ModuleID");
                });

            migrationBuilder.CreateTable(
                name: "LessonQuizzes",
                columns: table => new
                {
                    LessonID = table.Column<int>(type: "int", nullable: false),
                    PassingScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LessonQu__B084ACB0C53F76A0", x => x.LessonID);
                    table.ForeignKey(
                        name: "FK__LessonQui__Lesso__4BAC3F29",
                        column: x => x.LessonID,
                        principalTable: "Lessons",
                        principalColumn: "LessonID");
                });

            migrationBuilder.CreateTable(
                name: "LessonTexts",
                columns: table => new
                {
                    LessonID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LessonTe__B084ACB0DF089528", x => x.LessonID);
                    table.ForeignKey(
                        name: "FK__LessonTex__Lesso__48CFD27E",
                        column: x => x.LessonID,
                        principalTable: "Lessons",
                        principalColumn: "LessonID");
                });

            migrationBuilder.CreateTable(
                name: "LessonVideos",
                columns: table => new
                {
                    LessonID = table.Column<int>(type: "int", nullable: false),
                    VideoURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LessonVi__B084ACB022F26045", x => x.LessonID);
                    table.ForeignKey(
                        name: "FK__LessonVid__Lesso__45F365D3",
                        column: x => x.LessonID,
                        principalTable: "Lessons",
                        principalColumn: "LessonID");
                });

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    ProgressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    LessonID = table.Column<int>(type: "int", nullable: true),
                    CompletionStatus = table.Column<bool>(type: "bit", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Progress__BAE29C85D69AC3BD", x => x.ProgressID);
                    table.ForeignKey(
                        name: "FK__Progresse__Lesso__5AEE82B9",
                        column: x => x.LessonID,
                        principalTable: "Lessons",
                        principalColumn: "LessonID");
                    table.ForeignKey(
                        name: "FK__Progresse__UserI__59FA5E80",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonID = table.Column<int>(type: "int", nullable: true),
                    QuestionText = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuizQues__0DC06F8C6623E068", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK__QuizQuest__Lesso__4E88ABD4",
                        column: x => x.LessonID,
                        principalTable: "LessonQuizzes",
                        principalColumn: "LessonID");
                });

            migrationBuilder.CreateTable(
                name: "QuizAnswers",
                columns: table => new
                {
                    AnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<int>(type: "int", nullable: true),
                    AnswerText = table.Column<string>(type: "ntext", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuizAnsw__D48250247DA5BAE7", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK__QuizAnswe__Quest__5165187F",
                        column: x => x.QuestionID,
                        principalTable: "QuizQuestions",
                        principalColumn: "QuestionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminLogs_AdminID",
                table: "AdminLogs",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "UQ__Categori__8517B2E0FDEADC86",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_CourseID",
                table: "Certificates",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_UserID",
                table: "Certificates",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCategories_CategoryID",
                table: "CourseCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstructors_UserID",
                table: "CourseInstructors",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseID",
                table: "Enrollments",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserID",
                table: "Enrollments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LessonTypeID",
                table: "Lessons",
                column: "LessonTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ModuleID",
                table: "Lessons",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "UQ__LessonTy__D4E7DFA87562B604",
                table: "LessonTypes",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseID",
                table: "Modules",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_LessonID",
                table: "Progresses",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_UserID",
                table: "Progresses",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswers_QuestionID",
                table: "QuizAnswers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_LessonID",
                table: "QuizQuestions",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__8A2B61607E447F19",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CourseID",
                table: "Transactions",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserID",
                table: "Transactions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleID",
                table: "UserRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D1053449025D91",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminLogs");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "CourseCategories");

            migrationBuilder.DropTable(
                name: "CourseInstructors");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "LessonTexts");

            migrationBuilder.DropTable(
                name: "LessonVideos");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "QuizAnswers");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LessonQuizzes");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "LessonTypes");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
