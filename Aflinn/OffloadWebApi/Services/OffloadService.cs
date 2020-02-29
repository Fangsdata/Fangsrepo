using System;
using System.Collections.Generic;
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

        List<TopListDto> IOffloadService.GetOffloads(QueryParamsForTopList filters)
        {
           /* public string count { get; set; }
            public string[] fishingGear { get; set; }
            public string[] boatLength { get; set; }
            public string[] landingTown { get; set; }
            public string[] landingState { get; set; }
            public string[] month { get; set; }
            public string[] year { get; set; }**/

            var parsedFilters = new QueryOffloadsInput();

            // Valitade Count
            try
            {
                int count = int.Parse(filters.count);
                if(count <= 1)
                {
                    count = 1;
                }
                else if(count >= 500)
                {
                    count = 500;
                }
            }
            catch
            {
                Console.WriteLine("Cannot count to int");
                return null;
            }

            // Valitade fishingGear
            try
            {

            }
            catch
            {
                
            }

            return this._offloadRepo.GetFilteredResults(parsedFilters);
        }
    }
}
