MERGE INTO "ReportPersons" as RT
USING (SELECT {id} as "MyID") as Q
ON RT."ClothID" = id
WHEN MATCHED THEN
	UPDATE SET RT."ClothID" = NULL,
		RT."ClothName" = NULL,
		RT."ClothDescription" = NULL,
		RT."ClothRating" = NULL,
		RT."ClothSize" = NULL;
