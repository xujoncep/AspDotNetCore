using FluentApi.Models;
using FluentApi.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace FluentApi.Data
{
    public class FluentApiDbContext: DbContext
    {

        public FluentApiDbContext(DbContextOptions<FluentApiDbContext> options) :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Address> Addresss { get; set; }
        public DbSet<StudentSubject> StudentSubject { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //auto key generate off
            //modelBuilder.Entity<User>()
            //    .Property(x => x.UserId)
            //    .ValueGeneratedNever();

            //modelBuilder.Entity<Passport>()
            //    .Property(x => x.PassportId)
            //    .ValueGeneratedNever();

            // User to Passport (One-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Passport)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PassportId); // FK in User table

            // User to Address (One-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<User>(u => u.AddressId); // FK in User table

            // One To Many relation: Doctor(parient), Patient(Child)

            modelBuilder.Entity<Doctor>()
                .HasMany(p => p.Patients)
                .WithOne(d => d.Doctor)
                .HasForeignKey(k => k.DoctorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            //Many to Many relation: Student, Subject

            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.HasKey(ckey => new { ckey.StudentId, ckey.SubjectId });

                entity.HasOne(s => s.Student)
                .WithMany(s => s.StudentSubject)
                .HasForeignKey(s => s.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(s => s.Subject)
                .WithMany(s => s.StudentSubject)
                .HasForeignKey(s => s.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            });
        }
    }
}
