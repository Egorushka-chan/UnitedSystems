﻿//syntax = "proto3";

//option csharp_namespace = "WODownloaderClient";

//service WODownloader{
//	rpc DownloadDatabaseEntities (RequestDownload) returns (stream ResponseDownload);
//}

//message ClothProto{
//	int32 ID = 1;
//	string Name = 2;
//	optional string Description = 3;
//	optional int32 Rating = 4;
//	optional string Size = 5;
//}

//message PersonProto{
//	int32 ID = 1;
//	string Name = 2;
//	optional string Type = 3;
//}

//message PhysiqueProto{
//	int32 ID = 1;
//	int32 Weight = 2;
//	int32 Growth = 3;
//	int32 Force = 4;
//	optional string Description = 5;
//	int32 PersonID = 6;
//}

//message SetProto{
//	int32 ID = 1;
//	string Name = 2;
//	optional string Description = 3;
//	int32 PhysiqueID = 4;
//	int32 SeasonID = 5;
//}

//message SeasonProto{
//	int32 ID = 1;
//	string Name = 2;
//}

//message SetHasClothesProto{
//	int32 ID = 1;
//	int32 ClothID = 2;
//	int32 SetID = 3;
//}

//message MaterialProto{
//	int32 ID = 1;
//	string Name = 2;
//	optional string Description = 3;
//}

//message ClothHasMaterialsProto{
//	int32 ID = 1;
//	int32 ClothID = 2;
//	int32 MaterialID = 3;
//}

//message PhotoProto{
//	int32 ID = 1;
//	string Name = 2;
//	string HashCode = 3;
//	bool IsDBStored = 4;
//	int32 ClothID = 5;
//}

//message RequestDownload{
//	bool Proceed = 1;
//}

//message ResponseDownload{
//	int32 PackageNumber = 1;
//	int32 PackageSize = 2;
//	repeated ClothProto Cloths = 3;
//	repeated PersonProto Persons = 4;
//	repeated PhysiqueProto Physiques = 5;
//	repeated SetProto Sets = 6;
//	repeated SeasonProto Seasons = 7;
//	repeated SetHasClothesProto SetHasClothes = 8;
//    repeated MaterialProto Materials = 9;
//	repeated ClothHasMaterialsProto ClothHasMaterials = 10;
//	repeated PhotoProto Photos = 11;
//}