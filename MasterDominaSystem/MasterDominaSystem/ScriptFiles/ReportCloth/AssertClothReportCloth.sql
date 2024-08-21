CREATE OR REPLACE PROCEDURE AssertClothReportCloth
(MyClothID integer, MyClothName text, MyClothDescription text, MyClothRating integer, MyClothSize text,
	materialsIDs integer[] , photoIDs integer[]) as $$
DECLARE
	mID integer;
	pID integer;
BEGIN
	Delete From "ReportCloth" where "ClothID" = MyClothID;
	MERGE INTO "ReportCloths" as RT
	USING (Select MyClothID, MyClothName, MyClothRating, MyClothSize) AS Q
	ON MyClothID = RT."ClothID"
		WHEN MATCHED THEN
			DELETE
		WHEN NOT MATCHED THEN
			INSERT (RT."ClothName", RT."ClothRating", RT."ClothSize")
			VALUES (MyClothName, MyClothRating, MyClothSize);

	IF COALESCE(MyClothDescription, -9) != -9
		THEN UPDATE "ReportCloths" as RT
			 SET RT."ClothDescription" = MyClothDescription
			 WHERE RT."ClothID" = MyClothID;
	END IF;

	FOREACH mID in ARRAY materialIDs
	LOOP
		MERGE INTO "ReportCloths" as RT
		USING (Select MyClothID) as Q
		ON RT."ClothID" = MyClothID AND RT."MaterialID" IS NULL
			WHEN MATCHED THEN
				UPDATE SET RT."MaterialID" = mID
			WHEN NOT MATCHED THEN
				INSERT (RT."ClothID", RT."ClothName", RT."ClothDescription", RT."ClothRating", RT."ClothSize",
						RT."MaterialID")
				VALUES (MyClothID, MyClothName, MyClothDescription, MyClothRating, MyClothSize,
						mID);
	END LOOP;

	FOREACH pID in ARRAY photoIDs
	LOOP
		BEGIN
			MERGE INTO "ReportCloths" as RT
			USING (Select MyClothID) as Q
			ON RT."ClothID" = MyClothID and RT."PhotoID" IS NULL
				WHEN MATCHED THEN
					UPDATE SET RT."PhotoID" = pID
				WHEN NOT MATCHED THEN
					INSERT (RT."ClothID", RT."ClothName", RT."ClothDescription", RT."ClothRating", RT."ClothSize",
							RT."PhotoID")
					VALUES (MyClothID, MyClothName, MyClothDescription, MyClothRating, MyClothSize,
							pID);
			FOREACH mID in ARRAY materialIDs
			LOOP
				MERGE INTO "ReportCloths" as RT
				USING (Select MyClothID, pID) as Q
				ON RT."ClothID" = MyClothID AND RT."PhotoID" = pID AND RT."MaterialID" IS NULL
					WHEN MATCHED THEN
						UPDATE SET RT."MaterialID" = mID
					WHEN NOT MATCHED THEN
						INSERT (RT."ClothID", RT."ClothName", RT."ClothDescription", RT."ClothRating", RT."ClothSize",
								RT."MaterialID", RT."PhotoID")
						VALUES (MyClothID, MyClothName, MyClothDescription, MyClothRating, MyClothSize,
								mID, pID);
			END LOOP;
		END;
	END LOOP;
END $$ LANGUAGE plpgsql