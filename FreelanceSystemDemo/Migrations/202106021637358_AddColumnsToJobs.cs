namespace FreelanceSystemDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnsToJobs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "JobType", c => c.String());
            AddColumn("dbo.Jobs", "JobBudget", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "NumberOfProposal", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "PublishDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "PublishDate");
            DropColumn("dbo.Jobs", "NumberOfProposal");
            DropColumn("dbo.Jobs", "JobBudget");
            DropColumn("dbo.Jobs", "JobType");
        }
    }
}
