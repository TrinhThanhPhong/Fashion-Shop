namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statisticals", "ThoiGian", c => c.DateTime(nullable: false));
            AddColumn("dbo.Statisticals", "LuotTruyCap", c => c.Long(nullable: false));
            DropColumn("dbo.Statisticals", "Time");
            DropColumn("dbo.Statisticals", "View");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Statisticals", "View", c => c.Long(nullable: false));
            AddColumn("dbo.Statisticals", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.Statisticals", "LuotTruyCap");
            DropColumn("dbo.Statisticals", "ThoiGian");
        }
    }
}
