﻿using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;

namespace OffloadWebApi.Repository
{
    public class OffloadRepo : IOffloadRepo
    {
        public OffloadRepo()
        {
        }

        public BoatDto GetBoatByRadioSignal(string BoatRadioSignalId)
        {
            throw new NotImplementedException();
        }

        public List<OffloadDto> GetFilteredResults(QueryOffloadsInput filters)
        {
            throw new NotImplementedException();
        }

        public OffloadDetailDto GetOffloadById(int id)
        {
            throw new NotImplementedException();
        }

        List<TopListDto> IOffloadRepo.GetFilteredResults(QueryOffloadsInput filters)
        {
            throw new NotImplementedException();
        }
    }
}