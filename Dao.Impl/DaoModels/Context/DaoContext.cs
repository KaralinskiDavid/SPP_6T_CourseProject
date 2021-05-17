using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dao.Impl.DaoModels.Context
{
    public class DaoContext : DbContext
    {
        public DaoContext(DbContextOptions<DaoContext> opts) : base(opts) { }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<DaySchedule> DaySchedules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne(e => e.Group).WithMany(g => g.Students).HasForeignKey(e => e.GroupId);
                entity.HasOne(e => e.Speciality).WithOne(s => s.HeadStudent).HasForeignKey<Student>(s=>s.SpecialityId);
            });
            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.HasOne(e => e.HeadStudent).WithOne(s=>s.Speciality).HasForeignKey<Speciality>(s=>s.HeadStudentId);
            });
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasMany(e => e.Students).WithOne(s => s.Group).HasForeignKey(s => s.GroupId);
                entity.HasOne(e => e.Schedule).WithOne(s => s.Group).HasForeignKey<Group>(g => g.ScheduleId);
            });
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasMany(e => e.DaySchedules).WithOne(ds => ds.Schedule).HasForeignKey(ds => ds.ScheduleId);
                entity.HasOne(e => e.Group).WithOne(g => g.Schedule).HasForeignKey<Schedule>(s=>s.GroupId);
            });
            modelBuilder.Entity<DaySchedule>(entity =>
            {
                entity.HasMany(e => e.Lessons).WithOne(l=>l.DaySchedule).HasForeignKey(l => l.DayScheduleId);
            });
            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasOne(e => e.LessonType).WithMany().HasForeignKey(e => e.LessonTypeId);
            });
        }

    }
}
