using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace visopt.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}".TrimEnd(' '); } }

        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:HH:mm:ss tt}")]
        public DateTime StartWorkingHour { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss tt}")]
        public DateTime EndWorkingHour { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }

    public class DoctorConfiguration : EntityTypeConfiguration<Doctor>
    {
        public DoctorConfiguration()
        {
            ToTable("tblDoctors");
            HasKey(k => k.Id);
            Property(p => p.FirstName).HasMaxLength(50);
            Property(p => p.LastName).HasMaxLength(50);
        }
    }
}