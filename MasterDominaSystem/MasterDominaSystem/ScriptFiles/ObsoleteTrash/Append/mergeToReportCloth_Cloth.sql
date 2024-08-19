MERGE INTO "ReportCloths" as RT
	USING (Select {0} as "MyID", '{1}' as "MyName", '{2}' as "MyDesc", {3} as "MyRating", '{4}' as "MySize") as Q
	ON "MyID" = RT."ClothID"
WHEN MATCHED THEN
	UPDATE SET RT."ClothName" = "MyName", RT."ClothDescription" = "MyDesc",
	RT."ClothRating" = "MyRating", RT."ClothSize" = "MySize"
WHEN NOT MATCHED THEN
	INSERT (RT."ClothName", RT."ClothDescription", RT."ClothRating", RT."ClothSize")
	VALUES ("MyID", "MyName", "MyDesc", "MyRating", "MySize");
