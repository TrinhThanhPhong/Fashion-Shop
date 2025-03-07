namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditProductCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tb_ProductCategory", "Alias", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tb_ProductCategory", "Alias", c => c.String(nullable: false, maxLength: 150));
        }
    }
}
