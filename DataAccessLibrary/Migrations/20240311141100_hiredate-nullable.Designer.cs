﻿// <auto-generated />
using System;
using DataAccessLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLibrary.Migrations
{
    [DbContext(typeof(ESContext))]
    [Migration("20240311141100_hiredate-nullable")]
    partial class hiredatenullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseTopic", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<int>("TopicsId")
                        .HasColumnType("int");

                    b.HasKey("CoursesId", "TopicsId");

                    b.HasIndex("TopicsId");

                    b.ToTable("CourseTopic");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Choice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCorrect")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("ManagerId")
                        .IsUnique()
                        .HasFilter("[ManagerId] IS NOT NULL");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.DepartmentCourse", b =>
                {
                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.ToTable("DepartmentCourses");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExamDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.ExamQuestion", b =>
                {
                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("ExamId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("ExamQuestion");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.ExamTaken", b =>
                {
                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("ExamId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("ExamsTaken");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("Difficulty")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("Grade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.StudentAnswer", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("ChoiceId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "ExamId", "ChoiceId");

                    b.HasIndex("ChoiceId");

                    b.HasIndex("ExamId");

                    b.ToTable("StudentAnswers");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CourseTopic", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLibrary.Model.Topic", null)
                        .WithMany()
                        .HasForeignKey("TopicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Choice", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Question", "Question")
                        .WithMany("Choices")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Department", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Branch", "Branch")
                        .WithMany("Departments")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLibrary.Model.Instructor", "Manager")
                        .WithOne("ManagedDepartment")
                        .HasForeignKey("DataAccessLibrary.Model.Department", "ManagerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Branch");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.DepartmentCourse", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Course", "Course")
                        .WithMany("DepartmentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLibrary.Model.Department", "Department")
                        .WithMany("DepartmentCourses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLibrary.Model.Instructor", "Instructor")
                        .WithMany("DepartmentCourses")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Course");

                    b.Navigation("Department");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Exam", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Course", "Course")
                        .WithMany("Exams")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.ExamQuestion", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Exam", null)
                        .WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccessLibrary.Model.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLibrary.Model.ExamTaken", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLibrary.Model.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Instructor", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Branch", "Branch")
                        .WithMany("Instructors")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DataAccessLibrary.Model.User", "User")
                        .WithOne("Instructor")
                        .HasForeignKey("DataAccessLibrary.Model.Instructor", "UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Branch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Question", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Course", "Course")
                        .WithMany("Questions")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLibrary.Model.Instructor", "Instructor")
                        .WithMany("Questions")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Course");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Student", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DataAccessLibrary.Model.User", "User")
                        .WithOne("Student")
                        .HasForeignKey("DataAccessLibrary.Model.Student", "UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.StudentAnswer", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Choice", "Choice")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("ChoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLibrary.Model.Exam", "Exam")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("ExamId");

                    b.HasOne("DataAccessLibrary.Model.Student", "Student")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("StudentId");

                    b.Navigation("Choice");

                    b.Navigation("Exam");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.User", b =>
                {
                    b.HasOne("DataAccessLibrary.Model.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Branch", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Instructors");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Choice", b =>
                {
                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Course", b =>
                {
                    b.Navigation("DepartmentCourses");

                    b.Navigation("Exams");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Department", b =>
                {
                    b.Navigation("DepartmentCourses");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Exam", b =>
                {
                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Instructor", b =>
                {
                    b.Navigation("DepartmentCourses");

                    b.Navigation("ManagedDepartment");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Question", b =>
                {
                    b.Navigation("Choices");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.Student", b =>
                {
                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("DataAccessLibrary.Model.User", b =>
                {
                    b.Navigation("Instructor");

                    b.Navigation("Student");
                });
#pragma warning restore 612, 618
        }
    }
}
