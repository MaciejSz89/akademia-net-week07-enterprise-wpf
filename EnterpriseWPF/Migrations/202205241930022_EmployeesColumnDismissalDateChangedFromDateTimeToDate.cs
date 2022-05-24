namespace EnterpriseWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeesColumnDismissalDateChangedFromDateTimeToDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "DismissalDate", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "DismissalDate", c => c.DateTime());
        }
    }
}
