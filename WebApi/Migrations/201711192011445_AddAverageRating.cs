namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAverageRating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRatings", "BarId", "dbo.Bar");
            DropForeignKey("dbo.UserRatings", "Username", "dbo.User");
            DropIndex("dbo.UserRatings", new[] { "BarId" });
            DropIndex("dbo.UserRatings", new[] { "Username" });
            DropPrimaryKey("dbo.Bar");
            DropPrimaryKey("dbo.UserRatings");
            DropPrimaryKey("dbo.User");
            AddColumn("dbo.Bar", "AvgRating", c => c.Single(nullable: false));
            AlterColumn("dbo.Bar", "BarId", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Bar", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserRatings", "BarId", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserRatings", "Username", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.UserRatings", "Comment", c => c.String(maxLength: 400));
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.User", "Password", c => c.String(maxLength: 30));
            AddPrimaryKey("dbo.Bar", "BarId");
            AddPrimaryKey("dbo.UserRatings", new[] { "BarId", "Username" });
            AddPrimaryKey("dbo.User", "Username");
            CreateIndex("dbo.UserRatings", "BarId");
            CreateIndex("dbo.UserRatings", "Username");
            AddForeignKey("dbo.UserRatings", "BarId", "dbo.Bar", "BarId", cascadeDelete: true);
            AddForeignKey("dbo.UserRatings", "Username", "dbo.User", "Username", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRatings", "Username", "dbo.User");
            DropForeignKey("dbo.UserRatings", "BarId", "dbo.Bar");
            DropIndex("dbo.UserRatings", new[] { "Username" });
            DropIndex("dbo.UserRatings", new[] { "BarId" });
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.UserRatings");
            DropPrimaryKey("dbo.Bar");
            AlterColumn("dbo.User", "Password", c => c.String());
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserRatings", "Comment", c => c.String());
            AlterColumn("dbo.UserRatings", "Username", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserRatings", "BarId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Bar", "Title", c => c.String());
            AlterColumn("dbo.Bar", "BarId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Bar", "AvgRating");
            AddPrimaryKey("dbo.User", "Username");
            AddPrimaryKey("dbo.UserRatings", new[] { "BarId", "Username" });
            AddPrimaryKey("dbo.Bar", "BarId");
            CreateIndex("dbo.UserRatings", "Username");
            CreateIndex("dbo.UserRatings", "BarId");
            AddForeignKey("dbo.UserRatings", "Username", "dbo.User", "Username", cascadeDelete: true);
            AddForeignKey("dbo.UserRatings", "BarId", "dbo.Bar", "BarId", cascadeDelete: true);
        }
    }
}
