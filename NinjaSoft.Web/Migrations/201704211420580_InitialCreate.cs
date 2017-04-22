namespace NinjaSoft.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contributors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cid = c.String(),
                        OrgName = c.String(),
                        Total = c.Double(nullable: false),
                        Pacs = c.Double(nullable: false),
                        Indivs = c.Double(nullable: false),
                        LogoUrl = c.String(),
                        Cycle = c.String(),
                        Origin = c.String(),
                        Source = c.String(),
                        Notice = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        Politician_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Politicians", t => t.Politician_Id)
                .Index(t => t.Politician_Id);
            
            CreateTable(
                "dbo.Politicians",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BioguideId = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        Cid = c.String(),
                        Comments = c.String(),
                        CongressOffice = c.String(),
                        ExitCode = c.String(),
                        FacebookId = c.String(),
                        Fax = c.String(),
                        Feccandid = c.String(),
                        FirstElected = c.String(),
                        Firstlast = c.String(),
                        Gender = c.String(),
                        Lastname = c.String(),
                        Office = c.String(),
                        Party = c.String(),
                        Phone = c.String(),
                        TwitterId = c.String(),
                        VotesmartId = c.String(),
                        Website = c.String(),
                        YoutubeUrl = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sectors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cid = c.String(),
                        SectorName = c.String(),
                        SectorId = c.String(),
                        Indivs = c.Double(nullable: false),
                        Pacs = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        Politician_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Politicians", t => t.Politician_Id)
                .Index(t => t.Politician_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Summaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cid = c.String(),
                        FirstElected = c.String(),
                        NextElection = c.String(),
                        Total = c.Double(nullable: false),
                        Spent = c.Double(nullable: false),
                        CashOnHand = c.Double(nullable: false),
                        Debt = c.Double(nullable: false),
                        Origin = c.String(),
                        Source = c.String(),
                        Cycle = c.String(),
                        SourceLastUpdated = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Sectors", "Politician_Id", "dbo.Politicians");
            DropForeignKey("dbo.Contributors", "Politician_Id", "dbo.Politicians");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Sectors", new[] { "Politician_Id" });
            DropIndex("dbo.Contributors", new[] { "Politician_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Summaries");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Sectors");
            DropTable("dbo.Politicians");
            DropTable("dbo.Contributors");
        }
    }
}
