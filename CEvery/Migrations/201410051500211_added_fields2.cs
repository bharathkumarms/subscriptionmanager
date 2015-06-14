namespace CEvery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_fields2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leads", "Address3", c => c.String());
            AddColumn("dbo.Leads", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leads", "Address3");
            DropColumn("dbo.Leads", "Phone");
        }
    }
}
