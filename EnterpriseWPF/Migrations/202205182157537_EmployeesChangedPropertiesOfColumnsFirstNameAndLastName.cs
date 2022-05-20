namespace EnterpriseWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeesChangedPropertiesOfColumnsFirstNameAndLastName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "LastName", c => c.String());
            AlterColumn("dbo.Employees", "FirstName", c => c.String());
        }
    }
}
