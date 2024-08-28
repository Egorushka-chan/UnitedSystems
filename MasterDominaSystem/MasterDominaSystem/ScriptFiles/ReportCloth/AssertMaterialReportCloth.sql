CREATE OR REPLACE PROCEDURE AssertMaterialReportCloth
(MyMaterialID integer, MyMaterialName text, MyMaterialDescription text) as $$
BEGIN
	MERGE INTO "ReportCloths" as RT
	USING (SELECT MyMaterialID, MyMaterialName, MyMaterialDescription) as Q
	ON RT."MaterialID" = Q.MyMaterialID
		WHEN MATCHED THEN
			UPDATE SET "MaterialName" = Q.MyMaterialName, "MaterialDescription" = Q.MyMaterialDescription;
END $$ LANGUAGE plpgsql;