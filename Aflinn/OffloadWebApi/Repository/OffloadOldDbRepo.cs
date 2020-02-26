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
            var sizeOfList = filters.FishingGear.Count;
            Console.WriteLine(sizeOfList);
            using var cmd = _connection.CreateCommand();
            _connection.Open();
            cmd.CommandText = @"SELECT 

                                CAST(boat_regestration_id AS CHAR(20)) as 'Línubátar',
	                            CAST(boat_name AS CHAR(20)) as 'Línubátar nafn' , 
                                SUM(CONVERT(CAST(fish_weight as CHAR(20)), UNSIGNED)) as 'Afl í kg'

                                FROM englishVersion ";
            if(filters.FishingGear.Count > 0)
            {
                cmd.CommandText = cmd.CommandText + "WHERE fishing_gear = ";
                for(var i = 0; i < sizeOfList; i++)
                {
                    cmd.CommandText = cmd.CommandText + filters.FishingGear[i];
                    Console.WriteLine(filters.FishingGear[i]);
                    if((i + 1) < sizeOfList)
                    {
                        cmd.CommandText = cmd.CommandText + " OR fishing_gear = ";
                    }
                }
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