namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tb_News", "Alias", c => c.String(maxLength: 150));
            AlterColumn("dbo.Tb_News", "Image", c => c.String(maxLength: 250));
            AlterColumn("dbo.Tb_News", "SeoTitle", c => c.String(maxLength: 250));
            AlterColumn("dbo.Tb_News", "SeoDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.Tb_News", "SeoKeywords", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tb_News", "SeoKeywords", c => c.String());
            AlterColumn("dbo.Tb_News", "SeoDescription", c => c.String());
            AlterColumn("dbo.Tb_News", "SeoTitle", c => c.String());
            AlterColumn("dbo.Tb_News", "Image", c => c.String());
            AlterColumn("dbo.Tb_News", "Alias", c => c.String());
        }
    }
}
