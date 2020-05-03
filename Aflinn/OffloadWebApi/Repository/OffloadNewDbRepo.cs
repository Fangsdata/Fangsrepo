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
       
        private TopListDto genTopListDto(int id = -1, string town = "", string state = "", DateTime? landingDate = null, double totalWeight = 0.0d, List<FishSimpleDto> fish = null, string boatRegistrationId = "", string boatRadioSignalId = "", string boatName = "", string boatFishingGear = "", double BoatLength = 0.0d, string boatLandingTown = "", string boatLandingState = "", string boatNationoality = "", double avrageTrips = 0.0d, double largestLanding = 0.0d, double smallest = 0.0d, int trips = 0)
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
                BoatLength = BoatLength,
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
                    retStr += " And (" + filterName + " = '" + filters[i] + "'";
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
                Aflinn_Landings.`Registreringsmerke (seddel)`,
                Aflinn_Boats.`Radiokallesignal (seddel)` ,
                Aflinn_Landings.`Fartøynavn`,
                Aflinn_Boats.`Fartøykommune`,
                Aflinn_Boats.`Fartøyfylke`,
                Aflinn_Boats.`Fartøynasjonalitet`,
                Aflinn_Boats.`Største lengde`,
                Aflinn_Boats.`Bruttotonnasje annen`,
                Aflinn_Boats.`Bruttotonnasje 1969`,
                Aflinn_Boats.`Byggeår`,
                Aflinn_Boats.`Motorkraft`,
                Aflinn_Fishing_gear.`Redskap`
            FROM Aflinn_Landings
            LEFT JOIN Aflinn_Boats ON Aflinn_Landings.`Registreringsmerke (seddel)` = Aflinn_Boats.`Registreringsmerke (seddel)`
            LEFT JOIN Aflinn_Fishing_gear ON Aflinn_Landings.`Redskap (kode)` = Aflinn_Fishing_gear.`Redskap (kode)`
            WHERE Aflinn_Boats.`Radiokallesignal (seddel)` = '{0}'
            ORDER BY Aflinn_Landings.`Landingsdato` DESC
            LIMIT 1   ", BoatRadioSignalId);
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

            _connection.Close();
            return boat;
        }

        public List<TopListDto> GetFilteredResults(QueryOffloadsInput filters)
        {
            var filteredResults = new List<TopListDto>();
            var cmd = _connection.CreateCommand();
            _connection.Open();
            cmd.CommandText = string.Format("SELECT * from");

            var reader = cmd.ExecuteReader();

            string fromDate = string.Format("2020-03-01");
            string toDate = string.Format("2020-04-01");
            cmd.CommandText += string.Format(" WHERE {0}{1}", filters.fromDate, filters.toDate);

            if(filters.BoatLength != null)
            {
                string lenStr = string.Format("AND (boat_length BETWEEN {0} AND {1})", filters.BoatLength[0], filters.BoatLength[1]);
                cmd.CommandText = cmd.CommandText + lenStr;
            }
            if(filters.FishingGear != null)
            {
                cmd.CommandText = cmd.CommandText + AddFilter(filters.FishingGear, "fishing_gear");
            }
            if(filters.FishName != null)
            {
                cmd.CommandText = cmd.CommandText + AddFilter(filters.FishName, "fish_name");
            }
            if(filters.LandingState != null)
            {
                cmd.CommandText = cmd.CommandText + AddFilter(filters.LandingState, "landing_state");
            }
            
            using(reader)
            {
                while(reader.Read())
                {
                    var topListDto = genTopListDto();
                    filteredResults.Add(topListDto);
                }
            }

            return filteredResults;
        }

        public List<OffloadDto> GetLastOffloadsFromBoat(string BoatRadioSignalId, int count, int Offset)
        {
            var lastOffloads = new List<OffloadDto>();
            var cmd = _connection.CreateCommand();
            _connection.Open();
            cmd.CommandText = string.Format(
            @"SELECT Aflinn_Landings.`Dokumentnummer`,
                Aflinn_Landing_town.`Landingskommune`,
                Aflinn_Landing_state.`Landingsfylke`,
                Aflinn_Landings.`Lon (lokasjon)`,
                Aflinn_Landings.`Lat (lokasjon)`,
                Aflinn_Landings.`Landingsdato`,
                Aflinn_Landings.`Landingsklokkeslett`,
                SUM(Aflinn_Landings.`Rundvekt`) AS Rundvekt
                FROM Aflinn_Landings
                LEFT JOIN Aflinn_Landing_town ON Aflinn_Landings.`Landingskommune (kode)` = Aflinn_Landing_town.`Landingskommune (kode)`
                LEFT JOIN Aflinn_Landing_state ON Aflinn_Landings.`Landingsfylke (kode)` = Aflinn_Landing_state.`Landingsfylke (kode)`
                LEFT JOIN Aflinn_Boats ON Aflinn_Landings.`Registreringsmerke (seddel)` = Aflinn_Boats.`Registreringsmerke (seddel)`
                LEFT JOIN Aflinn_Landings_id_date ON Aflinn_Landings.`Dokumentnummer` = Aflinn_Landings_id_date.`Dokumentnummer` AND Aflinn_Landings.`Landingsdato` = Aflinn_Landings_id_date.`Landingsdato` AND Aflinn_Landings.`Linjenummer` = Aflinn_Landings_id_date.`Linjenummer`
                WHERE Aflinn_Boats.`Radiokallesignal (seddel)` = '{0}'
                GROUP BY Aflinn_Landings.`Dokumentnummer`
                ORDER BY Aflinn_Landings.`Landingsdato` DESC
                LIMIT {1}
                OFFSET {2}",
            BoatRadioSignalId,
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

            _connection.Close();
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
                Aflinn_Landings.`Dokumentnummer`,
                Aflinn_Landings.`Dokument versjonsnummer`,
                Aflinn_Landings.`Dokument salgsdato`,
                Aflinn_Landing_town.`Landingskommune`,
                Aflinn_Landing_state.`Landingsfylke`,
                Aflinn_Landings.`Registreringsmerke (seddel)`,
                Aflinn_Landings.`Fartøynavn`,
                Aflinn_Landings.`Lon (lokasjon)`,
                Aflinn_Landings.`Lat (lokasjon)`,
                Aflinn_Landings.`Landingsdato`,
                Aflinn_Landings.`Landingsklokkeslett`,
                Aflinn_Landings.`Landingsmåned (kode)`,
                Aflinn_Boats.`Lengdegruppe`,
                Aflinn_Boats.`Radiokallesignal (seddel)`,
                Aflinn_Fish.`Art`,
                Aflinn_Fish.`Art - gruppe`,
                Aflinn_Fish.`Art - hovedgruppe`,
                Aflinn_Landings.`Anvendelse`,
                Aflinn_Landings.`Anvendelse hovedgruppe`,
                Aflinn_Fish_condition.`Produkttilstand`,
                Aflinn_Fish_preservation.`Konserveringsmåte`,
                Aflinn_Packaging.`Landingsmåte`,
                Aflinn_Fish_quality.`Kvalitet`,
                Aflinn_Landings.`Rundvekt`,
                Aflinn_Fishing_gear.`Redskap`,
                Aflinn_Fishing_gear.`Redskap - gruppe`,
                Aflinn_Fishing_gear.`Redskap - hovedgruppe`,
                Aflinn_Fish.`Art (kode)` as `fish_id`
            FROM Aflinn_Landings
                LEFT JOIN Aflinn_Landing_town ON Aflinn_Landings.`Landingskommune (kode)` = Aflinn_Landing_town.`Landingskommune (kode)`
                LEFT JOIN Aflinn_Landing_state ON Aflinn_Landings.`Landingsfylke (kode)` = Aflinn_Landing_state.`Landingsfylke (kode)`
                LEFT JOIN Aflinn_Boats ON Aflinn_Landings.`Registreringsmerke (seddel)` = Aflinn_Boats.`Registreringsmerke (seddel)`
                LEFT JOIN Aflinn_Fishing_gear ON Aflinn_Landings.`Redskap (kode)` = Aflinn_Fishing_gear.`Redskap (kode)`
                LEFT JOIN Aflinn_Fish ON Aflinn_Landings.`Art (kode)` = Aflinn_Fish.`Art (kode)`
                LEFT JOIN Aflinn_Fish_condition ON Aflinn_Landings.`Produkttilstand (kode)` = Aflinn_Fish_condition.`Produkttilstand (kode)`
                LEFT JOIN Aflinn_Fish_preservation ON Aflinn_Landings.`Konserveringsmåte (kode)` = Aflinn_Fish_preservation.`Konserveringsmåte (kode)`
                LEFT JOIN Aflinn_Packaging ON Aflinn_Landings.`Landingsmåte (kode)` = Aflinn_Packaging.`Landingsmåte (kode)`
                LEFT JOIN Aflinn_Fish_quality ON Aflinn_Landings.`Kvalitet (kode)` = Aflinn_Fish_quality.`Kvalitet (kode)`
                LEFT JOIN Aflinn_Landings_id_date ON Aflinn_Landings.`Dokumentnummer` = Aflinn_Landings_id_date.`Dokumentnummer` AND Aflinn_Landings.`Landingsdato` = Aflinn_Landings_id_date.`Landingsdato` AND Aflinn_Landings.`Linjenummer` = Aflinn_Landings_id_date.`Linjenummer`
                WHERE Aflinn_Landings.`Dokumentnummer` = {0}", offloadId);
            var reader = cmd.ExecuteReader();
            using(reader)
            {
                reader.Read();
                if(reader.HasRows)
                {
                    offload = genOffloadDto(
                        id: reader.GetInt64(0).ToString(),
                        town: reader.GetString(3),
                        state: reader.GetString(4),
                        landingdate: reader.GetDateTime(2),
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
            _connection.Close();
            return offload;
        }

        public List<BoatSimpleDto> SearchForBoat(string boatSearchTerm, int count, int Offset)
        {
            var searchedBoats = new List<BoatSimpleDto>();
            var cmd = _connection.CreateCommand();
            _connection.Open();
            cmd.CommandText = string.Format(
                @"  SELECT 
                    Aflinn_Boats.`Registreringsmerke (seddel)`,
                    Aflinn_Boats.`Radiokallesignal (seddel)`,
                    Aflinn_Landings.`Fartøynavn`,
                    Aflinn_Boats.`Fartøyfylke`,
                    Aflinn_Boats.`Fartøynasjonalitet`,
                    Aflinn_Boats.`Fartøykommune`,
                    Aflinn_Boats.`Største lengde`,
                    Aflinn_Fishing_gear.`Redskap`
                    FROM Aflinn_Landings

                    LEFT JOIN Aflinn_Boats ON Aflinn_Boats.`Registreringsmerke (seddel)` = Aflinn_Landings.`Registreringsmerke (seddel)`
                    LEFT JOIN Aflinn_Fishing_gear ON Aflinn_Fishing_gear.`Redskap (kode)` = Aflinn_Landings.`Redskap (kode)`
                    WHERE Aflinn_Landings.`Fartøynavn` LIKE '{0}'
                    OR Aflinn_Boats.`Radiokallesignal (seddel)` LIKE '{0}'
                    OR Aflinn_Boats.`Registreringsmerke (seddel)` LIKE '{0}'
                    GROUP BY Aflinn_Boats.`Registreringsmerke (seddel)`
                    limit {1}
                    offset {2}
                    ",
                boatSearchTerm,
                count,
                Offset);
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
            _connection.Close();
            return searchedBoats;
        }
    }
}