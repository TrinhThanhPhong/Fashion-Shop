namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Adv", "Alias", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Adv", "Alias");
        }
    }
}
