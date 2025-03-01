namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ahihi1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Product", "ahihi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Product", "ahihi");
        }
    }
}
