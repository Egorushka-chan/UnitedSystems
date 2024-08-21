MERGE INTO "ReportPersons" as RT
USING (SELECT {id} as "MyID") as Q
ON RT."SetID" = id
WHEN MATCHED THEN
	UPDATE SET RT."SetID" = NULL,
		RT."SetName" = NULL,
		RT."SetDescription" = NULL;
