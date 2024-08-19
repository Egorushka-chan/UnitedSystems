MERGE INTO "ReportPersons" as RT
	USING (Select {0} as "MyID", {1} as "MyName", {2} as "MyDesc", {3} as "MyPhysiqueID") as Q
	ON "MyPhysiqueID" = RT."PhysiqueID"
WHEN MATCHED AND RT."SetID" IS NULL OR RT."SetID" = "MyID" THEN
	UPDATE SET 
		RT."SetID" = "MyID",
		RT."PhysiqueWeight" = "MyWeight",
		RT."PhysiqueGrowth" = "MyGrowth",
		RT."PhysiqueForce" = "MyForce",
		RT."PhysiqueDescription" = "MyDescription";
