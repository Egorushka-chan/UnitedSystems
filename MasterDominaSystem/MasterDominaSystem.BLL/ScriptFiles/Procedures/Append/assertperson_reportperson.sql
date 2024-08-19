CREATE OR REPLACE PROCEDURE assertperson_reportperson(personID integer) as $$
BEGIN
	DELETE FROM "ReportPersons" WHERE "PersonID" = personID;
	MERGE INTO "ReportPersons" as RT
	USING (Select "Persons"."ID" as "PersonID", "Persons"."Name" as "PersonName", "Persons"."Type" as "PersonType",
		"Physiques"."ID" as "PhysiqueID", "Physiques"."Weight" as "PhysiqueWeight",
		"Physiques"."Growth" as "PhysiqueGrowth",  "Physiques"."Force" as "PhysiqueForce",
		"Physiques"."Description" as "PhysiqueDescription",
		"Sets"."ID" as "SetID", "Sets"."Name" as "SetName", "Sets"."Description" as "SetDescription",
		"Seasons"."ID" as "SeasonID", "Seasons"."Name" as "SeasonName",
		"Clothes"."ID" as "ClothID", "Clothes"."Name" as "ClothName", "Clothes"."Description" as "ClothDescription",
		"Clothes"."Rating" as "ClothRating", "Clothes"."Size" as "ClothSize",
		"Photos"."ID" as "PhotoID", "Photos"."Name" as "PhotoName", "Photos"."HashCode" as "PhotoHashCode"
			FROM ((((("Persons" LEFT JOIN "Physiques" ON "Persons"."ID" = "Physiques"."PersonID")
			LEFT JOIN "Sets" on "Sets"."PhysiqueID" = "Physiques"."ID")
			LEFT JOIN "Seasons" on "Sets"."SeasonID" = "Seasons"."ID")
			LEFT JOIN "SetHasClothes" ON "SetHasClothes"."SetID" = "Sets"."ID")
			LEFT JOIN "Clothes" ON "SetHasClothes"."ClothID" = "Clothes"."ID")
			LEFT JOIN "Photos" ON "Photos"."ClothID" = "Clothes"."ID"
			WHERE "PersonID" = personID) as Q
	ON RT."PersonID" = Q."PersonID"
	WHEN MATCHED THEN
		DELETE
	WHEN NOT MATCHED THEN
		INSERT (RT."PersonID", RT."PersonName", RT."PersonType",
			RT."PhysiqueID", RT."PhysiqueWeight", RT."PhysiqueGrowth", RT."PhysiqueForce", RT."PhysiqueDescription",
			RT."SetID", RT."SetName", RT."SetDescription",
			RT."SeasonID", RT."SeasonName",
			RT."ClothID", RT."ClothName", RT."ClothDescription", RT."ClothRating", RT."ClothSize",
			RT."PhotoID", RT."PhotoName", RT."PhotoHashCode")
		Values (Q."PersonID", Q."PersonName", Q."PersonType",
			Q."PhysiqueID", Q."PhysiqueWeight", Q."PhysiqueGrowth", Q."PhysiqueForce", Q."PhysiqueDescription",
			Q."SetID", Q."SetName", Q."SetDescription",
			Q."SeasonID", Q."SeasonName",
			Q."ClothID", Q."ClothName", Q."ClothDescription", Q."ClothRating", Q."ClothSize",
			Q."PhotoID", Q."PhotoName", Q."PhotoHashCode");
END $$
LANGUAGE plpgsql;