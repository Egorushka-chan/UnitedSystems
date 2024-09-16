CREATE OR REPLACE PROCEDURE AssertPhotoReportCloth
(MyPhotoID integer, MyPhotoName text, MyPhotoHash text) as $$
BEGIN
	MERGE INTO "ReportCloths" as RT
	USING (Select MyPhotoID) as Q
	ON RT."PhotoID" = Q.MyPhotoID
		WHEN MATCHED THEN
			UPDATE SET "PhotoName" = MyPhotoName, "PhotoHashCode" = MyPhotoHash;
END $$ LANGUAGE plpgsql;