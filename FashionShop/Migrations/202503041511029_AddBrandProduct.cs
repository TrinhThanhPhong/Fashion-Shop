namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrandProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Product", "ProductBrand", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Product", "ProductBrand");
        }
    }
}
