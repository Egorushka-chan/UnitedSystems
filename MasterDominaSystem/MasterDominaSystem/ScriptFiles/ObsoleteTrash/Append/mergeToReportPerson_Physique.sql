MERGE INTO public."ReportPersons" as RT
	USING (Select {0} as "MyID", {1} as "MyWeight", {2} as "MyGrowth", {3} as "MyForce", '{4}' as "MyDescription",
	{5} as "MyPersonID")
	ON "MyPersonID" = RT."PersonID"
WHEN MATCHED AND RT."PhysiqueID" IS NULL OR RT."PhysiqueID" = "MyID" THEN
	UPDATE SET RT."PhysiqueWeight" = "MyWeight", RT."PhysiqueGrowth" = "MyGrowth", RT."PhysiqueForce" = "MyForce",
	RT."PhysiqueDescription" = "MyDescription";