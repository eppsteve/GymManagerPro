CREATE TABLE [dbo].[Trainers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(25) NULL, 
    [DateOfBirth] DATE NULL, 
    [Phone] NCHAR(15) NULL, 
    [Salary] NCHAR(10) NULL, 
    [Notes] NCHAR(200) NULL, 
    [Address] NCHAR(20) NULL
)
