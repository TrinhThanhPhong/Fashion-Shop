﻿namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Order", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Order", "Email");
        }
    }
}
