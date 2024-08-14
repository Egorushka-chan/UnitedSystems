MERGE INTO "ReportCloths" as RT
	USING (Select {0} as "MyID", {1} as "MyClothID", {2} as "MyMaterialID") as Q
	ON "MyClothID" = RT."ClothID" AND RT."MaterialID" IS NULL
WHEN MATCHED THEN 
	UPDATE SET RT."MaterialID" = "MyMaterialID"
WHEN NOT MATCHED AND RT."ClothID" = "MyClothID" THEN
	INSERT (RT."ClothID", RT."ClothName", RT."ClothDescription", RT."ClothRating", RT."ClothSize",
	RT."PhotoID", RT."PhotoName", RT."PhotoHashCode",
	RT."MaterialID", RT."MaterialName", RT."MaterialDescription")
	Values (RT."ClothID", RT."ClothName", RT."ClothDescription", RT."ClothRating", RT."ClothSize",
	RT."PhotoID", RT."PhotoName", RT."PhotoHashCode",
	"MyID", "MyClothID", "MyMaterialID");