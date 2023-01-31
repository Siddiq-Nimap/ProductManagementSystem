namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingcolm : DbMigration
    {
        public override void Up()
        {
            Sql("alter table Logins add Roles nvarchar(30)");
        }
        
        public override void Down()
        {
        }
    }
}
