namespace GeneralStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false),
                        ItemCount = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Transaction", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Transaction", new[] { "ProductId" });
            DropIndex("dbo.Transaction", new[] { "CustomerId" });
            DropTable("dbo.Transaction");
        }
    }
}
