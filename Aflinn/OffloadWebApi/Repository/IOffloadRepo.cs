﻿using System;
using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;

namespace OffloadWebApi.Repository
{
    public interface IOffloadRepo
    {
        List<TopListDto> GetFilteredResults(QueryOffloadsInput filters);
        #nullable enable
        BoatDto? GetBoatByRadioSignal(string BoatRadioSignalId);
        BoatDto? GetBoatByRegistration(string RegistrationId);
        List<OffloadDto> GetLastOffloadsFromBoat(string boatRegistrationId, int count, int Offset);

        OffloadDto GetSingleOffload(string offloadId);
        OffloadDto GetSingleOffloadByDateAndBoat(string date, string RegistrationId);

        List<BoatSimpleDto>? SearchForBoat(string boatSearchTerm, int count, int Offset);
        
        string GetValue(string key);
    }
}