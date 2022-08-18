CREATE TABLE [dbo].[Wishlist] (
    [WishlistId] INT      IDENTITY (1, 1) NOT NULL,
    [BookId]     INT      NOT NULL,
    [UserId]     INT      NOT NULL,
    [IsLiked]    BIT      NOT NULL,
    [CreatedOn]  DATETIME NULL,
    [CreatedBy]  INT      NULL,
    [ModifiedOn] DATETIME NULL,
    [ModifiedBy] INT      NULL,
    CONSTRAINT [PK_Wishlist] PRIMARY KEY CLUSTERED ([WishlistId] ASC),
    CONSTRAINT [FK_Wishlist_Books] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([BookId]),
    CONSTRAINT [FK_Wishlist_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserID])
);

