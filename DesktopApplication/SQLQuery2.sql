SELECT Programmes.Name, Memberships.StartDate, Memberships.EndDate
FROM Memberships
JOIN Programmes
ON Memberships.Programme = Programmes.Name
JOIN Members
ON Memberships.Member = Members.Id
WHERE Members.Id = 6