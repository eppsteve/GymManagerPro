CREATE TABLE [dbo].[MembersContact]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MemberID] INT NOT NULL, 
    [Address] NCHAR(30) NULL, 
    [CellPhone] NCHAR(15) NULL, 
    [HomePhone] NCHAR(15) NULL, 
    [email] NCHAR(25) NULL, 
    [EmergencyContact] NCHAR(30) NULL, 
    CONSTRAINT [FK_MembersContact_Members] FOREIGN KEY (MemberID) REFERENCES [Members]([Id])
)
