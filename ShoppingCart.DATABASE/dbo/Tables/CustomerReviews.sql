CREATE TABLE [dbo].[CustomerReviews] (
    [ReviewId]   INT            IDENTITY (1, 1) NOT NULL,
    [BookId]     INT            NOT NULL,
    [UserId]     INT            NOT NULL,
    [Rating]     INT            NULL,
    [Headline]   NVARCHAR (100) NOT NULL,
    [Comments]   NVARCHAR (MAX) NULL,
    [BottomLine] NVARCHAR (100) NULL,
    [NickName]   NVARCHAR (50)  NULL,
    [Location]   NVARCHAR (100) NULL,
    [Industry]   NVARCHAR (100) NULL,
    [JobTitle]   NVARCHAR (50)  NULL,
    [CreatedOn]  DATETIME       NULL,
    [CreatedBy]  INT            NULL,
    [ModifiedOn] DATETIME       NULL,
    [ModifiedBy] INT            NULL,
    CONSTRAINT [PK_CustomerReviews] PRIMARY KEY CLUSTERED ([ReviewId] ASC),
    CONSTRAINT [FK_CustomerReviews_Books] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([BookId]),
    CONSTRAINT [FK_CustomerReviews_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserID])
);

