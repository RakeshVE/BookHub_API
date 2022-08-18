CREATE TABLE [dbo].[PaymentDetails] (
    [PaymentId]       INT            IDENTITY (1, 1) NOT NULL,
    [UserId]          INT            NOT NULL,
    [CheckoutId]      INT            NOT NULL,
    [TransactionId]   NVARCHAR (100) NOT NULL,
    [TransactionType] NVARCHAR (50)  NOT NULL,
    [PaymentMode]     NVARCHAR (50)  NOT NULL,
    [Status]          NVARCHAR (50)  NOT NULL,
    [Amount]          DECIMAL (18)   NOT NULL,
    [CreatedOn]       DATETIME       NULL,
    [CreatedBy]       INT            NULL,
    [ModifiedOn]      DATETIME       NULL,
    [ModifiedBy]      INT            NULL,
    CONSTRAINT [PK_PaymentDetails] PRIMARY KEY CLUSTERED ([PaymentId] ASC),
    CONSTRAINT [FK_PaymentDetails_Checkout] FOREIGN KEY ([CheckoutId]) REFERENCES [dbo].[Checkout] ([CheckoutId]),
    CONSTRAINT [FK_PaymentDetails_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserID])
);

