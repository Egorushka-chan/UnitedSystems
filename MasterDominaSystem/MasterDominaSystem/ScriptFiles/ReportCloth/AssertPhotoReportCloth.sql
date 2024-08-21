CREATE OR REPLACE PROCEDURE AssertPhotoReportCloth
(MyPhotoID integer, MyPhotoName text, MyPhotoHash text) as $$
BEGIN
	MERGE INTO "ReportCloths" as RT
	USING (Select MyPhotoID) as Q
	ON RT."PhotoID" = MyPhotoID
		WHEN MATCHED THEN
			UPDATE SET RT."PhotoName" = MyPhotoName, RT."PhotoHashCode" = MyPhotoHash;
END $$ LANGUAGE plpgsql