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

        public int GetBoatWeight(int boatWeight1, int boatWeight2)
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

        public OffloadDetailDto GetOffloadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public OffloadDto GetSingleOffload(string offloadId)
        {
            throw new System.NotImplementedException();
        }

        public List<BoatSimpleDto> SearchForBoat(string boatSearchTerm)
        {
            throw new System.NotImplementedException();
        }
    }
}