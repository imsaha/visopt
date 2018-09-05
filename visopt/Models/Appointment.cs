using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace visopt.Models
{


    public class Appointment
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yy HH:mm:ss tt}")]
        public DateTime StartDateTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yy HH:mm:ss tt}")]
        public DateTime EndsDateTime { get; set; }

        [NotMapped]
        public ICollection<Doctor> Doctors { get; set; }
        [NotMapped]
        public ICollection<Client> Clients { get; set; }
    }

    public class AppointmentConfiguration : EntityTypeConfiguration<Appointment>
    {
        public AppointmentConfiguration()
        {
            ToTable("tblAppointments");
            HasKey(k => k.Id);

            HasRequired(d => d.Doctor)
                .WithMany(a=>a.Appointments)
                .HasForeignKey(f => f.DoctorId)
                .WillCascadeOnDelete(false);

            HasRequired(d => d.Client)
                .WithMany(a => a.Appointments)
                .HasForeignKey(f => f.ClientId)
                .WillCascadeOnDelete(false);
        }
    }
}