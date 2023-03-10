namespace CrudOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductGenericName = c.String(),
                        ProductDescription = c.String(),
                        ProductTitle = c.String(),
                        ProductWeight = c.Int(nullable: false),
                        ProductPrice = c.Int(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Product1");
        }
    }
}
