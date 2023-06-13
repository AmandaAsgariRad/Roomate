SELECT c.[Name], c.Id
FROM Chore c 
LEFT JOIN RoommateChore rc ON c.Id = rc.ChoreId
WHERE rc.Id is null;