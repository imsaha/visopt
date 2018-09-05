namespace visopt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblAppointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndsDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblClients", t => t.ClientId)
                .ForeignKey("dbo.tblDoctors", t => t.DoctorId)
                .Index(t => t.DoctorId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.tblClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        MobileNo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.MobileNo, unique: true);
            
            CreateTable(
                "dbo.tblDoctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        StartWorkingHour = c.DateTime(nullable: false),
                        EndWorkingHour = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblAppointments", "DoctorId", "dbo.tblDoctors");
            DropForeignKey("dbo.tblAppointments", "ClientId", "dbo.tblClients");
            DropIndex("dbo.tblClients", new[] { "MobileNo" });
            DropIndex("dbo.tblAppointments", new[] { "ClientId" });
            DropIndex("dbo.tblAppointments", new[] { "DoctorId" });
            DropTable("dbo.tblDoctors");
            DropTable("dbo.tblClients");
            DropTable("dbo.tblAppointments");
        }
    }
}
