namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultConstraint : DbMigration
    {
        public override void Up()
        {
            Sql(" alter table CatagoryLists add constraint DF_CatagoryLists_IsActive default 1 for IsActive");
        }
        
        public override void Down()
        {
        }
    }
}
