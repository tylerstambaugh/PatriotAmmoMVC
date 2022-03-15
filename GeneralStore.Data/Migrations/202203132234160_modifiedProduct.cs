namespace GeneralStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "IsExplosive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Product", "IsFood");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "IsFood", c => c.Boolean(nullable: false));
            DropColumn("dbo.Product", "IsExplosive");
        }
    }
}
