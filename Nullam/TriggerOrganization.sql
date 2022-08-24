CREATE TRIGGER DeleteParticipantTriggerOrganization
	ON [Nullam].[Events]
	AFTER DELETE
	AS
	DELETE FROM [Nullam].[Organizations]
	WHERE EventId NOT IN (SELECT Id FROM [Nullam].[Events])
	GO