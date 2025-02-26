namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSizetoshoppingcart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_OrderDetail", "Size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_OrderDetail", "Size");
        }
    }
}
