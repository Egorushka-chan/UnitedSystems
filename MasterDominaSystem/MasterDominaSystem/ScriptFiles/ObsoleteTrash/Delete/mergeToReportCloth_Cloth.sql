MERGE INTO public."ReportCloths" as RT
	USING (Select {0} as "MyID")
	ON "MyID" = RT."ClothID"
WHEN MATCHED THEN 
	DELETE;