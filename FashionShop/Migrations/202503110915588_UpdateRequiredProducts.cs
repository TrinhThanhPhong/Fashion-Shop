namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRequiredProducts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tb_Product", "ProductCode", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tb_Product", "ProductCode", c => c.String(maxLength: 50));
        }
    }
}
