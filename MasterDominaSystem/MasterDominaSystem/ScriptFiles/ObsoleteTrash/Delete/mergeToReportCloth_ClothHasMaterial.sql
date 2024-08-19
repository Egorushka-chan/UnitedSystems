MERGE INTO public."ReportCloths" as RT
	USING (Select {0} as "MyID", {1} as "MyClothID", {2} as "MyMaterialID")
	ON "MyClothID" = RT."ClothID"
WHEN MATCHED and "MyMaterialID" = RT."MaterialID" THEN 
	UPDATE SET RT."MaterialID" = NULL, RT."MaterialName" = NULL, RT."MaterialDescription" = NULL;