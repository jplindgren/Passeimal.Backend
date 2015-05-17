namespace Passeimal.Backend.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelevanceToPlace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Places", "Relevance", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Places", "Relevance");
        }
    }
}
