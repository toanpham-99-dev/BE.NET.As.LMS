using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.Infrastructures
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
               new Role()
               {
                   HashCode = RoleHashCode.Admin,
                   Id = 1,
                   Name = "admin",
                   NormalizedName = "administrator",
                   Description = "Quản Lý"
               },
               new Role()
               {
                   Id = 2,
                   HashCode = RoleHashCode.Instructor,
                   Name = "instructor",
                   NormalizedName = "Instructor",
                   Description = "Giảng Viên"
               },
               new Role()
               {
                   Id = 3,
                   HashCode = RoleHashCode.User,
                   Name = "user",
                   NormalizedName = "User",
                   Description = "Người Dùng"
               });
            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = 1,
                   HashCode = UserHashCode.Admin,
                   UserName = "admin",
                   Avatar = "",
                   Email = "admin@gmail.com",
                   EmailConfirmed = true,
                   FullName = "Admin",
                   PhoneNumber = "0123456789",
                   PasswordHash = hasher.HashPassword(null, "123456")
               },
               new User
               {
                   Id = 2,
                   UserName = "instructor1",
                   HashCode = UserHashCode.Instructor1,
                   Avatar = "",
                   Email = "instructor1@gmail.com",
                   EmailConfirmed = true,
                   PhoneNumber = "0123456788",
                   FullName = "Giang Vien",
                   PasswordHash = hasher.HashPassword(null, "123456")
               },
               new User
               {
                   Id = 3,
                   UserName = "instructor2",
                   HashCode = UserHashCode.Instructor2,
                   Avatar = "",
                   Email = "instructor2@gmail.com",
                   EmailConfirmed = true,
                   FullName = "Giảng viên 1",
                   PhoneNumber = "0923456789",
                   PasswordHash = hasher.HashPassword(null, "123456")
               },
               new User
               {
                   Id = 4,
                   UserName = "instructor3",
                   HashCode = UserHashCode.Instructor3,
                   Avatar = "",
                   Email = "instructor3@gmail.com",
                   EmailConfirmed = true,
                   FullName = "Giảng viên 3",
                   PhoneNumber = "0932345678",
                   PasswordHash = hasher.HashPassword(null, "123456")
               },
               new User
               {
                   Id = 5,
                   UserName = "user1",
                   HashCode = UserHashCode.User1,
                   Avatar = "",
                   Email = "user1@gmail.com",
                   EmailConfirmed = true,
                   FullName = "Nguoi Dung 1",
                   PhoneNumber = "0123456787",
                   PasswordHash = hasher.HashPassword(null, "123456")
               },
               new User
               {
                   Id = 6,
                   UserName = "user2",
                   HashCode = UserHashCode.User2,
                   Avatar = "",
                   Email = "user2@gmail.com",
                   EmailConfirmed = true,
                   FullName = "Người dùng 1",
                   PhoneNumber = "0923456777",
                   PasswordHash = hasher.HashPassword(null, "123456")
               },
               new User
               {
                   Id = 7,
                   UserName = "user3",
                   HashCode = UserHashCode.User3,
                   Avatar = "",
                   Email = "user3@gmail.com",
                   EmailConfirmed = true,
                   FullName = "Người dùng 2",
                   PhoneNumber = "0909345097",
                   PasswordHash = hasher.HashPassword(null, "123456")
               });
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    HashCode = Guid.NewGuid().ToString(),
                    UserHashCode = UserHashCode.Admin,
                    RoleHashCode = RoleHashCode.Admin,
                    RoleId = 1,
                    UserId = 1
                },
                new UserRole
                {
                    HashCode = Guid.NewGuid().ToString(),
                    UserHashCode = UserHashCode.Instructor1,
                    RoleHashCode = RoleHashCode.Instructor,
                    RoleId = 2,
                    UserId = 2
                },
                new UserRole
                {
                    HashCode = Guid.NewGuid().ToString(),
                    UserHashCode = UserHashCode.Instructor2,
                    RoleHashCode = RoleHashCode.Instructor,
                    RoleId = 2,
                    UserId = 3
                },
                new UserRole
                {
                    HashCode = Guid.NewGuid().ToString(),
                    UserHashCode = UserHashCode.Instructor3,
                    RoleHashCode = RoleHashCode.Instructor,
                    RoleId = 2,
                    UserId = 4
                },
                new UserRole
                {
                    HashCode = Guid.NewGuid().ToString(),
                    UserHashCode = UserHashCode.User1,
                    RoleHashCode = RoleHashCode.User,
                    RoleId = 3,
                    UserId = 5
                },
                new UserRole
                {
                    HashCode = Guid.NewGuid().ToString(),
                    UserHashCode = UserHashCode.User2,
                    RoleHashCode = RoleHashCode.User,
                    RoleId = 3,
                    UserId = 6
                },
                new UserRole
                {
                    HashCode = Guid.NewGuid().ToString(),
                    UserHashCode = UserHashCode.User3,
                    RoleHashCode = RoleHashCode.User,
                    RoleId = 3,
                    UserId = 7
                });
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    HashCode = ConstHashCode.Category1,
                    CreatedBy = "admin",
                    Title = "Web",
                    Description = "Kiến thức về Web",
                    Alias = "web",
                    ImageURL = "link1",
                    UpdatedBy = "admin",
                    ParentId = null,
                },
                new Category
                {
                    Id = 2,
                    HashCode = ConstHashCode.Category2,
                    CreatedBy = "instructor 1",
                    Title = "Design",
                    Description = "Học thiết kế cơ bản",
                    Alias = "design",
                    ImageURL = "link1",
                    UpdatedBy = "instructor 1",
                    ParentId = 1,
                },
                new Category
                {
                    Id = 3,
                    HashCode = ConstHashCode.Category3,
                    CreatedBy = "instructor 1",
                    Title = "Business",
                    Description = "Quản trị nhân sự",
                    Alias = "business",
                    ImageURL = "link1",
                    UpdatedBy = "admin",
                    ParentId = 2,
                },
                new Category
                {
                    Id = 4,
                    HashCode = ConstHashCode.Category4,
                    CreatedBy = "admin",
                    Title = "Asp.net mvc",
                    Description = "Học asp.net mvc cơ bản",
                    Alias = "basic",
                    ImageURL = "link1",
                    UpdatedBy = "instructor 1",
                    ParentId = 2,
                },
                new Category
                {
                    Id = 5,
                    HashCode = ConstHashCode.Category5,
                    CreatedBy = "instructor 2",
                    Title = "Thiết kế giao diện",
                    Description = "Bài giảng thiết kế giao diện HTML, CSS",
                    Alias = "frontend",
                    ImageURL = "link1",
                    UpdatedBy = "instructor 2",
                    ParentId = 3,
                });
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    HashCode = ConstHashCode.Course1,
                    CategoryId = 1,
                    Title = "Kiến Thức Nhập Môn",
                    Content = $"<ul><li> Giúp bạn nắm được kiến thức cơ bản về HTML và CSS </li>\"<li> Hiểu được thành phần của một trang web</li>\"<li> Xây dựng một trang web tĩnh cơ bản.</li></ul>",
                    CreatedBy = "FullStack",
                    Description = "Để có cái nhìn tổng quan về ngành IT - Lập trình web các bạn nên xem các videos tại khóa này trước nhé.",
                    ImageURL = "1.png",
                    Level = EnumCourseLevel.Basic,
                    Status = 1,
                    Price = 0,
                    ViewCount = 43321,
                    Summary = "Tóm tắt về khóa học",
                    Syllabus = "Giáo trình 1",
                    Rating = 2,
                    InstructorHashCodeCreated = UserHashCode.Instructor1
                },
                new Course
                {
                    Id = 2,
                    HashCode = ConstHashCode.Course2,
                    CategoryId = 1,
                    Title = "HTML, CSS từ Zero đến Hero",
                    Content = $"<ul><li> Giúp bạn nắm được kiến thức cơ bản về HTML và CSS </li><li> Hiểu được thành phần của một trang web</li><li> Xây dựng một trang web tĩnh cơ bản.</li></ul>",
                    CreatedBy = "FullStack",
                    Description = "Để có cái nhìn tổng quan về ngành IT - Lập trình web các bạn nên xem các videos tại khóa này trước nhé.",
                    ImageURL = "1.png",
                    Level = EnumCourseLevel.Basic,
                    Status = 1,
                    Price = 0,
                    ViewCount = 32123,
                    Summary = "Tóm tắt về khóa học",
                    Syllabus = "Giáo trình 1",
                    Rating = 5,
                    InstructorHashCodeCreated = UserHashCode.Instructor2
                },
                new Course
                {
                    Id = 3,
                    HashCode = ConstHashCode.Course3,
                    CategoryId = 2,
                    Title = "HTML, CSS từ cơ bản đến nâng cao",
                    Content = $"<ul><li> Giúp bạn nắm được kiến thức cơ bản về HTML và CSS </li><li> Hiểu được thành phần của một trang web</li><li> Xây dựng một trang web tĩnh cơ bản.</li></ul>",
                    CreatedBy = "Frontend",
                    Description = "Để có cái nhìn tổng quan về ngành IT - Lập trình web các bạn nên xem các videos tại khóa này trước nhé.",
                    ImageURL = "3.png",
                    Level = EnumCourseLevel.Advanced,
                    Status = 1,
                    Price = 90000,
                    ViewCount = 1231,
                    Summary = "Tóm tắt về khóa học",
                    Syllabus = "Giáo trình 2",
                    Rating = 3,
                    InstructorHashCodeCreated = UserHashCode.Instructor3
                },
                new Course
                {
                    Id = 4,
                    HashCode = ConstHashCode.Course4,
                    CategoryId = 3,
                    Title = "Lập trình nâng cao JavaScript",
                    Content = $"<ul><li> Giúp bạn nắm được kiến thức cơ bản về HTML và CSS </li><li> Hiểu được thành phần của một trang web</li><li> Xây dựng một trang web tĩnh cơ bản.</li></ul>",
                    CreatedBy = "Backend",
                    Description = "Để có cái nhìn tổng quan về ngành IT - Lập trình web các bạn nên xem các videos tại khóa này trước nhé.",
                    ImageURL = "4.png",
                    Level = EnumCourseLevel.Advanced,
                    Status = 1,
                    Price = 45000,
                    ViewCount = 1231,
                    Summary = "Tóm tắt về khóa học",
                    Syllabus = "Giáo trình 3",
                    Rating = 4,
                    InstructorHashCodeCreated = UserHashCode.Instructor2
                },
                new Course
                {
                    Id = 5,
                    HashCode = ConstHashCode.Course5,
                    CategoryId = 3,
                    Title = "Lập trình JavaScrip Cơ Bản",
                    Content = "Kiến thức nâng cao",
                    CreatedBy = "Backend",
                    Description = "Để có cái nhìn tổng quan về ngành IT - Lập trình web các bạn nên xem các videos tại khóa này trước nhé.",
                    ImageURL = "5.png",
                    Level = 0,
                    Status = 1,
                    Price = 45000,
                    ViewCount = 32321,
                    Summary = "Tóm tắt về khóa học",
                    Syllabus = "Giáo trình 3",
                    Rating = 5,
                    InstructorHashCodeCreated = UserHashCode.Instructor1
                });
            modelBuilder.Entity<Section>().HasData(
               new Section
               {
                   Id = 1,
                   HashCode = ConstHashCode.Section1,
                   CourseId = 1,
                   Description = "Môi Trường, Con Người IT",
                   Priority = 1
               },
               new Section
               {
                   Id = 2,
                   HashCode = ConstHashCode.Section2,
                   CourseId = 1,
                   Description = "Khái Niệm Kỹ Thuật Cần Biết",
                   Priority = 2
               },
               new Section
               {
                   Id = 3,
                   HashCode = ConstHashCode.Section3,
                   CourseId = 1,
                   Description = "Phương Pháp Định Hướng",
                   Priority = 3
               },
               new Section
               {
                   Id = 4,
                   HashCode = ConstHashCode.Section4,
                   CourseId = 2,
                   Description = "Bắt Đầu",
                   Priority = 1
               },
               new Section
               {
                   Id = 5,
                   HashCode = ConstHashCode.Section5,
                   CourseId = 2,
                   Description = "Làm Quen Với HTML",
                   Priority = 2
               },
               new Section
               {
                   Id = 6,
                   CourseId = 2,
                   Description = "Làm Quen Với CSS",
                   Priority = 3
               });
            modelBuilder.Entity<Lesson>().HasData(
                new Lesson
                {
                    Id = 1,
                    SectionId = 1,
                    Duration = new TimeSpan(0,0,12,13),
                    HashCode = ConstHashCode.Lesson1,
                    Name = "Học IT cần tố chất gì? Góc nhìn khác từ chuyên gia định hướng giáo dục",
                    Description = "Tổng quan về mô hình",
                    LinkVideo = $"https://youtu.be/CyZ_O7v62h4",
                },
                new Lesson
                {
                    Id = 2,
                    SectionId = 1,
                    HashCode = ConstHashCode.Lesson2,
                    Duration = new TimeSpan(0, 0, 6, 13),
                    Name = "Sinh viên IT đi thực tập tại doanh nghiệp cần biết những gì?",
                    Description = "Lesson 2",
                    LinkVideo = $"https://youtu.be/YH-E4Y3EaT4",
                },
                new Lesson
                {
                    Id = 3,
                    SectionId = 2,
                    Duration = new TimeSpan(0, 0, 4, 13),
                    HashCode = ConstHashCode.Lesson3,
                    Name = "Mô hình Client - Server là gì?",
                    Description = "Mô hình Client - Server là gì?",
                    LinkVideo = $"https://youtu.be/zoELAirXMJY",
                },
                new Lesson
                {
                    Id = 4,
                    SectionId = 2,
                    HashCode = ConstHashCode.Lesson4,
                    Duration = new TimeSpan(0, 0, 7, 12),
                    Name = "Domain là gì? Tên miền là gì?",
                    Description = "Domain là gì? Tên miền là gì?",
                    LinkVideo = $"https://youtu.be/M62l1xA5Eu8",
                },
                new Lesson
                {
                    Id = 5,
                    SectionId = 3,
                    HashCode = ConstHashCode.Lesson5,
                    Duration = new TimeSpan(0, 0, 4, 2),
                    Name = "Phương pháp học lập trình của Admin F8?",
                    Description = "Lesson 5",
                    LinkVideo = $"https://youtu.be/DpvYHLUiZpc",
                },
                new Lesson
                {
                    Id = 6,
                    SectionId = 3,
                    HashCode = ConstHashCode.Lesson6,
                    Name = "Làm sao để có thu nhập cao và đi xa hơn trong ngành IT?",
                    Description = "Làm sao để có thu nhập cao và đi xa hơn trong ngành IT?",
                    LinkVideo = "https://youtu.be/oF7isq9IjZM",
                },
                new Lesson
                {
                    Id = 7,
                    SectionId = 4,
                    HashCode = ConstHashCode.Lesson7,
                    Name = "Làm được gì sau khóa học?",
                    Description = "Làm được gì sau khóa học?",
                    LinkVideo = $"https://youtu.be/R6plN3FvzFY",
                },
                new Lesson
                {
                    Id = 8,
                    SectionId = 5,
                    HashCode = ConstHashCode.Lesson8,
                    Name = "HTML, CSS là gì?",
                    Description = "HTML, CSS là gì?",
                    LinkVideo = $"https://youtu.be/zwsPND378OQ",
                },
                new Lesson
                {
                    Id = 9,
                    SectionId = 5,
                    HashCode = ConstHashCode.Lesson9,
                    Name = "Làm quen với Dev tools",
                    Description = "Làm quen với Dev tools",
                    LinkVideo = "https://youtu.be/7BJiPyN4zZ0",
                },
                new Lesson
                {
                    Id = 10,
                    SectionId = 6,
                    HashCode = ConstHashCode.Lesson10,
                    Name = "Cấu trúc file HTML",
                    Description = "Cấu trúc file HTML",
                    LinkVideo = $"https://youtu.be/LYnrFSGLCl8",
                });          
            modelBuilder.Entity<Notification>().HasData(
                new Notification
                {
                    Id = 1,
                    HashCode = ConstHashCode.Notification1,
                    Content = "Lập trình cơ bản",
                    Link = "Linkvd1",
                    IsRead = false,
                },
                new Notification
                {
                    Id = 2,
                    HashCode = ConstHashCode.Notification2,
                    Content = "Lập trình Java",
                    Link = "Linkvd2",
                    IsRead = false,
                },
                new Notification
                {
                    Id = 3,
                    HashCode = ConstHashCode.Notification3,
                    Content = "Lập trình Python",
                    Link = "Linkvd3",
                    IsRead = false,
                },
                new Notification
                {
                    Id = 4,
                    HashCode = ConstHashCode.Notification4,
                    Content = "Lập trình C#",
                    Link = "Linkvd4",
                    IsRead = false,
                },
                new Notification
                {
                    Id = 5,
                    HashCode = ConstHashCode.Notification5,
                    Content = "Lập trình nâng cao",
                    Link = "Linkvd5",
                    IsRead = false,
                });
            modelBuilder.Entity<NotificationUser>().HasData(
                new NotificationUser
                {
                    HashCode = ConstHashCode.NotificationUser1,
                    NotificationId = 1,
                    UserId = 1
                },
                new NotificationUser
                {
                    HashCode = ConstHashCode.NotificationUser2,
                    NotificationId = 1,
                    UserId = 3
                },
                new NotificationUser
                {
                    HashCode = ConstHashCode.NotificationUser3,
                    NotificationId = 2,
                    UserId = 4
                },
                new NotificationUser
                {
                    HashCode = ConstHashCode.NotificationUser4,
                    NotificationId = 2,
                    UserId = 5
                },
                new NotificationUser
                {
                    HashCode = ConstHashCode.NotificationUser5,
                    NotificationId = 3,
                    UserId = 3
                });
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    HashCode = ConstHashCode.Order1,
                    Quantity = 1,
                    TotalPrice = 100000,
                    UserId = 3
                },
                new Order
                {
                    Id = 2,
                    HashCode = ConstHashCode.Order2,
                    Quantity = 2,
                    TotalPrice = 150000,
                    UserId = 4
                },
                new Order
                {
                    Id = 3,
                    HashCode = ConstHashCode.Order3,
                    Quantity = 2,
                    TotalPrice = 230000,
                    UserId = 5
                },
                new Order
                {
                    Id = 4,
                    HashCode = ConstHashCode.Order4,
                    Quantity = 2,
                    TotalPrice = 250000,
                    UserId = 4
                },
                new Order
                {
                    Id = 5,
                    HashCode = ConstHashCode.Order5,
                    Quantity = 0,
                    TotalPrice = 0,
                    UserId = 5
                });
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    Id = 1,
                    HashCode = ConstHashCode.OrderDetail1,
                    Price = 100000,
                    CourseName = "Tiếng Anh chuyên ngành",
                    CourseId = 1,
                    OrderId = 1
                },
                new OrderDetail
                {
                    Id = 2,
                    HashCode = ConstHashCode.OrderDetail2,
                    Price = 105000,
                    CourseName = "Asp.net mvc 3",
                    CourseId = 2,
                    OrderId = 2
                },
                new OrderDetail
                {
                    Id = 3,
                    HashCode = ConstHashCode.OrderDetail3,
                    Price = 110000,
                    CourseName = "Học Python cơ bản",
                    CourseId = 3,
                    OrderId = 3
                },
                new OrderDetail
                {
                    Id = 4,
                    HashCode = ConstHashCode.OrderDetail4,
                    Price = 115000,
                    CourseName = "C# cơ bản",
                    CourseId = 4,
                    OrderId = 4
                },
                new OrderDetail
                {
                    Id = 5,
                    HashCode = ConstHashCode.OrderDetail5,
                    Price = 120000,
                    CourseName = "Tiếng Anh ngữ pháp",
                    CourseId = 5,
                    OrderId = 3
                }
                );
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz
                {
                    Id = 1,
                    HashCode = ConstHashCode.Quiz1,
                    QuizContent = "Fresher Dot Net 10",
                    LessonId = 1
                },
                new Quiz
                {
                    Id = 2,
                    HashCode = ConstHashCode.Quiz2,
                    QuizContent = "Fresher Dot Net 10",
                    LessonId = 2
                },
                new Quiz
                {
                    Id = 3,
                    HashCode = ConstHashCode.Quiz3,
                    QuizContent = "Fresher Dot Net 10",
                    LessonId = 3
                },
                new Quiz
                {
                    Id = 4,
                    HashCode = ConstHashCode.Quiz4,
                    QuizContent = "Fresher Dot Net 10",
                    LessonId = 4
                },
                new Quiz
                {
                    Id = 5,
                    HashCode = ConstHashCode.Quiz5,
                    QuizContent = "Fresher Dot Net 10",
                    LessonId = 5
                });
            modelBuilder.Entity<QuizUser>().HasData(
                new QuizUser
                {
                    UserId = 3,
                    QuizId = 1,
                    HashCode = ConstHashCode.QuizUser1,
                    Score = 5
                },
                new QuizUser
                {
                    UserId = 3,
                    QuizId = 2,
                    HashCode = ConstHashCode.QuizUser2,
                    Score = 6
                },
                new QuizUser
                {
                    UserId = 4,
                    QuizId = 3,
                    HashCode = ConstHashCode.QuizUser3,
                    Score = 5
                },
                new QuizUser
                {
                    UserId = 4,
                    QuizId = 4,
                    HashCode = ConstHashCode.QuizUser4,
                    Score = 6
                },
                new QuizUser
                {
                    UserId = 5,
                    QuizId = 5,
                    HashCode = ConstHashCode.QuizUser5,
                    Score = 8
                });
            modelBuilder.Entity<Answer>().HasData(
                new Answer
                {
                    Id = 1,
                    HashCode = ConstHashCode.Answer1,
                    AnswerContent = "Can we do this?",
                    IsCorrect = true,
                    QuizId = 1
                },
                new Answer
                {
                    Id = 2,
                    HashCode = ConstHashCode.Answer2,
                    AnswerContent = "How to search unicode?",
                    IsCorrect = true,
                    QuizId = 1
                },
                new Answer
                {
                    Id = 3,
                    HashCode = ConstHashCode.Answer3,
                    AnswerContent = "Filter sort?",
                    IsCorrect = true,
                    QuizId = 2
                },
                new Answer
                {
                    Id = 4,
                    HashCode = ConstHashCode.Answer4,
                    AnswerContent = "Can we do this?",
                    IsCorrect = true,
                    QuizId = 2
                },
                new Answer
                {
                    Id = 5,
                    HashCode = ConstHashCode.Answer5,
                    AnswerContent = "Can we do this?",
                    IsCorrect = true,
                    QuizId = 3
                },
                new Answer
                {
                    Id = 6,
                    HashCode = ConstHashCode.Answer6,
                    AnswerContent = "Can we do this?",
                    IsCorrect = true,
                    QuizId = 3
                },
                new Answer
                {
                    Id = 7,
                    HashCode = ConstHashCode.Answer7,
                    AnswerContent = "Can we do this?",
                    IsCorrect = true,
                    QuizId = 4
                },
                new Answer
                {
                    Id = 8,
                    HashCode = ConstHashCode.Answer8,
                    AnswerContent = "Can we do this?",
                    IsCorrect = true,
                    QuizId = 4
                },
                new Answer
                {
                    Id = 9,
                    HashCode = ConstHashCode.Answer9,
                    AnswerContent = "Can we do this?",
                    IsCorrect = true,
                    QuizId = 5
                },
                new Answer
                {
                    Id = 10,
                    HashCode = ConstHashCode.Answer10,
                    AnswerContent = "Can we do this?",
                    IsCorrect = true,
                    QuizId = 5
                });
            modelBuilder.Entity<SocialMedia>().HasData(
                new SocialMedia
                {
                    Id = 1,
                    HashCode = ConstHashCode.SocialMedia1,
                    Link = "Link media 1",
                    Name = "Media 1",
                    UserId = 3
                },
                new SocialMedia
                {
                    Id = 2,
                    HashCode = ConstHashCode.SocialMedia2,
                    Link = "Link media 2",
                    Name = "Media 2",
                    UserId = 3
                },
                new SocialMedia
                {
                    Id = 3,
                    HashCode = ConstHashCode.SocialMedia3,
                    Link = "Link media 3",
                    Name = "Media 3",
                    UserId = 4
                },
                new SocialMedia
                {
                    Id = 4,
                    HashCode = ConstHashCode.SocialMedia4,
                    Link = "Link media 4",
                    Name = "Media 4",
                    UserId = 4
                },
                new SocialMedia
                {
                    Id = 5,
                    HashCode = ConstHashCode.SocialMedia5,
                    Link = "Link media 5",
                    Name = "Media 5",
                    UserId = 5
                },
                new SocialMedia
                {
                    Id = 6,
                    HashCode = ConstHashCode.SocialMedia6,
                    Link = "Link media 6",
                    Name = "Media 6",
                    UserId = 5
                });
            modelBuilder.Entity<UserCourse>().HasData(
                new UserCourse
                {
                    UserId = 1,
                    CourseId = 1,
                    HashCode = ConstHashCode.UserCourse1,
                    UserHashCode = UserHashCode.Admin,
                    CourseHashCode = ConstHashCode.Course1,
                    Completed = 1
                },
                new UserCourse
                {
                    UserId = 1,
                    CourseId = 2,
                    HashCode = ConstHashCode.UserCourse2,
                    UserHashCode = UserHashCode.Admin,
                    CourseHashCode = ConstHashCode.Course1,
                    Completed = 2
                },
                new UserCourse
                {
                    UserId = 1,
                    CourseId = 3,
                    HashCode = ConstHashCode.UserCourse4,
                    UserHashCode = UserHashCode.Admin,
                    CourseHashCode = ConstHashCode.Course2,
                    Completed = 3
                },
                new UserCourse
                {
                    UserId = 1,
                    CourseId = 4,
                    HashCode = ConstHashCode.UserCourse5,
                    UserHashCode = UserHashCode.Admin,
                    CourseHashCode = ConstHashCode.Course3,
                    Completed = 4
                },
                new UserCourse
                {
                    UserId = 1,
                    CourseId = 5,
                    HashCode = ConstHashCode.UserCourse6,
                    UserHashCode = UserHashCode.Admin,
                    CourseHashCode = ConstHashCode.Course4,
                    Completed = 1
                });
            modelBuilder.Entity<Assignment>().HasData(
                new Assignment
                {
                    Id = 1,
                    HashCode = ConstHashCode.Assignment1,
                    AssignmentName = "Assignment name 1",
                    LessonId = 1
                },
                new Assignment
                {
                    Id = 2,
                    HashCode = ConstHashCode.Assignment2,
                    AssignmentName = "Assignment name 2",
                    LessonId = 1
                },
                new Assignment
                {
                    Id = 3,
                    HashCode = ConstHashCode.Assignment3,
                    AssignmentName = "Assignment name 3",
                    LessonId = 2
                },
                new Assignment
                {
                    Id = 4,
                    HashCode = ConstHashCode.Assignment4,
                    AssignmentName = "Assignment name 4",
                    LessonId = 2
                },
                new Assignment
                {
                    Id = 5,
                    HashCode = ConstHashCode.Assignment5,
                    AssignmentName = "Assignment name 5",
                    LessonId = 3
                },
                new Assignment
                {
                    Id = 6,
                    HashCode = ConstHashCode.Assignment6,
                    AssignmentName = "Assignment name 6",
                    LessonId = 3
                },
                new Assignment
                {
                    Id = 7,
                    HashCode = ConstHashCode.Assignment7,
                    AssignmentName = "Assignment name 7",
                    LessonId = 4
                },
                new Assignment
                {
                    Id = 8,
                    HashCode = ConstHashCode.Assignment8,
                    AssignmentName = "Assignment name 8",
                    LessonId = 5
                });
            modelBuilder.Entity<AssignmentUser>().HasData(
                new AssignmentUser
                {
                    UserId = 3,
                    AssignmentId = 1,
                    HashCode = ConstHashCode.AssignmentUser1,
                    Link = "Link AssignmentUser1",
                },
                new AssignmentUser
                {
                    UserId = 3,
                    AssignmentId = 2,
                    HashCode = ConstHashCode.AssignmentUser2,
                    Link = "Link AssignmentUser2"
                },
                new AssignmentUser
                {
                    UserId = 4,
                    AssignmentId = 3,
                    HashCode = ConstHashCode.AssignmentUser3,
                    Link = "Link AssignmentUser3"
                },
                new AssignmentUser
                {
                    UserId = 4,
                    AssignmentId = 4,
                    HashCode = ConstHashCode.AssignmentUser4,
                    Link = "Link AssignmentUser4"
                },
                new AssignmentUser
                {
                    UserId = 5,
                    AssignmentId = 5,
                    HashCode = ConstHashCode.AssignmentUser5,
                    Link = "Link AssignmentUser5"
                },
                new AssignmentUser
                {
                    UserId = 5,
                    AssignmentId = 6,
                    HashCode = ConstHashCode.AssignmentUser6,
                    Link = "Link AssignmentUser6"
                },
                new AssignmentUser
                {
                    UserId = 5,
                    AssignmentId = 7,
                    HashCode = ConstHashCode.AssignmentUser7,
                    Link = "Link AssignmentUser7"
                },
                new AssignmentUser
                {
                    UserId = 3,
                    AssignmentId = 8,
                    HashCode = ConstHashCode.AssignmentUser8,
                    Link = "Link AssignmentUser8"
                });
            modelBuilder.Entity<Comment>().HasData(
               new Comment
               {
                   Id = 1,
                   UserId = 3,
                   HashCode = ConstHashCode.Comment1,
                   Title = "Bài học hữu ích và hay",
                   Content = "Đánh giá khóa học",
                   LikeCount = 30,
                   ParentId = null,
                   LessonId = 1
               },
               new Comment
               {
                   Id = 2,
                   UserId = 3,
                   HashCode = ConstHashCode.Comment2,
                   Title = "Cần cải thiện nội dung bài học chi tiết hơn",
                   Content = "Đánh giá nội dung",
                   LikeCount = 5,
                   ParentId = 2,
                   LessonId = 2
               },
               new Comment
               {
                   Id = 3,
                   UserId = 3,
                   HashCode = ConstHashCode.Comment3,
                   Title = "Cần cải thiện nội dung bài học chi tiết hơn",
                   Content = "Đánh giá nội dung",
                   LikeCount = 25,
                   ParentId = 3,
                   LessonId = 3
               },
               new Comment
               {
                   Id = 4,
                   UserId = 4,
                   HashCode = ConstHashCode.Comment4,
                   Title = "Cần cải thiện nội dung bài học chi tiết hơn",
                   Content = "Đánh giá nội dung",
                   LikeCount = 20,
                   ParentId = 4,
                   LessonId = 4
               },
               new Comment
               {
                   Id = 5,
                   UserId = 4,
                   HashCode = ConstHashCode.Comment5,
                   Title = "Cần cải thiện nội dung bài học chi tiết hơn",
                   Content = "Đánh giá nội dung",
                   LikeCount = 15,
                   ParentId = 5,
                   LessonId = 5
               },
               new Comment
               {
                   Id = 6,
                   UserId = 4,
                   HashCode = ConstHashCode.Comment6,
                   Title = "Cần cải thiện nội dung bài học chi tiết hơn",
                   Content = "Đánh giá nội dung",
                   LikeCount = 35,
                   ParentId = 6,
                   LessonId = 6
               },
               new Comment
               {
                   Id = 7,
                   UserId = 5,
                   HashCode = ConstHashCode.Comment7,
                   Title = "Cần cải thiện nội dung bài học chi tiết hơn",
                   Content = "Đánh giá nội dung",
                   LikeCount = 31,
                   ParentId = 7,
                   LessonId = 7
               },
               new Comment
               {
                   Id = 8,
                   UserId = 5,
                   HashCode = ConstHashCode.Comment8,
                   Title = "Cần cải thiện nội dung bài học chi tiết hơn",
                   Content = "Đánh giá nội dung",
                   LikeCount = 17,
                   ParentId = 8,
                   LessonId = 8
               });
            modelBuilder.Entity<DescriptionDetail>().HasData(
                new DescriptionDetail
                {
                    Id = 1,
                    CourseId = 1,
                    HashCode = ConstHashCode.DescriptionDetail1
                },
                new DescriptionDetail
                {
                    Id = 2,
                    CourseId = 2,
                    HashCode = ConstHashCode.DescriptionDetail2
                },
                new DescriptionDetail
                {
                    Id = 3,
                    CourseId = 3,
                    HashCode = ConstHashCode.DescriptionDetail3
                },
                new DescriptionDetail
                {
                    Id = 4,
                    CourseId = 4,
                    HashCode = ConstHashCode.DescriptionDetail4
                },
                new DescriptionDetail
                {
                    Id = 5,
                    CourseId = 5,
                    HashCode = ConstHashCode.DescriptionDetail5
                });
        }
    }
}