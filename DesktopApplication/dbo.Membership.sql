CREATE TABLE [dbo].[Membership]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Program] NCHAR(20) NULL, 
    [Price] MONEY NULL
)
