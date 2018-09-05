using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Web.Mvc;

namespace visopt.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}".TrimEnd(' '); } }

        public string MobileNo { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }

    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            ToTable("tblClients");
            HasKey(k => k.Id);
            HasIndex(i => i.MobileNo).IsUnique();


            Property(p => p.FirstName).HasMaxLength(50);
            Property(p => p.LastName).HasMaxLength(50);
            Property(p => p.MobileNo).HasMaxLength(50);
        }
    }
}