namespace WebApi.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bar",
                c => new
                    {
                        BarId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BarId);
            
            CreateTable(
                "dbo.UserRatings",
                c => new
                    {
                        BarId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(nullable: false, maxLength: 128),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BarId, t.Username })
                .ForeignKey("dbo.Bar", t => t.BarId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.Username, cascadeDelete: true)
                .Index(t => t.BarId)
                .Index(t => t.Username);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRatings", "Username", "dbo.User");
            DropForeignKey("dbo.UserRatings", "BarId", "dbo.Bar");
            DropIndex("dbo.UserRatings", new[] { "Username" });
            DropIndex("dbo.UserRatings", new[] { "BarId" });
            DropTable("dbo.User");
            DropTable("dbo.UserRatings");
            DropTable("dbo.Bar");
        }
    }
}
