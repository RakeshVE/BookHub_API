CREATE TABLE [dbo].[Checkout] (
    [CheckoutId] INT          IDENTITY (1, 1) NOT NULL,
    [CouponId]   INT          NULL,
    [UserId]     INT          NOT NULL,
    [Tax]        DECIMAL (18) NOT NULL,
    [Shipping]   DECIMAL (18) NOT NULL,
    [FinalPay]   DECIMAL (18) NOT NULL,
    [CreatedOn]  DATETIME     NULL,
    [CreatedBy]  INT          NULL,
    [ModifiedOn] DATETIME     NULL,
    [ModifiedBy] INT          NULL,
    CONSTRAINT [PK_Checkout] PRIMARY KEY CLUSTERED ([CheckoutId] ASC),
    CONSTRAINT [FK_Checkout_Coupon] FOREIGN KEY ([CouponId]) REFERENCES [dbo].[Coupon] ([CouponId])
);

