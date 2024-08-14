MERGE INTO public."ReportCloths" as RT
	USING (Select {0} as "MyID")
	ON "MyID" = RT."MaterialID"
WHEN MATCHED THEN
	UPDATE SET RT."MaterialID" = NULL, RT."MaterialName" = NULL, RT."MaterialDescription" = NULL;