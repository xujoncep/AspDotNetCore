using FluentApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FluentApi.Data
{
    public class FluentApiDbContext: DbContext
    {

        public FluentApiDbContext(DbContextOptions<FluentApiDbContext> options) :base(options)
        {
            
        }

        public DbSet<CarCompany> CarCompanies { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubject { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One to one relation: CarCompany(Parent), Carmodel(child)
            modelBuilder.Entity<CarCompany>()
                .HasOne(a => a.CarModel)
                .WithOne(b => b.CarCompany)
                .HasForeignKey<CarModel>( k => k.CarCompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
           
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
