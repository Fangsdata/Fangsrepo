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
    public class OffloadOldDbRepo : MustConstruct<string>, IOffloadRepo 
    {
        private MySqlConnection _connection { get; }
        public OffloadOldDbRepo(string connectionString) 
        : base(connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        } 

        private async Task<List<TopListEntity>> getFilterResultAsync()
        {
            using var cmd = _connection.CreateCommand();
            _connection.Open();
            cmd.CommandText = @"SELECT Fartøynavn, `Registreringsmerke (seddel)`,Tosk FROM eskoy.Afli_Alle_2019_Pivot_Pretty order by Torsk desc limit 10;";

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
            var result = getFilterResultAsync();
            result.Wait();
            var entity = result.Result;

            var dto = new List<TopListDto>();

            for(int i = 0; i < entity.Count; i++)
            {
                dto.Add(new TopListDto()
                {
                    BoatName = "hallo heimur"
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