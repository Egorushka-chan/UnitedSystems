﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations
{
    /// <inheritdoc cref="IGeneralInfoProvider"/>
    public class GeneralInfoProvider(IDistributedCache cache,
                                     IWardrobeContext context,
                                     ILogger<GeneralInfoProvider> logger) : IGeneralInfoProvider // не факт что понадобится
    {
        //private enum GeneralInfoType
        //{
        //    PersonClothCount = 0,
        //    PersonSetCount = 1,
        //    TotalPersonCount = 2
        //}

        //private static Dictionary<int, string[]> localization = new()
        //{

        //}

        public async Task<int> GetPersonClothCount(int id, CancellationToken token = default)
        {
            logger.LogTrace("Getting cloths count from person with ID: {id} from GeneralInfoProvider", id);

            byte[]? response = await cache.GetAsync($"person_cloth_count_{id}", token);
            if (response != null)
            {
                int clothes = BitConverter.ToInt32(response, 0);
                logger.LogDebug("Found person: {id} cloth count: {clothes} in cache!", id, clothes);
                return clothes;
            }
            else
            {
                logger.LogTrace("Cache info about person {id} clothes wasn't found, sending query to context", id);

                var physiques = from Person person in context.Persons
                                select person.Physiques;

                var sets = from Physique physuqie in physiques
                           select physuqie.Sets;

                var setClothes = from Set set in sets
                                 select set.SetHasClothes;

                int clothes = await (from SetHasClothes setCloth in setClothes
                                     select setCloth.ClothID).Distinct().CountAsync(cancellationToken: token);

                logger.LogTrace("Get person: {id} cloth count: {clothes} from context", id, clothes);

                var setter = cache.SetAsync($"person_cloth_count_{id}",
                    BitConverter.GetBytes(clothes),
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
                    },
                    token);
                logger.LogDebug("Put person: {id} cloth count: {clothes} into cache!", id, clothes);
                await setter;

                return clothes;
            }
        }

        public async Task<int> GetPersonSetCount(int id, CancellationToken token = default)
        {
            logger.LogTrace("Getting set count from person with ID: {id} from GeneralInfoProvider", id);

            byte[]? response = await cache.GetAsync($"person_set_count_{id}", token);
            if (response != null)
            {
                int sets = BitConverter.ToInt32(response, 0);
                logger.LogDebug("Found person: {id} set count: {sets} in cache!", id, sets);
                return sets;
            }
            else
            {
                logger.LogTrace("Cache info about person {id} sets wasn't found, sending query to context", id);

                var physiques = from Person person in context.Persons
                                select person.Physiques;

                int sets = await (from Physique physique in physiques
                                  select physique.Sets).Distinct().CountAsync(cancellationToken: token);

                logger.LogTrace("Get person: {id} sets count: {sets} from context", id, sets);

                var setter = cache.SetAsync($"person_set_count_{id}",
                    BitConverter.GetBytes(sets),
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
                    },
                    token);
                logger.LogDebug("Put person: {id} set count: {sets} into cache!", id, sets);
                await setter;

                return sets;
            }
        }

        public async Task<int> GetTotalPersonCount(CancellationToken token = default)
        {
            logger.LogTrace("Getting total person count from GeneralInfoProvider");

            byte[]? response = await cache.GetAsync("total_person_count", token);
            if (response != null)
            {
                int total = BitConverter.ToInt32(response, 0);
                logger.LogDebug("Found total person count: {total} in cache!", total);
                return total;
            }
            else
            {
                logger.LogTrace("Total person count in cache wasn't found, sending query to context");

                int total = context.Persons.Count();

                logger.LogTrace("Get total person count: {total} from context", total);

                var setter = cache.SetAsync("total_person_count",
                    BitConverter.GetBytes(total),
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                    },
                    token);
                logger.LogDebug("Put total person count: {total} into cache!", total);
                await setter;

                return total;
            }
        }
    }
}