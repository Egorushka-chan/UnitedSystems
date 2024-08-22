MERGE INTO "ReportPersons" as RT
USING (SELECT {id} as "MyID") as Q
ON RT."PhysiqueID" = id
WHEN MATCHED THEN
	UPDATE SET RT."PhysiqueID" = NULL,
		RT."PhysiqueDescription" = NULL,
		RT."PhysiqueWeight" = NULL,
		RT."PhysiqueGrowth" = NULL,
		RT."PhysiqueForce" = NULL,
		RT."SetID" = NULL,
		RT."SetName" = NULL,
		RT."SetDescription" = NULL,
		RT."ClothID" = NULL,
		RT."ClothName" = NULL,
		RT."ClothDescription" = NULL,
		RT."ClothRating" = NULL,
		RT."ClothSize" = NULL,
		RT."PhotoID" = NULL,
		RT."PhotoName" = NULL,
		RT."PhotoHashCode" = NULL;