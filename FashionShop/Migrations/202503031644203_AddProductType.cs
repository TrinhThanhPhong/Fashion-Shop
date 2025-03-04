namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Product", "ProductType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Product", "ProductType");
        }
    }
}
