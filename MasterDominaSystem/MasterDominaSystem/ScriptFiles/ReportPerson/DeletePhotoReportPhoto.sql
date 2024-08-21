MERGE INTO "ReportPersons" as RT
USING (SELECT {id} as "MyID") as Q
ON RT."PhotoID" = id
WHEN MATCHED THEN
	UPDATE SET RT."PhotoID" = NULL,
		RT."PhotoName" = NULL,
		RT."PhotoHashCode" = NULL;
