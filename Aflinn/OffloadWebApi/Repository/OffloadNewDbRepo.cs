using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using OffloadWebApi.Models.EntityModels;
using System.Data.Common;
using System;
using System.Globalization;

namespace OffloadWebApi.Repository
{
    public class OffloadNewDbRepo : IOffloadRepo
    {
        private MySqlConnection _connection { get; }
        public OffloadNewDbRepo(string connectionString) 
        {
            _connection = new MySqlConnection(connectionString);
        }

        ////////////////////////////////////////////////////////////////////
        //// PRIVATE HELPER FUNCTIONS //////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        
        private int GetBoatWeight(int boatWeight1, int boatWeight2)
        {
            if(boatWeight1 < boatWeight2)
            {
                return boatWeight2;
            }
            else
            {
                return boatWeight1;
            }
        }

        private FishDto genSimpleFish(int id = -1, string type = "", string condition = "", string preservation = "", string packaging = "", string quality = "", string application = "", float weight = 0)
        {
            return new FishDto
            {
                Id = id,
                Type = type,
                Condition = condition,
                Preservation = preservation,
                Packaging = packaging,
                Quality = quality,
                Application = application,
                Weight = weight
            };
        }

        private OffloadDto genOffloadDto(string id = "", string town = "", string state = "", DateTime? landingdate = null, float totalWeight = 0.0f, List<FishDto> fish = null, BoatSimpleDto boat = null, List<MapDataDto> mapData = null)
        {
            return new OffloadDto
            {
                Id = id,
                Town = town,
                State = state,
                LandingDate = landingdate,
                TotalWeight = totalWeight,
                Fish = fish,
                Boat = boat,
                MapData = mapData
            };
        } 

        private BoatSimpleDto genSimpleBoat(int id = -1, string registrationId = "", string radioSignalId = "", string name = "", string state = "", string nationality = "", string town = "", double length = 0.0d, string fishingGear = "", string image = "")
        {
            return new BoatSimpleDto
            {
                Id = id,
                Registration_id = registrationId,
                RadioSignalId = radioSignalId,
                Name = name,
                State = state,
                Nationality = nationality,
                Town = town,
                Length = length,
                FishingGear = fishingGear,
                Image = image
            };
        }
        private MapDataDto genMapData(double latitude = 0.0d, double longitude = 0.0d, DateTime? time = null)
        {
            return new MapDataDto
            {
                Latitude = latitude,
                Longitude = longitude,
                Time = time
            };
        }
       
        private TopListDto genTopListDto(int id = -1, string town = "", string state = "", DateTime? landingDate = null, double totalWeight = 0.0d, List<FishSimpleDto> fish = null, string boatRegistrationId = "", string boatRadioSignalId = "", string boatName = "", string boatFishingGear = "", double boatLength = 0.0d, string boatLandingTown = "", string boatLandingState = "", string boatNationoality = "", double avrageTrips = 0.0d, double largestLanding = 0.0d, double smallest = 0.0d, int trips = 0)
        {
            return new TopListDto
            {
                Id = id,
                Town = town,
                State = state,
                LandingDate = landingDate,
                TotalWeight = totalWeight,
                Fish = fish,
                BoatRegistrationId = boatRegistrationId,
                BoatRadioSignalId = boatRadioSignalId,
                BoatName = boatName, 
                BoatFishingGear = boatFishingGear,
                BoatLength = boatLength,
                boatLandingTown = boatLandingTown,
                boatLandingState = boatLandingState,
                BoatNationality = boatNationoality,
                Avrage = avrageTrips,
                LargestLanding = largestLanding,
                Smallest = smallest,
                Trips = trips
            };
        }       
      
        private string AddFilter<T>(List<T> filters, string filterName, bool commas = true)
        {
            string retStr = string.Empty;
            bool firstItem = true;
            for(int i = 0; i < filters.Count; i++)
            {
                if(firstItem)
                {
                    firstItem = false;
                    retStr += "AND ( " + filterName + " = '" + filters[i] + "'";
                }
                else
                {
                    retStr += " OR " + filterName + " = '" + filters[i] + "'";
                }
            }
            retStr += ")";
            if(!commas)
            {
                Console.WriteLine(retStr);
                retStr = retStr.Replace("'", string.Empty);
                Console.WriteLine(retStr);
            }
            return retStr;
        }
      
        ////////////////////////////////////////////////////////////////////
        //// Public Functions //////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        public BoatDto GetBoatByRadioSignal(string BoatRadioSignalId)
        {
            var boat = new BoatDto();
            var cmd = _connection.CreateCommand();
            _connection.Open();

            cmd.CommandText = string.Format(
            @"SELECT              
                `Registreringsmerke (seddel)`,
                `Radiokallesignal (seddel)`,
                `Fartøynavn`,
                `Fartøykommune`,
                `Fartøyfylke`,
                `Fartøynasjonalitet`,
                `Største lengde`,
                `Bruttotonnasje annen`,
                `Bruttotonnasje 1969`,
                `Byggeår`,
                `Motorkraft`,
                `Redskap`,
                `Landingsdato`
            FROM GetBoatByRadioSignal
            WHERE `Radiokallesignal (seddel)` = '{0}'
            ORDER BY `Landingsdato` DESC
            LIMIT 1;", BoatRadioSignalId);
            var reader = cmd.ExecuteReader();
            
            using(reader)
            {
                reader.Read();
                if(reader.HasRows)
                {
                    boat.RegistrationId = reader.GetString(0);
                    boat.RadioSignalId = reader.GetString(1);
                    boat.Name = reader.GetString(2);
                    boat.Town = reader.GetString(3);
                    boat.State = reader.GetString(4);
                    boat.Nationality = reader.GetString(5);
                    boat.Length = reader.GetDouble(6);
                    boat.Weight = GetBoatWeight(reader.GetInt16(7), reader.GetInt16(8));
                    boat.BuiltYear = reader.GetInt16(9);
                    boat.EnginePower = reader.GetInt16(10);
                    boat.FishingGear = reader.GetString(11);
                }
            }

            _connection.Dispose();
            return boat;
        }

        public List<TopListDto> GetFilteredResults(QueryOffloadsInput filters)
        {
            var filteredResults = new List<TopListDto>();
            var cmd = _connection.CreateCommand();
            _connection.Open();

            string boatLength = string.Empty;
            string fishingGear = string.Empty;
            string fishName = string.Empty;
            string landingState = string.Empty;
            string preservationMethood = string.Empty;
            if(filters.BoatLength != null)
            {
                boatLength = string.Format("AND `Største lengde` BETWEEN {0} AND {1}", filters.BoatLength[0].ToString(), filters.BoatLength[1].ToString());
            }
            if(filters.FishingGear != null)
            {
                fishingGear = AddFilter(filters.FishingGear, "`Redskap - gruppe`");
            }
            if(filters.FishName != null)
            {
                fishName = AddFilter(filters.FishName, "`Art - hovedgruppe`");
            }
            if(filters.LandingState != null)
            {
                landingState = AddFilter(filters.LandingState, "`Landingsfylke`");
            }
            if(filters.PreservationMethod != null)
            {
                preservationMethood = AddFilter(filters.PreservationMethod, "Konserveringsmåte");
            }
            
            cmd.CommandText = string.Format(
                @"SELECT
                    `Fartøynavn`, 
                    `Redskap`, 
                    `Største lengde`, 
                    SUM(`Rundvekt`),
                    `Radiokallesignal (seddel)`,
                    `Registreringsmerke (seddel)`,
					`Redskap - gruppe`,
                    `Art - hovedgruppe`,
                    `Landingsfylke`,
                    `Landingsdato`
                    FROM GetFilteredResults
                    WHERE `Landingsdato` BETWEEN CAST('{0}' AS DATE) AND CAST('{1}' AS DATE)
                    {2}
                    {3}
                    {4}
                    {5}
                    {6}
                    GROUP BY `Registreringsmerke (seddel)`
                    ORDER BY SUM(`Rundvekt`) DESC
                    LIMIT {7}
                    OFFSET {8};",
                filters.fromDate,
                filters.toDate,
                boatLength,
                fishingGear,
                fishName,
                landingState,
                preservationMethood,
                filters.Count,
                filters.pageNo); 
            Console.WriteLine(cmd.CommandText);
            var reader = cmd.ExecuteReader();

            using(reader)
            {
                while(reader.Read())
                {
                    var topListDto = genTopListDto(
                        boatName: reader.GetString(0),
                        boatFishingGear: reader.GetString(1),
                        boatLength: reader.GetDouble(2),
                        totalWeight: reader.GetDouble(3),
                        boatRadioSignalId: reader.GetString(4),
                        boatRegistrationId: reader.GetString(5));
                    filteredResults.Add(topListDto);
                }
            }
            _connection.Dispose();
            return filteredResults;
        }

        public List<OffloadDto> GetLastOffloadsFromBoat(string boatRegistrationId, int count, int Offset)
        {
            var lastOffloads = new List<OffloadDto>();
            var cmd = _connection.CreateCommand();
            _connection.Open();
            cmd.CommandText = string.Format(
            @"SELECT  
               `Dokumentnummer`,
                `Landingskommune`,
                `Landingsfylke`,
                `Lon (lokasjon)`,
                `Lat (lokasjon)`,
                `Landingsdato`,
                `Landingsklokkeslett`,
                SUM(`Rundvekt`)
                FROM GetLastOffloadsFromBoat
                WHERE `Registreringsmerke (seddel)` = '{0}'
                GROUP BY `Dokumentnummer`
                ORDER BY `Landingsdato` DESC
                LIMIT {1}
                OFFSET {2};",
            boatRegistrationId,
            count,
            Offset);

            var reader = cmd.ExecuteReader();

            using(reader)
            {
                while(reader.Read())
                {
                    var offload = genOffloadDto(
                        id: reader.GetInt64(0).ToString(),
                        town: reader.GetString(1),
                        state: reader.GetString(2),
                        landingdate: reader.GetDateTime(5),
                        totalWeight: reader.GetFloat(7),
                        mapData: new List<MapDataDto>());

                    offload.MapData.Add(genMapData(
                        latitude: reader.GetDouble(3),
                        longitude: reader.GetDouble(4)));

                    lastOffloads.Add(offload);
                }
            }

            _connection.Dispose();
            return lastOffloads;
        }

        public OffloadDto GetSingleOffload(string offloadId)
        {
            var offload = new OffloadDto();
            var cmd = _connection.CreateCommand();
            _connection.Open();
            cmd.CommandTimeout = 90;

            cmd.CommandText = string.Format(
            @"SELECT 
                    `Dokumentnummer`,
                    `Dokument versjonsnummer`,
                    `Dokument salgsdato`,
                    `Landingskommune`,
                    `Landingsfylke`,
                    `Registreringsmerke (seddel)`,
                    `Fartøynavn`,
                    `Lon (lokasjon)`,
                    `Lat (lokasjon)`,
                    `Landingsdato`,
                    `Landingsklokkeslett`,
                    `Landingsmåned (kode)`,
                    `Lengdegruppe`,
                    `Radiokallesignal (seddel)`,
                    `Art`,
                    `Art - gruppe`,
                    `Art - hovedgruppe`,
                    `Anvendelse`,
                    `Anvendelse hovedgruppe`,
                    `Produkttilstand`,
                    `Konserveringsmåte`,
                    `Landingsmåte`,
                    `Kvalitet`,
                    SUM(`Rundvekt`),
                    `Redskap`,
                    `Redskap - gruppe`,
                    `Redskap - hovedgruppe`,
                    `Art (kode)` as `fish_id`
                FROM GetSingleOffload
                WHERE `Dokumentnummer` = {0} AND `Rundvekt` != 0
                GROUP BY `Art`, `Produkttilstand`, `Kvalitet`, `Anvendelse`, `Landingsmåte`, `Konserveringsmåte`
                ORDER BY SUM(`Rundvekt`) DESC;", offloadId);
            var reader = cmd.ExecuteReader();
            using(reader)
            {
                reader.Read();
                if(reader.HasRows)
                {
                    DateTime? rowLandingTown = null;
                    try
                    {
                       rowLandingTown = reader.GetDateTime(9);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("--- unable to create date");
                    }

                    offload = genOffloadDto(
                        id: reader.GetInt64(0).ToString(),
                        town: reader.GetString(3),
                        state: reader.GetString(4),
                        landingdate: rowLandingTown, 
                        totalWeight: reader.GetFloat(23),
                        boat: genSimpleBoat( 
                            registrationId: reader.GetString(5),
                            radioSignalId: reader.GetString(13),
                            name: reader.GetString(6),
                            fishingGear: reader.GetString(24)));

                    offload.MapData = new List<MapDataDto>();
                    offload.MapData.Add(genMapData(reader.GetDouble(8), reader.GetDouble(7)));

                    offload.Fish = new List<FishDto>();
                    var fishRow = genSimpleFish(
                        reader.GetInt32(27),
                        reader.GetString(14),
                        reader.GetString(19),
                        reader.GetString(20),
                        reader.GetString(21),
                        reader.GetString(22),
                        reader.GetString(17),
                        reader.GetFloat(23));
                    offload.Fish.Add(fishRow);

                    while(reader.Read())
                    {
                        fishRow = genSimpleFish(
                            reader.GetInt32(27),
                            reader.GetString(14),
                            reader.GetString(19),
                            reader.GetString(20),
                            reader.GetString(21),
                            reader.GetString(22),
                            reader.GetString(17),
                            reader.GetFloat(23));
                        offload.Fish.Add(fishRow);
                        offload.TotalWeight += reader.GetFloat(23);
                    }
                }
            }
            _connection.Dispose();
            return offload;
        }

        public List<BoatSimpleDto> SearchForBoat(string boatSearchTerm, int count, int Offset)
        {
            var searchedBoats = new List<BoatSimpleDto>();
            var cmd = _connection.CreateCommand();
            _connection.Open();
            cmd.CommandText = string.Format(
                @"(SELECT 
                    `Registreringsmerke (seddel)`,
                    `Radiokallesignal (seddel)`,
                    `Fartøynavn`,
                    `Fartøyfylke`,
                    `Fartøynasjonalitet`,
                    `Fartøykommune`,
                    `Største lengde`,
                    `Redskap`                
				FROM SearchForBoat
				WHERE `Fartøynavn` LIKE '{0}%' AND `Fartøynavn` != ''
				GROUP BY `Registreringsmerke (seddel)`
				limit {1}
				offset 0)
                UNION
                (SELECT 
                    `Registreringsmerke (seddel)`,
                    `Radiokallesignal (seddel)`,
                    `Fartøynavn`,
                    `Fartøyfylke`,
                    `Fartøynasjonalitet`,
                    `Fartøykommune`,
                    `Største lengde`,
                    `Redskap`                
				FROM SearchForBoat
                WHERE `Radiokallesignal (seddel)` LIKE '{0}%' AND `Radiokallesignal (seddel)` != ''
				OR `Registreringsmerke (seddel)` LIKE '{0}%' AND `Registreringsmerke (seddel)` != ''
				GROUP BY `Registreringsmerke (seddel)`
				limit {1}
				offset 0
                );",
                boatSearchTerm,
                count);
            var reader = cmd.ExecuteReader();

            using(reader)
            {
                while(reader.Read())
                {
                    var boat = genSimpleBoat(
                        registrationId: reader.GetString(0),
                        radioSignalId: reader.GetString(1),
                        name: reader.GetString(2),
                        state: reader.GetString(3),
                        nationality: reader.GetString(4),
                        town: reader.GetString(5),
                        length: reader.GetDouble(6),
                        fishingGear: reader.GetString(7));
                    searchedBoats.Add(boat);
                }
            }
            _connection.Dispose();
            return searchedBoats;
        }

        public BoatDto GetBoatByRegistration(string RegistrationId)
        {
            var boat = new BoatDto();
            var cmd = _connection.CreateCommand();
            _connection.Open();

            cmd.CommandText = string.Format(
            @"SELECT 
                `Registreringsmerke (seddel)`,
                `Radiokallesignal (seddel)` ,
                `Fartøynavn`,
                `Fartøykommune`,
                `Fartøyfylke`,
                `Fartøynasjonalitet`,
                `Største lengde`,
                `Bruttotonnasje annen`,
                `Bruttotonnasje 1969`,
                `Byggeår`,
                `Motorkraft`,
                `Redskap`,
                `Landingsdato`
            FROM GetBoatByRegistration
            WHERE `Registreringsmerke (seddel)` = '{0}'
            ORDER BY `Landingsdato` DESC
            LIMIT 1 ", RegistrationId);
            var reader = cmd.ExecuteReader();
            
            using(reader)
            {
                reader.Read();
                if(reader.HasRows)
                {
                    boat.RegistrationId = reader.GetString(0);
                    boat.RadioSignalId = reader.GetString(1);
                    boat.Name = reader.GetString(2);
                    boat.Town = reader.GetString(3);
                    boat.State = reader.GetString(4);
                    boat.Nationality = reader.GetString(5);
                    boat.Length = reader.GetDouble(6);
                    boat.Weight = GetBoatWeight(reader.GetInt16(7), reader.GetInt16(8));
                    boat.BuiltYear = reader.GetInt16(9);
                    boat.EnginePower = reader.GetInt16(10);
                    boat.FishingGear = reader.GetString(11);
                }
            }

            _connection.Dispose();
            return boat;        
        }
    }
}