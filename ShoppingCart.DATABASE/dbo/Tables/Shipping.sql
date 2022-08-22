CREATE TABLE [dbo].[Shipping] (
    [ShippingId]  INT            IDENTITY (1, 1) NOT NULL,
    [CheckoutId]  INT            NOT NULL,
    [FirstName]   NVARCHAR (50)  NOT NULL,
    [LastName]    NVARCHAR (50)  NULL,
    [Address]     NVARCHAR (500) NOT NULL,
    [City]        NVARCHAR (50)  NULL,
    [State]       NVARCHAR (50)  NULL,
    [Country]     NVARCHAR (50)  NOT NULL,
    [ZipCode]     BIGINT         NOT NULL,
    [Phone]       NVARCHAR (12)  NOT NULL,
    [AddressType] NVARCHAR (50)  NULL,
    [CreatedOn]   DATETIME       NULL,
    [CreatedBy]   INT            NULL,
    [ModifiedOn]  DATETIME       NULL,
    [ModifiedBy]  INT            NULL,
    CONSTRAINT [PK_Shipping] PRIMARY KEY CLUSTERED ([ShippingId] ASC),
    CONSTRAINT [FK_Shipping_Checkout] FOREIGN KEY ([CheckoutId]) REFERENCES [dbo].[Checkout] ([CheckoutId])
);

