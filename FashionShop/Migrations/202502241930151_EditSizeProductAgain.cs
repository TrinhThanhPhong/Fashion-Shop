namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditSizeProductAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Product", "XS", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "S", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "M", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "L", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "XL", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "XXL", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "XXXL", c => c.Int(nullable: false));
            DropColumn("dbo.Tb_Product", "ProductSize");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_Product", "ProductSize", c => c.String());
            DropColumn("dbo.Tb_Product", "XXXL");
            DropColumn("dbo.Tb_Product", "XXL");
            DropColumn("dbo.Tb_Product", "XL");
            DropColumn("dbo.Tb_Product", "L");
            DropColumn("dbo.Tb_Product", "M");
            DropColumn("dbo.Tb_Product", "S");
            DropColumn("dbo.Tb_Product", "XS");
        }
    }
}
