CREATE TRIGGER DeleteParticipantTriggePerson
	ON [Nullam].[Events]
	AFTER DELETE
	AS
	DELETE FROM [Nullam].[Persons]
	WHERE EventId NOT IN (SELECT Id FROM [Nullam].[Events])
	GO