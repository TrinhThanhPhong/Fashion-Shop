namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteahihi : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tb_Product", "ahihi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_Product", "ahihi", c => c.String());
        }
    }
}
