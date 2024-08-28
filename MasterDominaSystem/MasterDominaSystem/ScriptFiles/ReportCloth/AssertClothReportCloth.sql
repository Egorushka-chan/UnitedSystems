CREATE OR REPLACE PROCEDURE AssertClothReportCloth
(MyClothID integer, MyClothName text, MyClothDescription text, MyClothRating integer, MyClothSize text,
	materialsIDs integer[] , photoIDs integer[]) as $$
DECLARE
	mID integer;
	pID integer;
BEGIN
	Delete From "ReportCloths" where "ClothID" = MyClothID;
	MERGE INTO "ReportCloths" as RT
	USING (Select MyClothID, MyClothName, MyClothRating, MyClothSize) AS Q
	ON Q.MyClothID = RT."ClothID"
		WHEN MATCHED THEN
			DELETE
		WHEN NOT MATCHED THEN
			INSERT ("ClothID", "ClothName", "ClothRating", "ClothSize")
			VALUES (Q.MyClothID, Q.MyClothName, Q.MyClothRating, Q.MyClothSize);

	IF COALESCE(MyClothDescription, 'Rofls') != 'Rofls'
		THEN UPDATE "ReportCloths" as RT
			 SET "ClothDescription" = MyClothDescription
			 WHERE RT."ClothID" = MyClothID;
	END IF;

	FOREACH mID in ARRAY materialsIDs
	LOOP
		MERGE INTO "ReportCloths" as RT
		USING (Select MyClothID) as Q
		ON RT."ClothID" = Q.MyClothID AND RT."MaterialID" IS NULL
			WHEN MATCHED THEN
				UPDATE SET "MaterialID" = mID
			WHEN NOT MATCHED THEN
				INSERT ("ClothID", "ClothName", "ClothDescription", "ClothRating", "ClothSize",
						"MaterialID")
				VALUES (Q.MyClothID, MyClothName, MyClothDescription, MyClothRating, MyClothSize,
						mID);
	END LOOP;

	FOREACH pID in ARRAY photoIDs
	LOOP
		BEGIN
			MERGE INTO "ReportCloths" as RT
			USING (Select MyClothID) as Q
			ON RT."ClothID" = Q.MyClothID and RT."PhotoID" IS NULL
				WHEN MATCHED THEN
					UPDATE SET "PhotoID" = pID
				WHEN NOT MATCHED THEN
					INSERT ("ClothID", "ClothName", "ClothDescription", "ClothRating", "ClothSize",
							"PhotoID")
					VALUES (Q.MyClothID, MyClothName, MyClothDescription, MyClothRating, MyClothSize,
							pID);
			FOREACH mID in ARRAY materialsIDs
			LOOP
				MERGE INTO "ReportCloths" as RT
				USING (Select MyClothID, pID) as Q
				ON RT."ClothID" = Q.MyClothID AND RT."PhotoID" = Q.pID AND RT."MaterialID" IS NULL
					WHEN MATCHED THEN
						UPDATE SET "MaterialID" = mID
					WHEN NOT MATCHED THEN
						INSERT ("ClothID", "ClothName", "ClothDescription", "ClothRating", "ClothSize",
								"MaterialID", "PhotoID")
						VALUES (Q.MyClothID, MyClothName, MyClothDescription, MyClothRating, MyClothSize,
								mID, Q.pID);
			END LOOP;
		END;
	END LOOP;
END $$ LANGUAGE plpgsql;