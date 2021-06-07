namespace FreelanceSystemDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saveJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "savedJobID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "savedJobID");
        }
    }
}
