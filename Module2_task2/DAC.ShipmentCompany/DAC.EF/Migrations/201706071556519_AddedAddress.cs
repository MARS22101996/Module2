namespace DAC.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouse", "Address", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Warehouse", "Address");
        }
    }
}
