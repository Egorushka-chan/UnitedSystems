MERGE INTO "ReportPersons" as RT
	USING (Select '{0}' as "MyID", '{1}' as "MyName", '{2}' as "MyType") AS Q
	ON "MyID" = RT."PersonID"
WHEN MATCHED THEN
	UPDATE SET RT."PersonName" = "MyName", RT."PersonType" = "MyType"
WHEN NOT MATCHED THEN
	INSERT (RT."PersonID", RT."PersonName", RT."PersonType")
	VALUES ("MyID", "MyName", "MyType");