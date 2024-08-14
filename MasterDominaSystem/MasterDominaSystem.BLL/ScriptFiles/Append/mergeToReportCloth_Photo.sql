MERGE INTO public."ReportCloths" as RT
	USING (Select {0} as "MyID", '{1}' as "MyName", '{2}' as "MyHashCode", {3} as "MyClothID")
	ON "MyClothID" = RT."ClothID"
WHEN MATCHED AND RT."PhotoID" IS NULL THEN 
	UPDATE SET RT."PhotoName" = "MyName", RT."PhotoHashCode" = "MyClothID"
WHEN MATCHED AND RT."PhotoID" = "MyID" THEN
	UPDATE SET RT."PhotoName" = "MyName", RT."PhotoHashCode" = "MyClothID";
	
	