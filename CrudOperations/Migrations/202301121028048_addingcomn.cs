namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingcomn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catagories", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catagories", "IsActive");
        }
    }
}
