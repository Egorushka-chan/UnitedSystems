MERGE INTO public."ReportCloths" as RT
	USING (Select {0} as "MyID")
	ON "MyID" = RT."PhotoID"
WHEN MATCHED THEN 
	UPDATE SET RT."PhotoID" = NULL, RT."PhotoName" = NULL, RT."PhotoDescription" = NULL;