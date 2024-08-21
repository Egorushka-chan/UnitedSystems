CREATE OR REPLACE PROCEDURE AssertMaterialReportCloth
(MyMaterialID integer, MyMaterialName text, MyMaterialDescription text) as $$
BEGIN
	MERGE INTO "ReportCloth" as RT
	USING (SELECT MyMaterialID, MyMaterialName, MyMaterialDescription) as Q
	ON RT."MaterialID" = MyMaterialID
		WHEN MATCHED THEN
			UPDATE SET RT."MaterialName" = MyMaterialName, RT."MaterialDescription" = MyMaterialDescription;
END $$ LANGUAGE plpgsql