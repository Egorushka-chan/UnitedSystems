MERGE INTO "ReportPersons" as RT
USING (SELECT {id} as "MyID") as Q
ON RT."PhysiqueID" = id
WHEN MATCHED THEN
	UPDATE SET RT."PhysiqueID" = NULL,
		RT."PhysiqueDescription" = NULL,
		RT."PhysiqueWeight" = NULL,
		RT."PhysiqueGrowth" = NULL,
		RT."PhysiqueForce" = NULL;
