using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Repository;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OffloadWebApi.Services
{
    public class BoatService : IBoatService
    {
        private IOffloadRepo _offloadRepo;

        private List<MapDataDto> GetMapData(string BoatRadioSignalId)
        {
            WebRequest request = WebRequest.Create("https://fangsdata-location-api.herokuapp.com/LastPosition/" + BoatRadioSignalId);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();

            List<MapDataDto> mapData = new List<MapDataDto>();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                dynamic json = JObject.Parse(responseFromServer);
                MapDataDto item = new MapDataDto
                {
                    Latitude = json.data.latitude,
                    Longitude = json.data.longitude,
                };
                mapData.Add(item);
            }
            response.Close(); 
            return mapData;
        }

        private string GetImage(string BoatRadioSignalId)
        {
            return "https://upload.wikimedia.org/wikipedia/commons/f/f1/Flag_of_Norway.png";
        }
        public BoatService(IOffloadRepo offloadRepo)
        {
            this._offloadRepo = offloadRepo;
        }
        #nullable enable
        public BoatDto? GetBoat(string BoatRadioSignalId)
        {
            var boat = this._offloadRepo.GetBoatByRadioSignal(BoatRadioSignalId);
            if(boat != null)
            {
                boat.MapData = GetMapData(BoatRadioSignalId);
                boat.Image = GetImage(BoatRadioSignalId);
            }
            return boat;
        }
    }
}