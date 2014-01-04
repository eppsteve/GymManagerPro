CREATE TABLE [dbo].[Members]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(20) NOT NULL, 
    [Sex] NCHAR(6) NULL, 
    [DateofBirth] DATE NULL, 
    [PersonalTrainer] NCHAR(20) NULL, 
    [Notes] NCHAR(200) NULL, 
    [Contact] NCHAR(10) NULL
)
