namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSubcribe : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Tb_OrderDetail");
            AddColumn("dbo.Tb_OrderDetail", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Tb_Subcribe", "Email", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Tb_OrderDetail", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Tb_OrderDetail");
            AlterColumn("dbo.Tb_Subcribe", "Email", c => c.String());
            DropColumn("dbo.Tb_OrderDetail", "Id");
            AddPrimaryKey("dbo.Tb_OrderDetail", new[] { "OrderId", "ProductId" });
        }
    }
}
