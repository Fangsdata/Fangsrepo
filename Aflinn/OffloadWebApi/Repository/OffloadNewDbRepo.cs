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
            throw new System.NotImplementedException();
        }

        public List<OffloadDto> GetLastOffloadsFromBoat(string BoatRadioSignalId, int count)
        {
            throw new System.NotImplementedException();
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
                Aflinn_Boats.`Radiokallesignal (seddel)` as `boat_radio_signal_id`,
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
                WHERE Aflinn_Landings.`Dokumentnummer` = {0}", offloadId);
            var reader = cmd.ExecuteReader();
            using(reader)
            {
                reader.Read();
                if(reader.HasRows)
                {
                    offload.Id = reader.GetInt64(0).ToString();
                    offload.Town = reader.GetString(3); 
                    offload.State = reader.GetString(4); 
                    offload.LandingDate = reader.GetDateTime(2);
                    offload.TotalWeight = reader.GetFloat(23);

                    offload.Boat = new BoatSimpleDto
                    {
                        Registration_id = reader.GetString(5),
                        RadioSignalId = reader.GetString(13),
                        Name = reader.GetString(6),
                        FishingGear = reader.GetString(24)
                    };

                    offload.MapData = new List<MapDataDto>();
                    offload.MapData.Add(new MapDataDto
                    {
                        Latitude = reader.GetDouble(8),
                        Longitude = reader.GetDouble(7),
                    });
                    
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

        public List<BoatSimpleDto> SearchForBoat(string boatSearchTerm, int count, int pageNo)
        {
            throw new System.NotImplementedException();
        }
    }
}