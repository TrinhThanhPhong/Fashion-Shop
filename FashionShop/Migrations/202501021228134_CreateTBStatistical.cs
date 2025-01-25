namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTBStatistical : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statisticals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        View = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tb_Product", "ViewCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Product", "ViewCount");
            DropTable("dbo.Statisticals");
        }
    }
}
