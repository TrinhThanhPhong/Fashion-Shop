namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateKey32 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Tb_OrderDetail");
            AddPrimaryKey("dbo.Tb_OrderDetail", new[] { "OrderId", "ProductId" });
            DropColumn("dbo.Tb_OrderDetail", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_OrderDetail", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Tb_OrderDetail");
            AddPrimaryKey("dbo.Tb_OrderDetail", "Id");
        }
    }
}
