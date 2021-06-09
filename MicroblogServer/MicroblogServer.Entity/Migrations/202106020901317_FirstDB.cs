namespace MicroblogServer.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FollowerUserId = c.Guid(nullable: false),
                        FollowedUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LikeTweets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TweetId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tweets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Message = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        PhoneNumber = c.String(),
                        Image = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Tweets");
            DropTable("dbo.LikeTweets");
            DropTable("dbo.Follows");
        }
    }
}
