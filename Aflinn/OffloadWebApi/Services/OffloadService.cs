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

        public List<OffloadDto> GetOffloadById(string boatRegistrationId, int count, int pageNr)
        {
            int offset = (pageNr - 1) * count;
            if(offset < 0)
            {
                offset = 0;
            }
            return this._offloadRepo.GetLastOffloadsFromBoat(boatRegistrationId, count, offset);
        }

        private int ParseCount(string count)
        {
            int cnt = 10;
            if(!string.IsNullOrEmpty(count))
            {
                cnt = int.Parse(count);
            }
            if(cnt < 0)
            {
               throw new System.InvalidOperationException();
            }
            if (cnt < 1)
            {
                cnt = 1;
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
           // boatLength = boatLength.Replace("'", string.Empty);
           // boatLength = boatLength.ToLower();
           switch (boatLength)
            {
                case null:
                    return null;
                case "":
                    return null;
                case "under 11m":
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
            month = month.Replace("'", string.Empty);
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

        private string parseDate(string month, string year, string day = "01")
        {
            return string.Format("{0}-{1}-{2}", year, month, day);
        }
        private string ParseFromDate(List<int> month, List<int> year)
        {
            string m = string.Empty;
            string y = string.Empty;

            if(month == null)
            {
                m = DateTime.Today.Month.ToString();
            }
            else if(month.Count >= 1)
            {
                m = month[0].ToString();
            }
            else
            {
                m = DateTime.Today.Month.ToString();
            }

            if(year == null)
            {
                y = DateTime.Today.Year.ToString();
            }
            else if(year != null || year.Count >= 1)
            {
                y = year[0].ToString();
            }
            else
            {
                y = DateTime.Today.Year.ToString();
            }

            return parseDate(m, y);
        }
        private string ParseToDate(List<int> month, List<int> year)
        {
            string m = string.Empty;
            string y = string.Empty;
            if(month == null)
            {
                m = DateTime.Today.Month.ToString();
            }
            else if(month.Count >= 2)
            {
                m = month[1].ToString();
            }
            else
            {
                m = DateTime.Today.Month.ToString();
            }

            if(year == null)
            {
                y = DateTime.Today.Year.ToString();
            }
            else if(month != null || year.Count >= 2)
            {
                y = year[1].ToString();
            }
            else
            {
                y = DateTime.Today.Year.ToString();
            }

            return parseDate(m, y, "31"); 
        }
        List<TopListDto> IOffloadService.GetOffloads(QueryParamsForTopList filters)
        {
            try
            {
                var parsedFilters = new QueryOffloadsInput
                {
                    Count = ParseCount(filters.count),
                    pageNo = ParsePageNo(filters.pageNo, filters.count),
                    PreservationMethod = ParesPreservationMethod(filters.preservationMethod),
                    FishingGear = ParseFishingGear(filters.fishingGear),
                    BoatLength = parseBoatLength(filters.boatLength),
                    LandingTown = ParseLandingTown(filters.landingTown),
                    LandingState = ParseLandingState(filters.landingState),
                    Month = ParseMonth(filters.month),
                    Year = ParseYear(filters.year),
                    FishName = ParseFishName(filters.fishName),
                    fromDate = ParseFromDate(ParseMonth(filters.month), ParseYear(filters.year)),
                    toDate = ParseToDate(ParseMonth(filters.month), ParseYear(filters.year)),
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

        private List<string> ParesPreservationMethod(string preservationMethod)
        {
            return ChangeInputIntoList(preservationMethod, ",");
        }

        private int ParsePageNo(string pageNo, string count)
        {
            int pageNumber = 1;
            int countOffset = ParseCount(count);
            if(!string.IsNullOrEmpty(pageNo))
            {
                bool sucess = int.TryParse(pageNo, out pageNumber);
                if(!sucess)
                {
                    Console.WriteLine("cant parse page no");
                }
            }
            if(pageNumber <= 0)
            {
                pageNumber = 1;
            }
            else if(pageNumber > 100)
            {
                pageNumber = 100;
            }
            return (pageNumber - 1) * countOffset;
        }

        private List<string> ParseFishName(string fishType)
        {
            return ChangeInputIntoList(fishType, ",");
        }

        public OffloadDto getOffloadDetails(string offloadId)
        {
            return this._offloadRepo.GetSingleOffload(offloadId);
        }
        public OffloadDto getOffloadDetailsByDateAndRegistration(string date, string registrationId)
        {
            return this._offloadRepo.GetSingleOffloadByDateAndBoat(date, registrationId);
        }
    }
}
