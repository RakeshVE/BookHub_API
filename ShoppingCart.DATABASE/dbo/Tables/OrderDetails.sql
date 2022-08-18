CREATE TABLE [dbo].[OrderDetails] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [OrderId]    INT           NOT NULL,
    [CheckoutId] INT           NOT NULL,
    [BookId]     INT           NOT NULL,
    [UserId]     INT           NOT NULL,
    [Status]     NVARCHAR (50) NULL,
    [Quantity]   INT           NULL,
    [Price]      DECIMAL (18)  NULL,
    [CreatedOn]  DATETIME      NULL,
    [CreatedBy]  INT           NULL,
    [ModifiedOn] DATETIME      NULL,
    [ModifiedBy] INT           NULL,
    CONSTRAINT [PK_OrderDetails_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderDetails_Books] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([BookId]),
    CONSTRAINT [FK_OrderDetails_Checkout] FOREIGN KEY ([CheckoutId]) REFERENCES [dbo].[Checkout] ([CheckoutId]),
    CONSTRAINT [FK_OrderDetails_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserID])
);

