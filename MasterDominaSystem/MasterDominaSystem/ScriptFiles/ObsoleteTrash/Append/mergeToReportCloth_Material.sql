MERGE INTO public."ReportCloths" as RT
	USING (Select {0} as "MatID", '{1}' as "MatName", '{2}' as "MatDesc")
	ON "MatID" = RT."MaterialID"
WHEN MATCHED THEN
	UPDATE SET RT."MaterialName" = "MatName", RT."MaterialDescription" = "MatDesc"