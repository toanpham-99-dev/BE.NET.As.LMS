using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BE.NET.As.LMS.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Syllabus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstructorHashCodeCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    DescriptionDetailId = table.Column<long>(type: "bigint", nullable: true),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationUsers",
                columns: table => new
                {
                    NotificationId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUsers", x => new { x.NotificationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_NotificationUsers_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotificationUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMedias_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserHashCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleHashCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DescriptionDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DescriptionDetails_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UserHashCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseHashCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalLesson = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCourses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JsonContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonHashCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
                    UserHashCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Quizs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
                    QuizContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizs_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentUsers",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AssignmentId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentUsers", x => new { x.UserId, x.AssignmentId });
                    table.ForeignKey(
                        name: "FK_AssignmentUsers_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignmentUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuizId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Quizs_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuizUsers",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    QuizId = table.Column<long>(type: "bigint", nullable: false),
                    HashCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Score = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizUsers", x => new { x.UserId, x.QuizId });
                    table.ForeignKey(
                        name: "FK_QuizUsers_Quizs_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Alias", "CreatedAt", "CreatedBy", "Description", "HashCode", "ImageURL", "ParentId", "Status", "Title", "UpdatedAt", "UpdatedBy", "isDeleted" },
                values: new object[] { 1L, "web", new DateTime(2022, 1, 14, 15, 8, 18, 395, DateTimeKind.Local).AddTicks(8780), "admin", "Kiến thức về Web", "3214928A0-E936-4BF0-BD08-12DF85F34979", "link1", null, 0, "Web", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(3579), "admin", false });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "Content", "CreatedAt", "HashCode", "Link", "Status", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 1L, "Lập trình cơ bản", new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(2455), "8A4EB22E-451D-4AB0-B1C1-54CEC2BFA4B4", "Linkvd1", 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(2618), false },
                    { 2L, "Lập trình Java", new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(4837), "35DAE15D-6B67-4979-A5DB-48E53950B538", "Linkvd2", 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(4846), false },
                    { 3L, "Lập trình Python", new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(4858), "AA7E120E-0DFE-449C-97C6-FCA8B2EB9CAA", "Linkvd3", 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(4860), false },
                    { 4L, "Lập trình C#", new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(4868), "52C60A16-0CDF-48B3-B033-39DC34DCC93D", "Linkvd4", 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(4871), false },
                    { 5L, "Lập trình nâng cao", new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(4878), "CEA56C0C-818A-410D-AC2C-D58D2D943CCD", "Linkvd5", 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(4880), false }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "HashCode", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, "5d2146b9-9491-40c8-b1ad-691cec7b3194", "Quản Lý", "3799D14C-03B5-461B-A6C0-AE3F8281B0B1", "admin", "administrator" },
                    { 2L, "0d6d8672-bb8d-448a-8c75-811449ba1697", "Giảng Viên", "2787DAC0-AFDF-43F5-8FBD-257B5857CA5B", "instructor", "Instructor" },
                    { 3L, "c023f128-d92c-44dc-86eb-8cd91ba95e5b", "Người Dùng", "1DB8AA44-AEF6-44C4-A34B-5845265DE6AE", "user", "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "BI", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "HashCode", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "isDeleted" },
                values: new object[,]
                {
                    { 1L, 0, "", null, "28ee5c7a-4199-407e-9184-c9de5d4f0923", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, "Admin", "355928A0-E936-4BF0-BD08-12DF85F34979", false, null, null, null, "AQAAAAEAACcQAAAAENLRKbX9QCelIRPdC5kD4JsGT3GmSjqiKUZghgkdKF6yGvd/rli4LMxVs0+xdFkfHA==", "0123456789", false, null, false, "admin", false },
                    { 2L, 0, "", null, "759ad3be-8200-4522-b536-34c797ce1d1d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "instructor1@gmail.com", true, "Giang Vien", "3670CCE4-A029-4A83-A047-BB1FE3E1D3F1", false, null, null, null, "AQAAAAEAACcQAAAAEPbi3vAq0VHQvvnZ+SFIEq7n9t9jqch4UQYuMmNz/uRbgppzDLn1qChN0MF+kgcWKA==", "0123456788", false, null, false, "instructor1", false },
                    { 3L, 0, "", null, "4af96991-bb29-4814-a022-85b9b5b15dee", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "instructor2@gmail.com", true, "Giảng viên 1", "D9E58D24-67C4-489B-8E12-4E93AF068726", false, null, null, null, "AQAAAAEAACcQAAAAEC5c2L72XwKceSMUJ48GDyLp2r4Eaoq+bkrmz/MfJD4XGVh+2o+t3jl/ILMz2mA1Uw==", "0923456789", false, null, false, "instructor2", false },
                    { 4L, 0, "", null, "5c51e79d-84a8-447d-b88e-46ec7b6201fb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "instructor3@gmail.com", true, "Giảng viên 3", "88385ACA-7D80-4B6F-8CDC-33E2E6EBFF41", false, null, null, null, "AQAAAAEAACcQAAAAED33oOSCv2Ywx1Fzw0TDaQCVBL86T6DsGmt9jjsYMBWZkiBelTzKtr0RKXLIZN15Ug==", "0932345678", false, null, false, "instructor3", false },
                    { 5L, 0, "", null, "c43d3c1b-6b45-454a-8a23-4bd8844bc248", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1@gmail.com", true, "Nguoi Dung 1", "378EF81B-BE99-489F-AD23-9990F655C7D5", false, null, null, null, "AQAAAAEAACcQAAAAEBTHA/oL5UCr9fiRjgLHjs9BDugjALn8HTeKolUpWff+w9BecDghv5EDjwBTX6GfJw==", "0123456787", false, null, false, "user1", false },
                    { 6L, 0, "", null, "b1bb8583-b0a9-4e2b-98a1-1fa835edfab1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2@gmail.com", true, "Người dùng 1", "D9DFDF78-8086-419C-8FA7-BFD5EE32167B", false, null, null, null, "AQAAAAEAACcQAAAAEP/3827RK/N5YLkl7Cjr5Uss4yyORWHqdaHwvsYclPyE2VNcliuC2nqCbIG2e9ixLQ==", "0923456777", false, null, false, "user2", false },
                    { 7L, 0, "", null, "a381b4d0-b9ef-4bb9-9af3-9a56188e86c4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user3@gmail.com", true, "Người dùng 2", "0656C94D-26ED-4E21-BBC8-A62528D5F42E", false, null, null, null, "AQAAAAEAACcQAAAAEPuVCvWfqhmBgQogoi72ZoNWIbvuEXDElOnRxFggrI/9An22zmbl/L0c8/5/ycROcw==", "0909345097", false, null, false, "user3", false }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Alias", "CreatedAt", "CreatedBy", "Description", "HashCode", "ImageURL", "ParentId", "Status", "Title", "UpdatedAt", "UpdatedBy", "isDeleted" },
                values: new object[] { 2L, "design", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(8129), "instructor 1", "Học thiết kế cơ bản", "375AF6DE-72A0-4601-83A7-6740A7D4E0F0", "link1", 1L, 0, "Design", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(8138), "instructor 1", false });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedAt", "CreatedBy", "DeletedBy", "Description", "DescriptionDetailId", "Duration", "HashCode", "ImageURL", "InstructorHashCodeCreated", "Level", "Price", "PublishedBy", "Rating", "Status", "Summary", "Syllabus", "Title", "UpdatedAt", "UpdatedBy", "ViewCount", "isDeleted" },
                values: new object[,]
                {
                    { 1L, 1L, "<ul><li> Giúp bạn nắm được kiến thức cơ bản về HTML và CSS </li>\"<li> Hiểu được thành phần của một trang web</li>\"<li> Xây dựng một trang web tĩnh cơ bản.</li></ul>", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(9215), "FullStack", null, "Để có cái nhìn tổng quan về ngành IT - Lập trình web các bạn nên xem các videos tại khóa này trước nhé.", null, new TimeSpan(0, 0, 0, 0, 0), "1234928A0-E936-4BF0-BD08-12DF85F34979", "1.png", "3670CCE4-A029-4A83-A047-BB1FE3E1D3F1", 1, 0m, null, 2.0, 1, "Tóm tắt về khóa học", "Giáo trình 1", "Kiến Thức Nhập Môn", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(9222), null, 43321, false },
                    { 2L, 1L, "<ul><li> Giúp bạn nắm được kiến thức cơ bản về HTML và CSS </li><li> Hiểu được thành phần của một trang web</li><li> Xây dựng một trang web tĩnh cơ bản.</li></ul>", new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(4283), "FullStack", null, "Để có cái nhìn tổng quan về ngành IT - Lập trình web các bạn nên xem các videos tại khóa này trước nhé.", null, new TimeSpan(0, 0, 0, 0, 0), "3221CCE4-A029-4A83-A047-BB1FE3E1D3F1", "1.png", "D9E58D24-67C4-489B-8E12-4E93AF068726", 1, 0m, null, 5.0, 1, "Tóm tắt về khóa học", "Giáo trình 1", "HTML, CSS từ Zero đến Hero", new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(4292), null, 32123, false }
                });

            migrationBuilder.InsertData(
                table: "NotificationUsers",
                columns: new[] { "NotificationId", "UserId", "HashCode" },
                values: new object[,]
                {
                    { 2L, 5L, "F66CD74D-90C5-45EA-8A80-8875E18EEFF3" },
                    { 3L, 3L, "E57E9738-523C-4123-A202-6090F75F597E" },
                    { 1L, 3L, "A8AA4D8F-F7F8-4D46-85EA-286BC604BBD0" },
                    { 2L, 4L, "6168B0D9-923D-4F81-B229-F54E6562EC1E" },
                    { 1L, 1L, "B27DAAA5-AC75-41A9-B5CC-77D9053ABC3E" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "HashCode", "Quantity", "Status", "TotalPrice", "UpdatedAt", "UserId", "isDeleted" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 1, 14, 15, 8, 18, 401, DateTimeKind.Local).AddTicks(3040), "AB314DBD-80D8-46DC-B1F4-0FDDC39EE741", 1, 0, 100000m, new DateTime(2022, 1, 14, 15, 8, 18, 401, DateTimeKind.Local).AddTicks(3072), 3L, false },
                    { 2L, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(3307), "6686CD83-31C5-49B5-AC8A-354848941111", 2, 0, 150000m, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(3344), 4L, false },
                    { 4L, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(3403), "44ACA34B-DD8A-4AF4-94C4-739560FF1D21", 2, 0, 250000m, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(3405), 4L, false }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "HashCode", "Status", "TotalPrice", "UpdatedAt", "UserId", "isDeleted" },
                values: new object[] { 5L, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(3413), "DADF67BA-191A-40E2-8FBC-AE0225A16B4A", 0, 0m, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(3415), 5L, false });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "HashCode", "Quantity", "Status", "TotalPrice", "UpdatedAt", "UserId", "isDeleted" },
                values: new object[] { 3L, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(3389), "1E70AF91-1C00-4DD5-8A0A-6EC4769CCE2B", 2, 0, 230000m, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(3394), 5L, false });

            migrationBuilder.InsertData(
                table: "SocialMedias",
                columns: new[] { "Id", "CreatedAt", "HashCode", "Link", "Name", "Status", "UpdatedAt", "UserId", "isDeleted" },
                values: new object[,]
                {
                    { 4L, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(4738), "1B3795F7-560B-4712-A292-1F0B12A38E9D", "Link media 4", "Media 4", 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(4740), 4L, false },
                    { 1L, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(2919), "05938D79-D30C-443C-B065-A1EC7E581EEA", "Link media 1", "Media 1", 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(2930), 3L, false },
                    { 2L, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(4710), "864348DE-E655-4792-828F-18C8FDA8998D", "Link media 2", "Media 2", 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(4720), 3L, false },
                    { 6L, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(4753), "C29FAF72-059F-4E32-99B6-29459DDF2470", "Link media 6", "Media 6", 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(4755), 5L, false },
                    { 3L, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(4730), "C9F0E904-28A6-4730-ACE2-D1758E762D21", "Link media 3", "Media 3", 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(4732), 4L, false },
                    { 5L, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(4747), "F480A309-E2B4-4948-904A-0556F53EB634", "Link media 5", "Media 5", 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(4748), 5L, false }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator", "HashCode", "RoleHashCode", "UserHashCode" },
                values: new object[,]
                {
                    { 3L, 5L, "UserRole", "4c674495-eed5-4c5a-b275-e67a413f2d35", "1DB8AA44-AEF6-44C4-A34B-5845265DE6AE", "378EF81B-BE99-489F-AD23-9990F655C7D5" },
                    { 1L, 1L, "UserRole", "69b5e02b-7c9e-4e17-a80f-5a36b7bbb390", "3799D14C-03B5-461B-A6C0-AE3F8281B0B1", "355928A0-E936-4BF0-BD08-12DF85F34979" },
                    { 2L, 2L, "UserRole", "a76f7071-0167-4cf1-84b7-ec6a6006eb92", "2787DAC0-AFDF-43F5-8FBD-257B5857CA5B", "3670CCE4-A029-4A83-A047-BB1FE3E1D3F1" },
                    { 3L, 6L, "UserRole", "b0c24bc0-9c21-4afe-8fa2-fbd6f3a64a22", "1DB8AA44-AEF6-44C4-A34B-5845265DE6AE", "D9DFDF78-8086-419C-8FA7-BFD5EE32167B" },
                    { 2L, 3L, "UserRole", "da9170e4-3b17-4547-86b0-92c0a2fc7a01", "2787DAC0-AFDF-43F5-8FBD-257B5857CA5B", "D9E58D24-67C4-489B-8E12-4E93AF068726" },
                    { 2L, 4L, "UserRole", "e448957a-df18-40ce-94ce-aa4a59c23209", "2787DAC0-AFDF-43F5-8FBD-257B5857CA5B", "88385ACA-7D80-4B6F-8CDC-33E2E6EBFF41" },
                    { 3L, 7L, "UserRole", "d4340eb2-a862-4ec6-9c77-c39634287fb5", "1DB8AA44-AEF6-44C4-A34B-5845265DE6AE", "0656C94D-26ED-4E21-BBC8-A62528D5F42E" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Alias", "CreatedAt", "CreatedBy", "Description", "HashCode", "ImageURL", "ParentId", "Status", "Title", "UpdatedAt", "UpdatedBy", "isDeleted" },
                values: new object[,]
                {
                    { 3L, "business", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(8148), "instructor 1", "Quản trị nhân sự", "E10D2A57-1AF2-4EB4-84CA-77F1036F7FD7", "link1", 2L, 0, "Business", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(8150), "admin", false },
                    { 4L, "basic", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(8155), "admin", "Học asp.net mvc cơ bản", "166769CE-C916-488A-A828-73D614FEE28F", "link1", 2L, 0, "Asp.net mvc", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(8156), "instructor 1", false }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedAt", "CreatedBy", "DeletedBy", "Description", "DescriptionDetailId", "Duration", "HashCode", "ImageURL", "InstructorHashCodeCreated", "Level", "Price", "PublishedBy", "Rating", "Status", "Summary", "Syllabus", "Title", "UpdatedAt", "UpdatedBy", "ViewCount", "isDeleted" },
                values: new object[] { 3L, 2L, "<ul><li> Giúp bạn nắm được kiến thức cơ bản về HTML và CSS </li><li> Hiểu được thành phần của một trang web</li><li> Xây dựng một trang web tĩnh cơ bản.</li></ul>", new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(4302), "Frontend", null, "Để có cái nhìn tổng quan về ngành IT - Lập trình web các bạn nên xem các videos tại khóa này trước nhé.", null, new TimeSpan(0, 0, 0, 0, 0), "D8E64356-21B3-4D8A-820B-E101548A033D", "3.png", "88385ACA-7D80-4B6F-8CDC-33E2E6EBFF41", 2, 90000m, null, 3.0, 1, "Tóm tắt về khóa học", "Giáo trình 2", "HTML, CSS từ cơ bản đến nâng cao", new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(4303), null, 1231, false });

            migrationBuilder.InsertData(
                table: "DescriptionDetails",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Description", "HashCode", "Status", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(9159), null, "27802CEC-BF9C-4DEA-B81F-B6755FF307C0", 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(9167), false },
                    { 2L, 2L, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(9605), null, "0C7DA9E9-00E3-4B0C-9754-3AD8AC2CB2BA", 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(9610), false }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "CourseId", "CourseName", "CreatedAt", "HashCode", "OrderId", "Price", "Status", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 1L, 1L, "Tiếng Anh chuyên ngành", new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(5213), "9F67F342-D8E5-4682-B54B-39C6C4C637F6", 1L, 100000m, 0, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(5231), false },
                    { 2L, 2L, "Asp.net mvc 3", new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(8398), "52FD4766-7284-4476-8FC5-27B161577CA4", 2L, 105000m, 0, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(8423), false }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Description", "HashCode", "Priority", "Status", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(5340), "Môi Trường, Con Người IT", "321TF81B-BE99-489F-AD23-9990F655C7D5", 1, 0, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(5348), false },
                    { 2L, 1L, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(6605), "Khái Niệm Kỹ Thuật Cần Biết", "431TF81B-BE99-489F-AD23-9990F655C7D5", 2, 0, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(6616), false },
                    { 3L, 1L, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(6623), "Phương Pháp Định Hướng", "51FE75A6-619A-49FF-A2C6-700428A48854", 3, 0, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(6625), false },
                    { 4L, 2L, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(6631), "Bắt Đầu", "1AB4F3A9-EA8A-4231-8DA4-3E42F768EF03", 1, 0, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(6633), false },
                    { 5L, 2L, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(6655), "Làm Quen Với HTML", "6D107704-F439-469E-8B1C-B2A25440D346", 2, 0, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(6657), false },
                    { 6L, 2L, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(6663), "Làm Quen Với CSS", "4d938648-0ebc-46f7-b953-093c7d28db43", 3, 0, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(6665), false }
                });

            migrationBuilder.InsertData(
                table: "UserCourses",
                columns: new[] { "CourseId", "UserId", "Completed", "CourseHashCode", "HashCode", "TotalLesson", "UserHashCode" },
                values: new object[,]
                {
                    { 1L, 1L, 1, "1234928A0-E936-4BF0-BD08-12DF85F34979", "007D5F97-57BE-43CE-81BE-86C1CD376B7A", 0, "355928A0-E936-4BF0-BD08-12DF85F34979" },
                    { 2L, 1L, 2, "1234928A0-E936-4BF0-BD08-12DF85F34979", "3555C645-FE22-471B-BE72-9B6828C6F2B4", 0, "355928A0-E936-4BF0-BD08-12DF85F34979" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Alias", "CreatedAt", "CreatedBy", "Description", "HashCode", "ImageURL", "ParentId", "Status", "Title", "UpdatedAt", "UpdatedBy", "isDeleted" },
                values: new object[] { 5L, "frontend", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(8161), "instructor 2", "Bài giảng thiết kế giao diện HTML, CSS", "0A7D4CD3-32BB-4E9F-B2FD-E68D8B467C5D", "link1", 3L, 0, "Thiết kế giao diện", new DateTime(2022, 1, 14, 15, 8, 18, 398, DateTimeKind.Local).AddTicks(8162), "instructor 2", false });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedAt", "CreatedBy", "DeletedBy", "Description", "DescriptionDetailId", "Duration", "HashCode", "ImageURL", "InstructorHashCodeCreated", "Level", "Price", "PublishedBy", "Rating", "Status", "Summary", "Syllabus", "Title", "UpdatedAt", "UpdatedBy", "ViewCount", "isDeleted" },
                values: new object[,]
                {
                    { 4L, 3L, "<ul><li> Giúp bạn nắm được kiến thức cơ bản về HTML và CSS </li><li> Hiểu được thành phần của một trang web</li><li> Xây dựng một trang web tĩnh cơ bản.</li></ul>", new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(4329), "Backend", null, "Để có cái nhìn tổng quan về ngành IT - Lập trình web các bạn nên xem các videos tại khóa này trước nhé.", null, new TimeSpan(0, 0, 0, 0, 0), "0F3B51E6-7DD9-4861-A693-53C7E258EE5E", "4.png", "D9E58D24-67C4-489B-8E12-4E93AF068726", 2, 45000m, null, 4.0, 1, "Tóm tắt về khóa học", "Giáo trình 3", "Lập trình nâng cao JavaScript", new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(4331), null, 1231, false },
                    { 5L, 3L, "Kiến thức nâng cao", new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(4337), "Backend", null, "Để có cái nhìn tổng quan về ngành IT - Lập trình web các bạn nên xem các videos tại khóa này trước nhé.", null, new TimeSpan(0, 0, 0, 0, 0), "0FE71577-FF4B-4CCD-B514-B002AED2B49F", "5.png", "3670CCE4-A029-4A83-A047-BB1FE3E1D3F1", 0, 45000m, null, 5.0, 1, "Tóm tắt về khóa học", "Giáo trình 3", "Lập trình JavaScrip Cơ Bản", new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(4338), null, 32321, false }
                });

            migrationBuilder.InsertData(
                table: "DescriptionDetails",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Description", "HashCode", "Status", "UpdatedAt", "isDeleted" },
                values: new object[] { 3L, 3L, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(9615), null, "B9137264-D116-4C19-A04B-AD1626C9279E", 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(9617), false });

            migrationBuilder.InsertData(
                table: "Lesson",
                columns: new[] { "Id", "CreatedAt", "Description", "Duration", "HashCode", "LinkVideo", "Name", "Priority", "SectionId", "Status", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(7704), "Tổng quan về mô hình", new TimeSpan(0, 0, 12, 13, 0), "521TF81B-BE99-489F-AD23-9990F655C7D5", "https://youtu.be/CyZ_O7v62h4", "Học IT cần tố chất gì? Góc nhìn khác từ chuyên gia định hướng giáo dục", 0, 1L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 399, DateTimeKind.Local).AddTicks(7715), false },
                    { 2L, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(41), "Lesson 2", new TimeSpan(0, 0, 6, 13, 0), "631TF81B-BE99-489F-AD23-9990F655C7D5", "https://youtu.be/YH-E4Y3EaT4", "Sinh viên IT đi thực tập tại doanh nghiệp cần biết những gì?", 0, 1L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(50), false },
                    { 3L, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(68), "Mô hình Client - Server là gì?", new TimeSpan(0, 0, 4, 13, 0), "BA21DA23-BCB1-41BD-95BB-669F657524BF", "https://youtu.be/zoELAirXMJY", "Mô hình Client - Server là gì?", 0, 2L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(70), false },
                    { 4L, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(78), "Domain là gì? Tên miền là gì?", new TimeSpan(0, 0, 7, 12, 0), "576225BB-C716-4F8A-8B96-51ED727DB42A", "https://youtu.be/M62l1xA5Eu8", "Domain là gì? Tên miền là gì?", 0, 2L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(80), false },
                    { 5L, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(86), "Lesson 5", new TimeSpan(0, 0, 4, 2, 0), "D3B5FFF7-E24A-419A-B058-9AC26552088C", "https://youtu.be/DpvYHLUiZpc", "Phương pháp học lập trình của Admin F8?", 0, 3L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(88), false },
                    { 6L, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(96), "Làm sao để có thu nhập cao và đi xa hơn trong ngành IT?", new TimeSpan(0, 0, 0, 0, 0), "5B4B08CB-7906-4705-9AC1-0CC5809E458E", "https://youtu.be/oF7isq9IjZM", "Làm sao để có thu nhập cao và đi xa hơn trong ngành IT?", 0, 3L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(98), false },
                    { 7L, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(120), "Làm được gì sau khóa học?", new TimeSpan(0, 0, 0, 0, 0), "9467E25E-19EC-4165-B526-A01AFACA0A48", "https://youtu.be/R6plN3FvzFY", "Làm được gì sau khóa học?", 0, 4L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(122), false },
                    { 8L, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(129), "HTML, CSS là gì?", new TimeSpan(0, 0, 0, 0, 0), "FD468249-96A5-409E-B94F-E68B52D2AD60", "https://youtu.be/zwsPND378OQ", "HTML, CSS là gì?", 0, 5L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(133), false },
                    { 9L, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(141), "Làm quen với Dev tools", new TimeSpan(0, 0, 0, 0, 0), "659FC410-323E-4B8A-907F-DA1F022161F2", "https://youtu.be/7BJiPyN4zZ0", "Làm quen với Dev tools", 0, 5L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(143), false },
                    { 10L, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(150), "Cấu trúc file HTML", new TimeSpan(0, 0, 0, 0, 0), "72501755-F532-4BB2-B14F-A83A9C1352BC", "https://youtu.be/LYnrFSGLCl8", "Cấu trúc file HTML", 0, 6L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 400, DateTimeKind.Local).AddTicks(152), false }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "CourseId", "CourseName", "CreatedAt", "HashCode", "OrderId", "Price", "Status", "UpdatedAt", "isDeleted" },
                values: new object[] { 3L, 3L, "Học Python cơ bản", new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(8439), "8F78610C-EDAC-493F-9C81-CE987B61168F", 3L, 110000m, 0, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(8441), false });

            migrationBuilder.InsertData(
                table: "UserCourses",
                columns: new[] { "CourseId", "UserId", "Completed", "CourseHashCode", "HashCode", "TotalLesson", "UserHashCode" },
                values: new object[] { 3L, 1L, 3, "3221CCE4-A029-4A83-A047-BB1FE3E1D3F1", "3BD97597-F69D-48BA-ADB6-9156BEB0F8E4", 0, "355928A0-E936-4BF0-BD08-12DF85F34979" });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "Id", "AssignmentName", "CreatedAt", "HashCode", "LessonId", "Status", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 8L, "Assignment name 8", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2365), "BA369E3C-4273-4293-833E-F5475CBBF36D", 5L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2367), false },
                    { 7L, "Assignment name 7", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2356), "30DA8FB2-099C-4ED3-8E0B-669BED4DAF6F", 4L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2358), false },
                    { 1L, "Assignment name 1", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(1080), "54EC57F9-6CC6-4581-BC24-34FA6E8391EC", 1L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(1105), false },
                    { 2L, "Assignment name 2", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2289), "C6BAAD97-F161-4940-B44A-FDFE0282DBD3", 1L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2299), false },
                    { 3L, "Assignment name 3", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2311), "755AB990-AD10-42F4-9AC9-5F7D0137E1A3", 2L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2313), false },
                    { 4L, "Assignment name 4", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2320), "EA2DD1E1-2CED-4344-B0B6-B931AA939500", 2L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2322), false },
                    { 5L, "Assignment name 5", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2329), "4FBACC4A-703C-49A9-B973-29E92BDA4C6D", 3L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2331), false },
                    { 6L, "Assignment name 6", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2337), "CEFF1199-EAEB-4873-BF5F-AD799108ADBF", 3L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(2338), false }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedAt", "HashCode", "LessonId", "LikeCount", "ParentId", "Status", "Title", "UpdatedAt", "UserId", "isDeleted" },
                values: new object[,]
                {
                    { 8L, "Đánh giá nội dung", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8462), "E7DEFD81-FB1E-4D10-979D-099D03E6DBA3", 8L, 17, 8L, 0, "Cần cải thiện nội dung bài học chi tiết hơn", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8464), 5L, false },
                    { 6L, "Đánh giá nội dung", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8443), "7854EEC9-937B-45E9-B476-FEFF62BAB968", 6L, 35, 6L, 0, "Cần cải thiện nội dung bài học chi tiết hơn", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8444), 4L, false },
                    { 5L, "Đánh giá nội dung", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8437), "3A7C987A-9719-4145-B478-BD366B2762B2", 5L, 15, 5L, 0, "Cần cải thiện nội dung bài học chi tiết hơn", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8438), 4L, false },
                    { 4L, "Đánh giá nội dung", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8431), "FAF27910-A14B-48CB-8788-9C05F784AA19", 4L, 20, 4L, 0, "Cần cải thiện nội dung bài học chi tiết hơn", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8432), 4L, false },
                    { 1L, "Đánh giá khóa học", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(6250), "A42B7705-7BAE-4358-89FF-DB6D92C65BA0", 1L, 30, null, 0, "Bài học hữu ích và hay", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(6262), 3L, false },
                    { 3L, "Đánh giá nội dung", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8424), "FF532BFC-A866-4C55-A7E1-B83BCB233F49", 3L, 25, 3L, 0, "Cần cải thiện nội dung bài học chi tiết hơn", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8426), 3L, false },
                    { 2L, "Đánh giá nội dung", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8329), "96B2E210-6C1D-413B-9676-A9436EF976AB", 2L, 5, 2L, 0, "Cần cải thiện nội dung bài học chi tiết hơn", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8336), 3L, false },
                    { 7L, "Đánh giá nội dung", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8456), "8226A0AE-C6AD-41DF-9519-A34040154A36", 7L, 31, 7L, 0, "Cần cải thiện nội dung bài học chi tiết hơn", new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(8457), 5L, false }
                });

            migrationBuilder.InsertData(
                table: "DescriptionDetails",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Description", "HashCode", "Status", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 4L, 4L, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(9620), null, "767A99BE-81D7-48C9-BF7A-1EBEA7A11206", 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(9622), false },
                    { 5L, 5L, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(9626), null, "70954743-B945-4EFF-8CDB-99C6CAADDAC8", 0, new DateTime(2022, 1, 14, 15, 8, 18, 405, DateTimeKind.Local).AddTicks(9627), false }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "CourseId", "CourseName", "CreatedAt", "HashCode", "OrderId", "Price", "Status", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 5L, 5L, "Tiếng Anh ngữ pháp", new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(8464), "7F90E6C8-D478-424E-81AE-8588B7EE23A6", 3L, 120000m, 0, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(8466), false },
                    { 4L, 4L, "C# cơ bản", new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(8450), "2BF76B2F-A3FC-4A51-94A4-27E82FA6EB25", 4L, 115000m, 0, new DateTime(2022, 1, 14, 15, 8, 18, 402, DateTimeKind.Local).AddTicks(8452), false }
                });

            migrationBuilder.InsertData(
                table: "Quizs",
                columns: new[] { "Id", "CreatedAt", "HashCode", "LessonId", "QuizContent", "Score", "Status", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 3L, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(1625), "61C992E8-9551-428A-9525-6EA29C2E4468", 3L, "Fresher Dot Net 10", 0, 0, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(1627), false },
                    { 4L, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(1635), "F8D24954-20EB-43BA-809E-098199746070", 4L, "Fresher Dot Net 10", 0, 0, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(1637), false },
                    { 5L, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(1645), "8B31A0D0-31FC-47BC-9EFE-DBB8AAD5A35C", 5L, "Fresher Dot Net 10", 0, 0, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(1648), false },
                    { 1L, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(56), "E0B10CBD-DA37-417A-B0FF-44B3195E603D", 1L, "Fresher Dot Net 10", 0, 0, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(78), false },
                    { 2L, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(1593), "C4862B80-BC3C-40AC-8F3C-41FF53ABF209", 2L, "Fresher Dot Net 10", 0, 0, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(1612), false }
                });

            migrationBuilder.InsertData(
                table: "UserCourses",
                columns: new[] { "CourseId", "UserId", "Completed", "CourseHashCode", "HashCode", "TotalLesson", "UserHashCode" },
                values: new object[,]
                {
                    { 5L, 1L, 1, "0F3B51E6-7DD9-4861-A693-53C7E258EE5E", "D915D8D8-A927-4E07-A563-E9B7D6C6591A", 0, "355928A0-E936-4BF0-BD08-12DF85F34979" },
                    { 4L, 1L, 4, "D8E64356-21B3-4D8A-820B-E101548A033D", "71AC38C9-4DC8-4ACA-8F04-41F795A7C7A1", 0, "355928A0-E936-4BF0-BD08-12DF85F34979" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "AnswerContent", "CreatedAt", "HashCode", "IsCorrect", "QuizId", "Status", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 9L, "Can we do this?", new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1665), "5DE494DD-A535-489B-A845-BD370DC5A611", true, 5L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1667), false },
                    { 1L, "Can we do this?", new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(9521), "D9FF643C-CE04-4CCF-A26B-431967D4EA24", true, 1L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 403, DateTimeKind.Local).AddTicks(9547), false },
                    { 2L, "How to search unicode?", new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1566), "B0687982-1590-460C-A0FA-D12D73B6B5D0", true, 1L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1579), false },
                    { 8L, "Can we do this?", new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1657), "E0FCC671-4917-4C0D-84D9-29E4FABD0BAF", true, 4L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1659), false },
                    { 7L, "Can we do this?", new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1645), "6402B5A9-1342-4BD6-8F90-0F424F1CB0FD", true, 4L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1646), false },
                    { 3L, "Filter sort?", new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1590), "625728E7-F56A-4F01-8426-DD2DDBC83A59", true, 2L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1593), false },
                    { 4L, "Can we do this?", new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1617), "6C7C1838-0C0F-4B64-92B7-530C6AAC098E", true, 2L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1619), false },
                    { 6L, "Can we do this?", new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1636), "67FF1756-D8A1-4157-8E27-B26E4762B8BC", true, 3L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1638), false },
                    { 10L, "Can we do this?", new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1672), "209B8442-4305-4BB3-9442-42AC0A18EE86", true, 5L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1674), false },
                    { 5L, "Can we do this?", new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1627), "649C0E26-0834-4437-BAAF-A4ACBC12CF78", true, 3L, 0, new DateTime(2022, 1, 14, 15, 8, 18, 404, DateTimeKind.Local).AddTicks(1629), false }
                });

            migrationBuilder.InsertData(
                table: "AssignmentUsers",
                columns: new[] { "AssignmentId", "UserId", "HashCode", "Link", "Point" },
                values: new object[,]
                {
                    { 1L, 3L, "6FAEC462-0F1E-4043-A3B4-DC8030965FF7", "Link AssignmentUser1", 0 },
                    { 8L, 3L, "344FDB9C-6C6B-45B7-90A0-1899C053A4E3", "Link AssignmentUser8", 0 },
                    { 7L, 5L, "C04974AC-82EC-4B66-811E-7196B903EB78", "Link AssignmentUser7", 0 },
                    { 6L, 5L, "ED7E0669-A242-4863-9C37-DE2518D109FA", "Link AssignmentUser6", 0 },
                    { 4L, 4L, "7ADD7CDB-4FF4-4E8C-A9D0-B28EF55A2EC6", "Link AssignmentUser4", 0 },
                    { 3L, 4L, "BBE9DEA8-BF9D-4635-96C0-9584777D9BFC", "Link AssignmentUser3", 0 },
                    { 2L, 3L, "64AADA74-A7F7-45DB-B25D-726917225AAC", "Link AssignmentUser2", 0 },
                    { 5L, 5L, "CA1D5B58-335E-4902-A768-66248CE43485", "Link AssignmentUser5", 0 }
                });

            migrationBuilder.InsertData(
                table: "QuizUsers",
                columns: new[] { "QuizId", "UserId", "HashCode", "Score" },
                values: new object[,]
                {
                    { 3L, 4L, "67C70C37-EE76-4084-93B2-87E63444D993", 5 },
                    { 2L, 3L, "D1FE52E8-62C9-42F2-88E2-BFF5D66875C2", 6 },
                    { 4L, 4L, "A00556C9-163D-4343-A59D-42F6A53B1BE2", 6 },
                    { 1L, 3L, "0BC9D864-02D7-423D-A64C-B03AA6331461", 5 },
                    { 5L, 5L, "DF671D71-0B59-467D-BCDF-B1BE8F4FF49F", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_HashCode",
                table: "Answers",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuizId",
                table: "Answers",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_HashCode",
                table: "Assignments",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LessonId",
                table: "Assignments",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentUsers_AssignmentId",
                table: "AssignmentUsers",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentUsers_HashCode",
                table: "AssignmentUsers",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_HashCode",
                table: "Categories",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_HashCode",
                table: "Comments",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LessonId",
                table: "Comments",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_HashCode",
                table: "Courses",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionDetails_CourseId",
                table: "DescriptionDetails",
                column: "CourseId",
                unique: true,
                filter: "[CourseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionDetails_HashCode",
                table: "DescriptionDetails",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_HashCode",
                table: "Lesson",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_SectionId",
                table: "Lesson",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_HashCode",
                table: "Notes",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_LessonId",
                table: "Notes",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_HashCode",
                table: "Notifications",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUsers_HashCode",
                table: "NotificationUsers",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUsers_UserId",
                table: "NotificationUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_CourseId",
                table: "OrderDetails",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_HashCode",
                table: "OrderDetails",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_HashCode",
                table: "Orders",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_HashCode",
                table: "Quizs",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizs_LessonId",
                table: "Quizs",
                column: "LessonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizUsers_HashCode",
                table: "QuizUsers",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizUsers_QuizId",
                table: "QuizUsers",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CourseId",
                table: "Sections",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_HashCode",
                table: "Sections",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_HashCode",
                table: "SocialMedias",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_UserId",
                table: "SocialMedias",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_HashCode",
                table: "UserCourses",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_UserId",
                table: "UserCourses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_HashCode",
                table: "Users",
                column: "HashCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AssignmentUsers");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DescriptionDetails");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "NotificationUsers");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "QuizUsers");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Quizs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
