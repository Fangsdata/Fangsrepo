using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Services
{
    public class MapService : IMapService
    {
        public List<MapDataDto> GetMapDataByRadioSignal(string radioSignal)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://fangsdata-location-api.herokuapp.com/LastPosition/" + radioSignal);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();

                List<MapDataDto> mapData = new List<MapDataDto>();

                using (Stream dataStream = response.GetResponseStream())
                {
                    try
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
                    catch(System.Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("---- Could not parse map data");
                    }
                }
                response.Close(); 
                return mapData;
            }
            catch(System.Exception e)            
            {
                Console.WriteLine(e);
                Console.WriteLine("---- Could not parse map data");
            }
            return null;
        }
    }
}