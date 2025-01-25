namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNewsColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tb_News", "CategoryId", "dbo.Tb_Category");
            DropIndex("dbo.Tb_News", new[] { "CategoryId" });
            RenameColumn(table: "dbo.Tb_News", name: "CategoryId", newName: "Category_Id");
            AlterColumn("dbo.Tb_News", "Category_Id", c => c.Int());
            CreateIndex("dbo.Tb_News", "Category_Id");
            AddForeignKey("dbo.Tb_News", "Category_Id", "dbo.Tb_Category", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tb_News", "Category_Id", "dbo.Tb_Category");
            DropIndex("dbo.Tb_News", new[] { "Category_Id" });
            AlterColumn("dbo.Tb_News", "Category_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Tb_News", name: "Category_Id", newName: "CategoryId");
            CreateIndex("dbo.Tb_News", "CategoryId");
            AddForeignKey("dbo.Tb_News", "CategoryId", "dbo.Tb_Category", "Id", cascadeDelete: true);
        }
    }
}
