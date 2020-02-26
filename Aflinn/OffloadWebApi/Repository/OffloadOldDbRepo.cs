using System.Collections.Generic;
using OffloadWebApi.Models.Dtos;
using OffloadWebApi.Models.InputModels;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using OffloadWebApi.Models.EntityModels;
using System.Data.Common;
using System;

namespace OffloadWebApi.Repository
{
    public class OffloadOldDbRepo : IOffloadRepo 
    {
        private MySqlConnection _connection { get; }
        public OffloadOldDbRepo(string connectionString) 
        {
            _connection = new MySqlConnection(connectionString);
        } 

        private async Task<List<TopListEntity>> getFilterResultAsync(QueryOffloadsInput filters)
        {
            using var cmd = _connection.CreateCommand();
            _connection.Open();
            cmd.CommandText = @"SELECT 

                                CAST(boat_regestration_id AS CHAR(20)) as 'Línubátar',
	                            CAST(boat_name AS CHAR(20)) as 'Línubátar nafn' , 
                                SUM(CONVERT(CAST(fish_weight as CHAR(20)), UNSIGNED)) as 'Afl í kg'

                                FROM englishVersion ";

                                // Ef filtering á við fishinggear, s.s. ef fishing gear filtering er til staðar þá fer það hingað.
            if(filters.FishingGear.Count > 0) 
            {
                cmd.CommandText = cmd.CommandText + "WHERE (fishing_gear = ";
                for(var i = 0; i < filters.FishingGear.Count; i++)
                {
                    cmd.CommandText = cmd.CommandText + filters.FishingGear[i];
                    Console.WriteLine(filters.FishingGear[i]);
                    if((i + 1) < filters.FishingGear.Count)
                    {
                        cmd.CommandText = cmd.CommandText + " OR fishing_gear = ";
                    }
                }
                cmd.CommandText = cmd.CommandText + ") ";
                Console.WriteLine(cmd.CommandText);
            }
            Console.WriteLine("Boatlength count: " + filters.BoatLength.Count);
            Console.WriteLine("Fishing gear count: " + filters.FishingGear.Count);
            
            // Her fyrir neðan er ef við erum með filteringu á fishinggear og boatlength þá gerist þetta
            if(filters.FishingGear.Count > 0 && filters.BoatLength.Count > 0)
            {   
                cmd.CommandText = cmd.CommandText + " AND (boat_length BETWEEN ";
                for(var i = 0; i < filters.BoatLength.Count; i++)
                {
                    cmd.CommandText = cmd.CommandText + filters.BoatLength[i] + " AND " + filters.BoatLength[i + 1];
                    i = i + 1;
                }
                cmd.CommandText = cmd.CommandText + ") ";
                Console.WriteLine(cmd.CommandText);
            }

            // Hér fyrir neðan ef við erum ekki með neina filteringu á fishing gear en við erum með filteringu á boatlength þá gerist þetta
            if(filters.FishingGear.Count == 0 && filters.BoatLength.Count > 0)
            {   
                cmd.CommandText = cmd.CommandText + " WHERE (boat_length BETWEEN ";
                for(var i = 0; i < filters.BoatLength.Count; i++)
                {
                    cmd.CommandText = cmd.CommandText + filters.BoatLength[i] + " AND " + filters.BoatLength[i + 1];
                    i = i + 1;
                }
                cmd.CommandText = cmd.CommandText + ") ";
                Console.WriteLine(cmd.CommandText);
            }

            cmd.CommandText = cmd.CommandText + " GROUP BY CAST(boat_regestration_id AS CHAR(20)), CAST(boat_name AS CHAR(20)) ORDER BY SUM(CONVERT(CAST(fish_weight as CHAR(20)), UNSIGNED)) DESC LIMIT " + filters.Count + ";";

                                // ....where ... fishing_gear = 'Autoline' OR fishing_gear = 'Andreline' OR fishing_gear = 'Juksa/pilk' OR fishing_gear = 'Flyteline'
            var res = await this.ReadAllAsync(await cmd.ExecuteReaderAsync());
            _connection.Close();
            return res;
        }
        private async Task<List<TopListEntity>> ReadAllAsync(DbDataReader reader)
        {
            var items = new List<TopListEntity>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var item = new TopListEntity()
                    {
                        boatName = reader.GetString(0),
                        registrationId = reader.GetString(1),
                        totalWeight = reader.GetDouble(2)
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        public BoatDto GetBoatByRadioSignal(string BoatRadioSignalId)
        {
            throw new System.NotImplementedException();
        }

        public List<TopListDto> GetFilteredResults(QueryOffloadsInput filters)
        {
            var result = getFilterResultAsync(filters);
            result.Wait();
            var entity = result.Result;

            var dto = new List<TopListDto>();

            for(int i = 0; i < entity.Count; i++)
            {
                dto.Add(new TopListDto()
                {
                    BoatName = entity[i].boatName,
                    TotalWeight = entity[i].totalWeight,
                    BoatRadioSignalId = entity[i].registrationId
                });
            }
            return dto;
        }

        public OffloadDetailDto GetOffloadById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}