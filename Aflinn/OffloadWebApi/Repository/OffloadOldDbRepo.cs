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
                                SUM(CONVERT(CAST(fish_weight as CHAR(20)), UNSIGNED)) as 'Afl í kg',
                                CAST(fish_utilization AS CHAR(20)) as 'Fish Utilization',
                                CAST(document_sales_id AS CHAR (20)) as 'Document sales id', 
                                CAST(buyer_id AS CHAR (20)) as 'Buyer id', 
                                CAST(buyer_nationality AS CHAR (20)) as 'Buyer nationality', 
                                CAST(landing_town_id AS CHAR (20)) as 'Landing town id', 
                                CAST(landing_town AS CHAR (20)) as 'Landing town', 
                                CAST(landing_state_id AS CHAR (20)) as 'Landing state id', 
                                CAST(landing_state AS CHAR (20)) as 'Landing state name', 
                                CAST(boat_id AS CHAR (20)) as 'Boat id', 
                                CAST(boat_radio_signal_id AS CHAR (20)) as 'Boat radio signal id', 
                                CAST(boat_town_id AS CHAR (20)) as 'Boat town id', 
                                CAST(boat_state_id AS CHAR (20)) as 'Boat state id', 
                                CAST(boat_nationality_id AS CHAR (20)) as 'Boat nationality id', 
                                CONVERT(CAST(boat_length AS CHAR (20)), FLOAT) as 'Boat length', 
                                CAST(boat_weight_1969 AS CHAR (20)) as 'Boat weight 1969', 
                                CAST(boat_weight AS CHAR (20)) as 'Boat weight', 
                                CAST(boat_built_year AS CHAR (20)) as 'Boat built year', 
                                CAST(engine_power AS CHAR (20)) as 'Engine power', 
                                CAST(fishing_gear_id AS CHAR (20)) as 'Fishing gear id', 
                                CAST(fishing_gear AS CHAR (20)) as 'Fishing gear', 
                                CAST(Longitude AS CHAR (20)) as 'Longitute', 
                                CAST(Latitude AS CHAR (20)) as 'Latitude', 
                                STR_TO_DATE(CONCAT(SUBSTRING(CAST(landing_date AS CHAR (20)), 1, 2), ',' , CAST(landing_month AS CHAR (20)), ',',SUBSTRING(CAST(landing_date AS CHAR (20)), 7, 10)), '%d,%m,%Y') AS 'Landing date', 
                                CAST(landing_time AS CHAR (20)) as 'Landing time', 
                                CAST(landing_month AS CHAR (20)) as 'Landing month', 
                                CAST(fish_id AS CHAR (20)) as 'Fish id', 
                                CAST(fish_name AS CHAR (20)) AS 'Fish name',
                                CAST(fish_condition_id AS CHAR (20)) AS 'Fish condition id',
                                CAST(fish_condition AS CHAR (20)) AS 'Fish condition',
                                CAST(fish_preservation_method_id AS CHAR (20)) AS 'Fish preservation method id',
                                CAST(fish_preservation_method AS CHAR (20)) AS 'Fish preservation method',
                                CAST(packaging_id AS CHAR (20)) AS 'Packaging id',
                                CAST(packaging AS CHAR (20)) AS 'Packaging',
                                CAST(fish_quality_id AS CHAR (20)) AS 'Fish quality id',
                                CAST(fish_quality AS CHAR (20)) AS 'Fish quality',
                                CAST(fish_utilization AS CHAR (20)) AS 'Fish utilization',
                                CAST(landing_id AS CHAR (20)) AS 'Landing id',
                                COUNT(landing_id)/COUNT(DISTINCT landing_month) AS 'Average landings per month',
                                SUM(CONVERT(CAST(fish_weight as CHAR(20)), UNSIGNED))/COUNT(DISTINCT landing_month) as 'Meðal afl í kg á mánuði'

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
                for(var i = 0; i < filters.BoatLength.Count; i++)
                {
                    cmd.CommandText = cmd.CommandText + " AND (boat_length BETWEEN ";
                    cmd.CommandText = cmd.CommandText + filters.BoatLength[i] + " AND " + filters.BoatLength[i + 1];
                    i = i + 1;
                    cmd.CommandText = cmd.CommandText + ") ";
                }
                Console.WriteLine(cmd.CommandText);
            }

            // Hér fyrir neðan ef við erum ekki með neina filteringu á fishing gear en við erum með filteringu á boatlength þá gerist þetta
            if(filters.FishingGear.Count == 0 && filters.BoatLength.Count > 0)
            {   
                cmd.CommandText = cmd.CommandText + " WHERE (boat_length BETWEEN ";
                for(var i = 0; i < filters.BoatLength.Count; i++)
                {
                    if(i >= 2)
                    {
                        cmd.CommandText = cmd.CommandText + " AND (boat_length BETWEEN ";
                    }
                    cmd.CommandText = cmd.CommandText + filters.BoatLength[i].ToString() + " AND " + filters.BoatLength[i + 1].ToString();
                    i = i + 1;
                    cmd.CommandText = cmd.CommandText + ") ";
                }
                Console.WriteLine(cmd.CommandText);
            }

            // Her fyrir nedan er filtering fyrir fishname
            if((filters.FishingGear.Count > 0 || filters.BoatLength.Count > 0) && filters.FishName.Count > 0)
            {
                cmd.CommandText = cmd.CommandText + " AND (fish_name = ";
                for(var i = 0; i < filters.FishName.Count; i++)
                { 
                    cmd.CommandText = cmd.CommandText + filters.FishName[i];
                    if((i + 1) < filters.FishName.Count)
                    {
                        cmd.CommandText = cmd.CommandText + " AND fishing_gear = ";
                    }
                }
                cmd.CommandText = cmd.CommandText + ") ";
                Console.WriteLine(cmd.CommandText);
            }
            if((filters.FishingGear.Count == 0 && filters.BoatLength.Count == 0) && filters.FishName.Count > 0)
            {
                cmd.CommandText = cmd.CommandText + " WHERE (fish_name = ";
                for(var i = 0; i < filters.FishName.Count; i++)
                { 
                    cmd.CommandText = cmd.CommandText + filters.FishName[i];
                    if((i + 1) < filters.FishName.Count)
                    {
                        cmd.CommandText = cmd.CommandText + " AND fishing_gear = ";
                    }
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
                        totalWeight = reader.GetDouble(2),
                        fishUtilization = reader.GetString(3),
                        documentSalesId = reader.GetString(4),
                        buyerId = reader.GetString(5),
                        buyerNationality = reader.GetString(6),
                        landingTownId = reader.GetString(7),
                        landingTown = reader.GetString(8),
                        landingStateId = reader.GetString(9),
                        landingState = reader.GetString(10),
                        boatId = reader.GetString(11),
                        boatRadioSignalId = reader.GetString(12),
                        boatTownId = reader.GetString(13),
                        boatStateId = reader.GetString(14),
                        boatNationalityId = reader.GetString(15),
                        boatLength = reader.GetDouble(16),
                        boatWeight1969 = reader.GetString(17),
                        boatWeight = reader.GetString(18),
                        boatBuiltYear = reader.GetString(19),
                        enginePower = reader.GetString(20),
                        fishingGearId = reader.GetString(21),
                        fishingGear = reader.GetString(22),
                        longitude = reader.GetString(23),
                        latitude = reader.GetString(24), 
                        landingDate = reader.GetDateTime(25),
                        landingTime = reader.GetString(26),
                        landingMonth = reader.GetString(27),
                        fishId = reader.GetString(28),
                        fishName = reader.GetString(29),
                        fishConditionId = reader.GetString(30),
                        fishCondition = reader.GetString(31),
                        fishPreservationMethodId = reader.GetString(32),
                        fishPreservationMethod = reader.GetString(33),
                        packagingId = reader.GetString(34),
                        packaging = reader.GetString(35),
                        fishQualityId = reader.GetString(36),
                        fishQuality = reader.GetString(37),
                        landingId = reader.GetString(38),
                        averageTrips = reader.GetString(39),
                        avrage = reader.GetDouble(40),
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
                    // ID!!!???
                    BoatName = entity[i].boatName,
                    TotalWeight = entity[i].totalWeight,
                    BoatRegistrationId = entity[i].registrationId,
                    Town = entity[i].landingTown,
                    State = entity[i].landingState,
                    LandingDate = entity[i].landingDate,
                    BoatRadioSignalId = entity[i].boatRadioSignalId,
                    BoatNationality = entity[i].boatNationalityId,

                    // medal thyngd afla per londun
                    Avrage = entity[i].avrage,

                    // medl fjoldi ferda a manudi
                    
                    // AverageTrips = entity[i].averageTrips,

                    // smallest er minnsta londunin

                    // largest landing er bara staersta

                    // trips er fjoldi landana
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