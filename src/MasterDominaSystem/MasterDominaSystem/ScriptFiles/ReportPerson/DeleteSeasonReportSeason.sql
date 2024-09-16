MERGE INTO "ReportPersons" as RT
USING (SELECT {id} as "MyID") as Q
ON RT."SeasonID" = id
WHEN MATCHED THEN
	UPDATE SET RT."SeasonID" = NULL,
		RT."SeasonName" = NULL;
