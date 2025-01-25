namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1206 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_ProductImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Image = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tb_ProductImage", "ProductId", "dbo.Tb_Product");
            DropIndex("dbo.Tb_ProductImage", new[] { "ProductId" });
            DropTable("dbo.Tb_ProductImage");
        }
    }
}
