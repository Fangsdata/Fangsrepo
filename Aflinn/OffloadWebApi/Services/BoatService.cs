using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Repository;
using System.Collections.Generic;

namespace OffloadWebApi.Services
{
    public class BoatService : IBoatService
    {
        private IOffloadRepo _offloadRepo;

        private string GetImage(string BoatRadioSignalId)
        {
            return "https://upload.wikimedia.org/wikipedia/commons/f/f1/Flag_of_Norway.png";
        }
        public BoatService(IOffloadRepo offloadRepo, IMapService mapService)
        {
            this._offloadRepo = offloadRepo;
        }
        #nullable enable
        public BoatDto? GetBoatByRadio(string BoatRadioSignalId)
        {
            var boat = this._offloadRepo.GetBoatByRadioSignal(BoatRadioSignalId);
            if(boat != null)
            {
                boat.Image = GetImage(BoatRadioSignalId);
            }
            return boat;
        }
        public BoatDto? GetBoatByRegistration(string boatRegistrationId)
        {
            var boat = this._offloadRepo.GetBoatByRegistration(boatRegistrationId);
            if(boat != null)
            {
                boat.Image = GetImage(boat.RadioSignalId);
            }
            return boat;
        }
        public List<BoatSimpleDto>? SearchForBoat(string boatSearchTerm, int count, int pageNo)
        {
            int offset = (pageNo - 1) * count;
            if(offset < 0)
            {
                offset = 0;
            }
            var boats = _offloadRepo.SearchForBoat(boatSearchTerm, count, offset); 
            if (boats == null)
            {
                return null;
            }

            foreach(var boat in boats)
            {
                boat.Image = GetImage(boat.RadioSignalId);
            }
            return boats;
        }
    }
}