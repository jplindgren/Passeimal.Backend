namespace Passeimal.Backend.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePlaceLocationColumns : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Places", name: "Location_Latitude", newName: "Latitude");
            RenameColumn(table: "dbo.Places", name: "Location_Longitude", newName: "Longitude");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Places", name: "Longitude", newName: "Location_Longitude");
            RenameColumn(table: "dbo.Places", name: "Latitude", newName: "Location_Latitude");
        }
    }
}
