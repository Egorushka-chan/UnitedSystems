MERGE INTO public."ReportCloths" as RT
	USING (Select {0} as "MyID", {1} as "MyName", {2} as "MyType")
	ON "MyID" = RT."PersonID"
WHEN MATCHED THEN
	UPDATE SET RT."PersonName" = "MyName", RT."PersonType" = "MyType"
WHEN NOT MATCHED THEN
	INSERT (RT."PersonID", RT."PersonName", RT."PersonType"
	VALUES ("MyID", "MyName", "MyType");