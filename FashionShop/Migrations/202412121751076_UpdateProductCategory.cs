namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_ProductCategory", "Alias", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Tb_ProductCategory", "Icon", c => c.String(maxLength: 250));
            AlterColumn("dbo.Tb_ProductCategory", "SeoTitle", c => c.String(maxLength: 250));
            AlterColumn("dbo.Tb_ProductCategory", "SeoDescription", c => c.String(maxLength: 250));
            AlterColumn("dbo.Tb_ProductCategory", "SeoKeywords", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tb_ProductCategory", "SeoKeywords", c => c.String());
            AlterColumn("dbo.Tb_ProductCategory", "SeoDescription", c => c.String());
            AlterColumn("dbo.Tb_ProductCategory", "SeoTitle", c => c.String());
            AlterColumn("dbo.Tb_ProductCategory", "Icon", c => c.String());
            DropColumn("dbo.Tb_ProductCategory", "Alias");
        }
    }
}
