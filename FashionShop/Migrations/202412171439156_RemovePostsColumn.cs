namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePostsColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tb_Posts", "CategoryId", "dbo.Tb_Category");
            DropIndex("dbo.Tb_Posts", new[] { "CategoryId" });
            RenameColumn(table: "dbo.Tb_Posts", name: "CategoryId", newName: "Category_Id");
            AlterColumn("dbo.Tb_Posts", "Category_Id", c => c.Int());
            CreateIndex("dbo.Tb_Posts", "Category_Id");
            AddForeignKey("dbo.Tb_Posts", "Category_Id", "dbo.Tb_Category", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tb_Posts", "Category_Id", "dbo.Tb_Category");
            DropIndex("dbo.Tb_Posts", new[] { "Category_Id" });
            AlterColumn("dbo.Tb_Posts", "Category_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Tb_Posts", name: "Category_Id", newName: "CategoryId");
            CreateIndex("dbo.Tb_Posts", "CategoryId");
            AddForeignKey("dbo.Tb_Posts", "CategoryId", "dbo.Tb_Category", "Id", cascadeDelete: true);
        }
    }
}
