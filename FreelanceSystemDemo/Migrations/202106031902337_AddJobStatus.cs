namespace FreelanceSystemDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "PostStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "PostStatus");
        }
    }
}
