namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CatagoryLists", "IsActive", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
        }
    }
}
