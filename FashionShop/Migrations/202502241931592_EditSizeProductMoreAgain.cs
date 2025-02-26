namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditSizeProductMoreAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Product", "SizeXS", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "SizeS", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "SizeM", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "SizeL", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "SizeXL", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "SizeXXL", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "SizeXXXL", c => c.Int(nullable: false));
            DropColumn("dbo.Tb_Product", "XS");
            DropColumn("dbo.Tb_Product", "S");
            DropColumn("dbo.Tb_Product", "M");
            DropColumn("dbo.Tb_Product", "L");
            DropColumn("dbo.Tb_Product", "XL");
            DropColumn("dbo.Tb_Product", "XXL");
            DropColumn("dbo.Tb_Product", "XXXL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_Product", "XXXL", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "XXL", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "XL", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "L", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "M", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "S", c => c.Int(nullable: false));
            AddColumn("dbo.Tb_Product", "XS", c => c.Int(nullable: false));
            DropColumn("dbo.Tb_Product", "SizeXXXL");
            DropColumn("dbo.Tb_Product", "SizeXXL");
            DropColumn("dbo.Tb_Product", "SizeXL");
            DropColumn("dbo.Tb_Product", "SizeL");
            DropColumn("dbo.Tb_Product", "SizeM");
            DropColumn("dbo.Tb_Product", "SizeS");
            DropColumn("dbo.Tb_Product", "SizeXS");
        }
    }
}
