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

        public List<OffloadDto> GetOffloadById(string radioSignal, int count)
        {
            return this._offloadRepo.GetLastOffloadsFromBoat(radioSignal, count);
        }

        private int ParseCount(string count)
        {
            int cnt = 0;
            if(!string.IsNullOrEmpty(count))
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
            if(string.IsNullOrEmpty(input))
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
                return new List<string> { input };
            }
        }
        private List<string> ParseFishingGear(string fishingGear)
        {
            return ChangeInputIntoList(fishingGear, ",");
        }

        private List<double> parseBoatLength(string boatLength)
        {
            Console.WriteLine("in servixe");
            Console.WriteLine(boatLength);
            switch (boatLength)
            {
                case null:
                    return null;
                case "":
                    return null;
                case "'Under 11 m'":
                    return new List<double> { 0d, 11d };
                case "'11m - 14,99m'":
                    return new List<double> { 11d, 14.99d };
                case "'15m - 20,99m'":
                    return new List<double> { 15d, 20.99d };
                case "'21m - 27,99m'":
                    return new List<double> { 21d, 27.99d };
                case "'28 m og over'":
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
        private List<string> ParseLandingTown(string input)
        {
            return ChangeInputIntoList(input, ",");
        }

        private List<string> ParseLandingState(string input)
        {
            return ChangeInputIntoList(input, ",");
        }

        private List<int> ParseMonth(string month)
        {
            if(string.IsNullOrEmpty(month))
            {
                return null;
            }
            var listMonth = ChangeInputIntoList(month, ",");
            var intListMonth = new List<int>();
            for(int i = 0; i < listMonth.Count; i++)
            {
                switch (listMonth[i])
                {
                    case "januar":
                        intListMonth.Add(1);
                        break;
                    case "februar":
                        intListMonth.Add(2);
                        break;
                    case "mars":
                        intListMonth.Add(3);
                        break;
                    case "april":
                        intListMonth.Add(4);
                        break;
                    case "mai":
                        intListMonth.Add(5);
                        break;
                    case "juni":
                        intListMonth.Add(66);
                        break;
                    case "juli":
                        intListMonth.Add(7);
                        break;
                    case "august":
                        intListMonth.Add(8);
                        break;
                    case "september":
                        intListMonth.Add(9);
                        break;
                    case "oktober":
                        intListMonth.Add(10);
                        break;
                    case "november":
                        intListMonth.Add(11);
                        break;
                    case "desember":
                        intListMonth.Add(12);
                        break;
                    default:
                        int iMonth = int.Parse(listMonth[i]);
                        if(iMonth <= 0)
                        {
                            throw new System.NotSupportedException();
                        }
                        else if (iMonth > 12)
                        {
                            throw new System.NotSupportedException();
                        }
                        intListMonth.Add(iMonth);
                        break;
                }
            }
            return intListMonth;
        }

        private List<int> ParseYear(string year)
        {
            if(string.IsNullOrEmpty(year))
            {
                return null;
            }
            var listYear = ChangeInputIntoList(year, ",");
            var iListYear = new List<int>();
            for(int i = 0; i < listYear.Count; i++)
            { 
                int temp = int.Parse(listYear[i]);
                if(temp < 2000)
                {
                    throw new NotSupportedException();
                }
                else if(temp > DateTime.Now.Year)
                {
                    throw new NotSupportedException();
                }
                iListYear.Add(temp);
            }
            return iListYear;
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
                    FishName = ParseFishName(filters.fishType)
                };
                return this._offloadRepo.GetFilteredResults(parsedFilters);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("could not parse input");
                return null;
            }
        }

        private List<string> ParseFishName(string fishType)
        {
            return ChangeInputIntoList(fishType, ",");
        }

        public OffloadDto getOffloadDetails(string offloadId)
        {
            return this._offloadRepo.GetSingleOffload(offloadId);
        }
    }
}
