CREATE DATABASE [Test]
GO

CREATE TABLE [dbo].[Tbl_Category](
	[CategoryId] [int] IDENTITY(101,1) PRIMARY KEY NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[Tbl_Category]
           ([CategoryName])
     VALUES
           ('Clothing'),('Luggage'),('Electronic')


-----------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------
DROP TABLE [dbo].[Tbl_Product]
GO

CREATE TABLE [dbo].[Tbl_Product](
	[ProductId] [int] IDENTITY(1001,2) PRIMARY KEY NOT NULL,
	[ProductName] [nvarchar](50) NULL,
	[CategoryId] [int] NOT NULL,
	[Price] [decimal](18, 0) NULL,
	[Currency] [money] NULL,
	[Units] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE dbo.[Tbl_Product]
   ADD CONSTRAINT FK_product_category FOREIGN KEY (CategoryId)
      REFERENCES [dbo].[Tbl_Category](CategoryId)
GO
----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Tbl_TranLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[EventName] [nvarchar](50) NOT NULL,
	[ProductId] [int] NULL,
	[CategoryId] [int] NULL,
	[ProductName] [nvarchar](50) NULL,
	[Price] [int] NULL,
	[Unit] [int] NULL,
	[Timestamp] [datetime],
 CONSTRAINT [PK_Tbl_TranLog] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tbl_TranLog] ADD  DEFAULT (getdate()) FOR [Timestamp]
GO
------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------

CREATE TRIGGER [dbo].[Trig_Product]
ON [dbo].[Tbl_Product]
FOR INSERT,UPDATE,DELETE
AS
BEGIN
INSERT INTO Tbl_TranLog 
SELECT 
CASE WHEN i.ProductId IS NULL THEN 'Deleted'
WHEN d.ProductId IS NULL THEN 'Added'
ELSE 'Modified'
END AS EventName,
COALESCE(d.ProductId,i.ProductId) AS ProductId,
COALESCE(d.CategoryId,i.CategoryId)AS CategoryId ,
COALESCE(d.ProductName,i.ProductName)AS ProductName,
COALESCE(d.Price,i.Price) AS Price,
COALESCE(d.Units,i.Units)AS Unit,
GetDate() AS Timestamp
FROM INSERTED i
FULL OUTER JOIN DELETED d
ON d.ProductId = i.ProductId
END

GO

ALTER TABLE [dbo].[Tbl_Product] ENABLE TRIGGER [Trig_Product]
GO
-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[Tbl_Product]
           ([ProductName]
           ,[CategoryId]
           ,[Price]
           ,[Currency]
           ,[Units])
     VALUES
           ('Pete England'
           ,101
           ,500
           ,null
           ,10),
		    ('Woodland'
           ,101
           ,1500
           ,null
           ,20)
GO
-----------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------
SELECT * FROM Tbl_Category
SELECT * FROM Tbl_Product
SELECT * FROM Tbl_TranLog


