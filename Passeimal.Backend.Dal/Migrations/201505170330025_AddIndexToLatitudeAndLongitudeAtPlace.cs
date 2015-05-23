namespace Passeimal.Backend.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToLatitudeAndLongitudeAtPlace : DbMigration{
        public override void Up(){
            CreateIndex("dbo.Places", new string[] { "Location_Latitude", "Location_Longitude" });
        }
        
        public override void Down(){
            DropIndex("dbo.Places", new string[] { "Location_Latitude", "Location_Longitude" });
        }
    }
}
