namespace Passeimal.Backend.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlace : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        GooglePlaceId = c.String(),
                        Location_Latitude = c.Double(nullable: false),
                        Location_Longitude = c.Double(nullable: false),
                        Icon = c.String(),
                        Name = c.String(),
                        Vicinity = c.String(),
                        FormattedAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pukes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        Severity = c.Int(nullable: false),
                        Note = c.String(),
                        Place_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.Place_Id)
                .Index(t => t.Place_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pukes", "Place_Id", "dbo.Places");
            DropIndex("dbo.Pukes", new[] { "Place_Id" });
            DropTable("dbo.Pukes");
            DropTable("dbo.Places");
        }
    }
}
