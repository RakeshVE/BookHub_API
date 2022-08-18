CREATE TABLE [dbo].[Coupon] (
    [CouponId]    INT           IDENTITY (1, 1) NOT NULL,
    [CouponCode]  NVARCHAR (50) NOT NULL,
    [BookId]      INT           NOT NULL,
    [DiscountPer] DECIMAL (18)  NOT NULL,
    [Validity]    DATETIME      NOT NULL,
    [IsActive]    BIT           NULL,
    [CreatedOn]   DATETIME      NULL,
    [CreatedBy]   INT           NULL,
    [ModifiedOn]  DATETIME      NULL,
    [ModifiedBy]  INT           NULL,
    CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED ([CouponId] ASC),
    CONSTRAINT [FK_Coupon_Books] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([BookId])
);

