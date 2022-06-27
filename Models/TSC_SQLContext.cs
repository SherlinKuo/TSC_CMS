using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TSC_CMS.Models
{
    public partial class TSC_SQLContext : DbContext
    {
        public TSC_SQLContext()
        {
        }

        public TSC_SQLContext(DbContextOptions<TSC_SQLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Actions { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<TimePeriod> TimePeriods { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=tcp:172.20.10.6;Initial Catalog=TSC_SQL;user id=sa;password=S@herlin;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>(entity =>
            {
                entity.ToTable("Action");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasComment("紀錄 Id");

                entity.Property(e => e.Action1)
                    .HasMaxLength(10)
                    .HasColumnName("action")
                    .HasComment("紀錄");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lesson");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasComment("行為(上課、繳費、請假)");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date")
                    .HasComment("日期");

                entity.Property(e => e.Lesson1)
                    .HasColumnName("lesson")
                    .HasComment("堂數");

                entity.Property(e => e.StudentId).HasColumnName("studentId");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id).HasComment("學生 Id");

                entity.Property(e => e.Money).HasColumnName("money");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .HasColumnName("name")
                    .HasComment("學生名");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(10)
                    .HasColumnName("name_en")
                    .HasComment("學生中文");

                entity.Property(e => e.Note)
                    .HasMaxLength(30)
                    .HasColumnName("note")
                    .HasComment("備註");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .HasColumnName("phone")
                    .HasComment("聯絡電話");

                entity.Property(e => e.TimePeriod)
                    .HasColumnName("time_period")
                    .HasComment("上課時段");
            });

            modelBuilder.Entity<TimePeriod>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TimePeriod");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TimePeriod1)
                    .HasMaxLength(10)
                    .HasColumnName("time_period");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
