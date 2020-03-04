using System;
using System.Collections.Generic;
using System.Linq;

using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;
using OffloadWebApi.Repository;

namespace OffloadWebApi.Services
{
    public class OffloadService : IOffloadService
    {
        private IOffloadRepo _offloadRepo;

        public OffloadService(IOffloadRepo offloadRepo)
        {
            this._offloadRepo = offloadRepo;
        }

        public OffloadDetailDto GetOffloadById(int id)
        {
            return this._offloadRepo.GetOffloadById(id);
        }

        private int ParseCount(string count)
        {
            int cnt = 0;
            if(count != null)
            {
                cnt = int.Parse(count);
            }
            if(cnt < 0)
            {
               throw new System.InvalidOperationException();
            }
            if (cnt < 10)
            {
                cnt = 10;
            }
            else if (cnt > 500)
            {
                cnt = 500;
            }

            return cnt;
        }
        
        private List<string> ChangeInputIntoList(string input, string splitter)
        {
            if (input == null)
            {
                return null;
            }
            input = input.ToLower();
            if (input.Contains(splitter))
            {
                return input.Split(splitter).ToList();
            }
            else
            {
                return new List<string>
                { 
                    input 
                };
            }
        }
        private List<string> ParseFishingGear(string fishingGear)
        {
            return ChangeInputIntoList(fishingGear, ",");
        }

        private List<double> parseBoatLength(string boatLength)
        {
            switch (boatLength)
            {
                case null:
                    return new List<double> { 0d, 10000d };
                case "":
                    return new List<double> { 0d, 10000d };
                case "Under 11 m":
                    return new List<double> { 0d, 11d };
                case "11m - 14,99m":
                    return new List<double> { 11d, 14.99d };
                case "15m - 20,99m":
                    return new List<double> { 15d, 20.99d };
                case "21m - 27,99m":
                    return new List<double> { 21d, 27.99d };
                case "28 m og over":
                    return new List<double> { 28d, 10000d };
                default:
                    var retval = ChangeInputIntoList(boatLength, ",");
                    if(retval.Count != 2)
                    {
                        throw new System.NotSupportedException();
                    }
                    return new List<double>
                    {
                        double.Parse(retval[0]),
                        double.Parse(retval[1])
                    };
            }
        }
        private List<string> ParseLandingTown(string fishingGear)
        {
            return null;
        }

        private List<string> ParseLandingState(string fishingGear)
        {
            return null;
        }

        private List<int> ParseMonth(string fishingGear)
        {
            return null;
        }

        private List<int> ParseYear(string fishingGear)
        {
            return null;
        }

        List<TopListDto> IOffloadService.GetOffloads(QueryParamsForTopList filters)
        {
            try
            {
                var parsedFilters = new QueryOffloadsInput
                {
                    Count = ParseCount(filters.count),
                    FishingGear = ParseFishingGear(filters.fishingGear),
                    BoatLength = parseBoatLength(filters.boatLength),
                    LandingTown = ParseLandingTown(filters.landingTown),
                    LandingState = ParseLandingState(filters.landingState),
                    Month = ParseMonth(filters.month),
                    Year = ParseYear(filters.year),
                };
                return this._offloadRepo.GetFilteredResults(parsedFilters);
            }
            catch
            {
                Console.WriteLine("could not parse input");
                return null;
            }
        }
    }
}
