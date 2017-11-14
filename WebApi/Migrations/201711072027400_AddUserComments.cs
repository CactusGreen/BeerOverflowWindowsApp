namespace WebApi.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddUserComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRatings", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRatings", "Comment");
        }
    }
}
