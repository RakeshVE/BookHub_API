CREATE TABLE [dbo].[Cart] (
    [CartId]      INT          IDENTITY (1, 1) NOT NULL,
    [UserId]      INT          NOT NULL,
    [BookId]      INT          NOT NULL,
    [Quantity]    INT          NOT NULL,
    [CartTotal]   INT          NOT NULL,
    [DiscountPer] DECIMAL (18) NULL,
    [NetPay]      DECIMAL (18) NOT NULL,
    [IsActive]    BIT          NULL,
    [CreatedOn]   DATETIME     NULL,
    [CreatedBy]   INT          NULL,
    [ModifiedOn]  DATETIME     NULL,
    [ModifiedBy]  INT          NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([CartId] ASC),
    CONSTRAINT [FK_Cart_Books] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([BookId]),
    CONSTRAINT [FK_Cart_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserID])
);

