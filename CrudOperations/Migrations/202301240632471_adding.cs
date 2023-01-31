namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Logins", "Roles");
            AddColumn("dbo.Logins", "Roles", c => c.String());
            Sql("update Logins set Roles = 'Admin' Where id = 1");
            Sql("update Logins set Roles = 'User' Where id = 2");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logins", "Roles");
        }
    }
}
