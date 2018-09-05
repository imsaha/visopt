using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace visopt.Models
{
    public class VisoContext : DbContext
    {
        public VisoContext() : base("VisoConnection")
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new DoctorConfiguration());
            modelBuilder.Configurations.Add(new AppointmentConfiguration());
        }
    }
}