SELECT Members.Id, Members.CardNumber, Members.LastName, Members.FirstName, Members.Sex, Members.DateOfBirth,
Members.Street, Members.Suburb, Members.City, Members.PostalCode, Members.HomePhone, Members.CellPhone,
Members.email, Members.Occupation, Members.Programme, Members.Notes, (Trainers.Name + Trainers.Surname) AS PersonalTrainer
FROM Members
LEFT OUTER JOIN Trainers
ON Members.PersonalTrainer = Trainers.Id
WHERE Members.Id = 4
