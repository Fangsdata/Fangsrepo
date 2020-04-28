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
        private double parseStrToDouble(string input)
        {
            try
            {
               return double.Parse(input.Replace(',', '.'), CultureInfo.InvariantCulture);
            }
            catch (System.Exception e) 
            {
                Console.WriteLine(e);
                Console.WriteLine("--- could not parse " + input);
            }
            return 0;
        }       

        private string AddFilter<T>(List<T> filters, bool firstFilter, string filterName, bool commas = true)
        {
            string retStr = string.Empty;
            bool firstItem = true;
            for(int i = 0; i < filters.Count; i++)
            {
                if(firstItem)
                {
                    firstItem = false;
                    if(firstFilter)
                    {
                        retStr += "WHERE (" + filterName + " = '" + filters[i] + "'";
                    }
                    else
                    {
                        retStr += " And (" + filterName + " = '" + filters[i] + "'";
                    }
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
        private async Task<List<TopListEntity>> getFilterResultAsync(QueryOffloadsInput filters)
        {
            if(filters.Month != null)
            {
                for(int i = 0; i < filters.Month.Count; i++)
                {
                    Console.WriteLine(filters.Month[i]);
                }     
            }
            if(filters.Year != null)
            {
                for(int i = 0; i < filters.Year.Count; i++)
                {
                    Console.WriteLine(filters.Year[i]);
                }
            }
            using var cmd = _connection.CreateCommand();
            cmd.CommandTimeout = 90;
            _connection.Open();
            cmd.CommandText = @"SELECT 
	                            CAST(boat_name AS CHAR(20)) as 'Línubátar nafn' , 
                                CAST(boat_regestration_id AS CHAR(20)) as 'Línubátar',
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
            bool firstFilter = true;

            if(filters.BoatLength != null)
            {  
                firstFilter = false;
                string lenStr = string.Format(" WHERE (boat_length BETWEEN {0} AND {1})", filters.BoatLength[0], filters.BoatLength[1]);
                cmd.CommandText = cmd.CommandText + lenStr;
            }
            if(filters.Month != null)
            {
                cmd.CommandText = cmd.CommandText + AddFilter(filters.Month, firstFilter, "landing_month", false);
                firstFilter = false;
            }
            if(filters.FishingGear != null)
            {
                cmd.CommandText = cmd.CommandText + AddFilter(filters.FishingGear, firstFilter, "fishing_gear");
                firstFilter = false;
            }
            if(filters.FishName != null)
            {
                cmd.CommandText = cmd.CommandText + AddFilter(filters.FishName, firstFilter, "fish_name");
                firstFilter = false;
            }
            if(filters.LandingState != null)
            {
                cmd.CommandText = cmd.CommandText + AddFilter(filters.LandingState, firstFilter, "landing_state");
                firstFilter = false; 
            }
            if(filters.Year != null)
            {
                bool is2016 = false;
                for(int i = 0; i < filters.Year.Count; i++)
                {
                    if (filters.Year[i] == 2016)
                    {
                        is2016 = true;
                    }
                } 
                if(!is2016)
                {            
                    _connection.Close();
                    return new List<TopListEntity>();
                }
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
                    try
                    {
                        item.landingDate = reader.GetDateTime(25);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("----CANT MAPP ITEM----");
                    }
                    items.Add(item);
                }
            }
            return items;
        }

        private async Task<BoatEntity> getBoatFromDb(string BoatRadioSignalId)
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandTimeout = 90;
            _connection.Open();
            cmd.CommandText = 
                @"SELECT
                    boat_id,
                    boat_regestration_id,
                    boat_radio_signal_id,
                    boat_name,
                    boat_town_id,
                    boat_state_id,
                    boat_length,
                    boat_weight_1969,
                    boat_weight,
                    boat_built_year,
                    engine_power,
                    fishing_gear
                FROM eskoy.englishVersion 
                where boat_radio_signal_id='" + BoatRadioSignalId + "' LIMIt 1;";
            var res = await this.readboatFromDb(await cmd.ExecuteReaderAsync());
            _connection.Close();
            return res; 
        }
        private async Task<BoatEntity> readboatFromDb(DbDataReader reader)
        {
            using (reader)
            {
                await reader.ReadAsync();

                if(reader.HasRows)
                {
                    return new BoatEntity()
                    {
                        RegistrationId = reader.GetString(1),
                        RadioSignalId = reader.GetString(2),
                        Name = reader.GetString(3),
                        Town = reader.GetString(4),
                        State = reader.GetString(5),
                        Length = reader.GetString(6),
                        Weight = reader.GetString(7),
                        weight_small_boat = reader.GetString(8),
                        BuiltYear = reader.GetString(9),
                        EnginePower = reader.GetString(10),
                        FishingGear = reader.GetString(11),
                    };
                }
                else
                {
                    return null;
                }
            }
            throw new System.NotImplementedException();
        }
        public BoatDto GetBoatByRadioSignal(string BoatRadioSignalId)
        {
            var result = getBoatFromDb(BoatRadioSignalId);
            result.Wait();
            var entity = result.Result;
            if(entity == null)
            {
                return null;
            }
            var dto = new BoatDto() 
            {
                   RegistrationId = entity.RegistrationId,
                   RadioSignalId = entity.RadioSignalId,
                   Name = entity.Name,
                   Town = entity.Town,
                   State = entity.State,
                   FishingGear = entity.FishingGear,
            };
            dto.Length = parseStrToDouble(entity.Length);
            try
            {
                dto.Weight = int.Parse(entity.Weight);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Weight not or parsed: " + entity.Weight);
            }
            try
            {
                dto.Weight = int.Parse(entity.weight_small_boat);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Weight not or parsed: " + entity.weight_small_boat);
            }
            try
            {
                dto.BuiltYear = int.Parse(entity.BuiltYear);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("BuiltYear not or parsed: " + entity.BuiltYear);
            }
            try
            {
                dto.EnginePower = int.Parse(entity.EnginePower);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("EnginePower not or parsed: " + entity.EnginePower);
            }
            return dto; 
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
                    BoatFishingGear = entity[i].fishingGear,
                    BoatLength = entity[i].boatLength,
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

        private async Task<List<OffloadEntity>> getOffloads(string BoatRadioSignalId, int count)
        {
            using var cmd = _connection.CreateCommand();
            _connection.Open();
            count = 12 * count;
            string commandString = string.Format(
                                    @"SELECT *
                                    FROM eskoy.englishVersion
                                    WHERE boat_radio_signal_id='{0}'
                                    ORDER BY landing_id DESC
                                    LIMIT {1};",
                                    BoatRadioSignalId,
                                    count);
            cmd.CommandText = commandString;
            var res = await this.readOfflads(await cmd.ExecuteReaderAsync());
            _connection.Close();
            return res;
        }

        private async Task<List<OffloadEntity>> readOfflads(DbDataReader reader)
        {
            using (reader)
            {
                var offloads = new List<OffloadEntity>();
                string lastRowId = string.Empty;
                bool hasInit = false;
                OffloadEntity offload = null;
                while(await reader.ReadAsync())
                {
                    string rowId = reader.GetString(38);
                    string rowTown = reader.GetString(4);
                    string rowState = reader.GetString(6);
                    DateTime rowLandingDate = DateTime.Now;
                    int totalWeight = 0;

                    int rowFishId = int.Parse(reader.GetString(26));
                    string rowFishType = reader.GetString(27);
                    string rowFishCondition = reader.GetString(29);
                    string rowFishPackaging = reader.GetString(33);
                    string rowFisQuality = reader.GetString(35);
                    string rowFishPreservation = reader.GetString(31);
                    string rowFishApplycation = reader.GetString(36);
                    float rowFishWeight = 0;
                    try
                    {
                        rowFishWeight = float.Parse(reader.GetString(37));
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e);  
                        Console.WriteLine("--- Canot parse weight for" + reader.GetString(37));  
                    }

                    if(lastRowId != rowId && 
                        offloads != null &&
                        hasInit)
                    {
                        float tempTotalWeight = 0f;
                        for(int i = 0; i < offload.Fish.Count; i++)
                        {
                            tempTotalWeight += offload.Fish[i].Weight;
                        }
                        offload.TotalWeight = tempTotalWeight;
                        offloads.Add(offload);
                        offload = null;
                        hasInit = false;
                    }
                    lastRowId = rowId;
                    if(!hasInit)
                    {
                        hasInit = true;
                        offload = new OffloadEntity
                        {
                            Id = rowId,
                            Town = rowTown,
                            State = rowState,
                            LandingDate = rowLandingDate,
                            TotalWeight = totalWeight,
                            Fish = new List<FishDto>()
                        };
                        var fish = new FishDto
                        {
                            Id = rowFishId,
                            Type = rowFishType,
                            Condition = rowFishCondition,
                            Preservation = rowFishPreservation,
                            Packaging = rowFishPackaging,
                            Quality = rowFisQuality,
                            Application = rowFishApplycation,
                            Weight = rowFishWeight
                        };
                        offload.Fish.Add(fish);
                    }
                    else
                    {
                        var fish = new FishDto
                        {
                            Id = rowFishId,
                            Type = rowFishType,
                            Condition = rowFishCondition,
                            Preservation = rowFishPreservation,
                            Packaging = rowFishPackaging,
                            Quality = rowFisQuality,
                            Application = rowFishApplycation,
                            Weight = rowFishWeight
                        };
                        offload.Fish.Add(fish);
                    }
                }
                return offloads;
            }
            throw new System.NotImplementedException();
        }

        public List<OffloadDto> GetLastOffloadsFromBoat(string BoatRadioSignalId, int count)
        {
            var result = getOffloads(BoatRadioSignalId, count);
            result.Wait();
            var entity = result.Result;
            if (entity == null)
            {
                return null;
            }
            var dto = new List<OffloadDto>();
            if(count > entity.Count)
            {
                count = entity.Count;
            }
            for(int i = 0; i < count; i++)
            {
                var item = new OffloadDto
                {
                    Id = entity[i].Id,
                    Town = entity[i].Town,
                    State = entity[i].State,
                    LandingDate = entity[i].LandingDate,
                    TotalWeight = entity[i].TotalWeight,
                    Fish = entity[i].Fish
                };
                dto.Add(item);
            }
            return dto;
        }
        private async Task<OffloadEntity> getOffload(string OffloadId)
        {
            using var cmd = _connection.CreateCommand();
            _connection.Open();
            string commandString = string.Format(
                                    @"SELECT *
                                    FROM eskoy.englishVersion
                                    WHERE landing_id='{0}'
                                    ORDER BY landing_id DESC;",
                                    OffloadId);
            cmd.CommandText = commandString;
            var res = await this.readOffload(await cmd.ExecuteReaderAsync());
            _connection.Close();
            return res;
        }
        private async Task<OffloadEntity> readOffload(DbDataReader reader)
        {
            using (reader)
            {
                OffloadEntity offload = null;
                bool hasInit = false;

                while(await reader.ReadAsync())
                {
                    string rowId = reader.GetString(38);
                    string rowTown = reader.GetString(4);
                    string rowState = reader.GetString(6);
                    DateTime rowLandingDate = DateTime.Now;
                    int totalWeight = 0;
                    string rowBoatRadioSignal = reader.GetString(9);
                    int rowBoatId = int.Parse(reader.GetString(7));
                    string rowRegistrationId = reader.GetString(8);
                    string rowBoatName = reader.GetString(10);
                    string rowBoatState = reader.GetString(6);
                    string rowBoatNat = reader.GetString(13);
                    double rowBoatLength = 0;
                    string rowBoatFishingGear = reader.GetString(20);
                    int rowFishId = int.Parse(reader.GetString(26));
                    string rowFishType = reader.GetString(27);
                    string rowFishCondition = reader.GetString(29);
                    string rowFishPackaging = reader.GetString(33);
                    string rowFisQuality = reader.GetString(35);
                    string rowFishPreservation = reader.GetString(31);
                    string rowFishApplycation = reader.GetString(36);
                    float rowFishWeight = 0;

                    double rowLatitude = parseStrToDouble(reader.GetString(22));
                    double rowLongitude = parseStrToDouble(reader.GetString(21));

                    rowBoatLength = parseStrToDouble(reader.GetString(15));
                    if(rowBoatLength == 0)
                    {
                        rowBoatLength = parseStrToDouble(reader.GetString(14));
                    }
                    try
                    {
                        rowFishWeight = float.Parse(reader.GetString(37));
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e);  
                        Console.WriteLine("--- Canot parse weight for" + reader.GetString(37));  
                    }

                    if(!hasInit)
                    {
                        hasInit = true;
                        offload = new OffloadEntity
                        {
                            Id = rowId,
                            Town = rowTown,
                            State = rowState,
                            LandingDate = rowLandingDate,
                            TotalWeight = totalWeight,
                            Fish = new List<FishDto>(),
                            Boat = new BoatSimpleDto
                            {
                                Id = rowBoatId,
                                Registration_id = rowRegistrationId,
                                RadioSignalId = rowBoatRadioSignal,
                                Name = rowBoatName,
                                State = rowBoatState,
                                Nationality = rowBoatNat,
                                Length = rowBoatLength,
                                FishingGear = rowBoatFishingGear,
                                Image = string.Empty
                            },
                            MapData = new List<MapDataDto>()
                        };
                        var mapEntry = new MapDataDto
                        {
                            Longitude = rowLongitude,
                            Latitude = rowLatitude,
                            Time = rowLandingDate
                        };
                        offload.MapData.Add(mapEntry);

                        var fish = new FishDto
                        {
                            Id = rowFishId,
                            Type = rowFishType,
                            Condition = rowFishCondition,
                            Preservation = rowFishPreservation,
                            Packaging = rowFishPackaging,
                            Quality = rowFisQuality,
                            Application = rowFishApplycation,
                            Weight = rowFishWeight
                        };
                        offload.Fish.Add(fish);
                    }
                    else
                    {
                        var fish = new FishDto
                        {
                            Id = rowFishId,
                            Type = rowFishType,
                            Condition = rowFishCondition,
                            Preservation = rowFishPreservation,
                            Packaging = rowFishPackaging,
                            Quality = rowFisQuality,
                            Application = rowFishApplycation,
                            Weight = rowFishWeight
                        };
                        offload.Fish.Add(fish);
                    }
                }
                float tempTotalWeight = 0f;
                for(int i = 0; i < offload.Fish.Count; i++)
                {
                    tempTotalWeight += offload.Fish[i].Weight;
                }
                offload.TotalWeight = tempTotalWeight;
                return offload;
            }
            throw new System.NotImplementedException();
        }
        public OffloadDto GetSingleOffload(string offloadId)
        {
            var result = getOffload(offloadId);
            result.Wait();
            var entity = result.Result;
            if (entity == null)
            {
                return null;
            }

            var dto = new OffloadDto
            {
                Id = entity.Id,
                Town = entity.Town,
                State = entity.State,
                LandingDate = entity.LandingDate,
                TotalWeight = entity.TotalWeight,
                Fish = entity.Fish,
                Boat = entity.Boat,
                MapData = entity.MapData
            };

            return dto;
        }

        private async Task<List<BoatSimpleDto>> GetSearchForBoat(string boatSearchTerm)
        {
            using var cmd = _connection.CreateCommand();
            _connection.Open();
            string commandString = string.Format(
                                    @"SELECT boat_regestration_id, boat_radio_signal_id, 
                                             boat_name, boat_state_id, boat_nationality_id, 
                                             boat_town_id,boat_length, fishing_gear
                                    FROM eskoy.englishVersion
                                    WHERE boat_name LIKE '%{0}%'
                                    OR boat_radio_signal_id LIKE '%{0}%'
                                    OR boat_regestration_id LIKE '%{0}%'
                                    LIMIT 15",
                                    boatSearchTerm);
            cmd.CommandText = commandString;
            var res = await this.readBoatSearch(await cmd.ExecuteReaderAsync());
            _connection.Close();
            return res; 
        }
        private async Task<List<BoatSimpleDto>> readBoatSearch(DbDataReader reader)
        {
            var boats = new List<BoatSimpleDto>();
            using (reader)
            {
                while(await reader.ReadAsync())
                {
                    boats.Add(new BoatSimpleDto
                    {
                        Registration_id = reader.GetString(0),
                        RadioSignalId = reader.GetString(1),
                        Name = reader.GetString(2),
                        State = reader.GetString(3),
                        Nationality = reader.GetString(4),
                        Town = reader.GetString(5),
                        Length = parseStrToDouble(reader.GetString(6)),
                        FishingGear = reader.GetString(7)
                    });
                }
            }
            return boats;
        }

        #nullable enable
        public List<BoatSimpleDto>? SearchForBoat(string boatSearchTerm)
        {
            var result = GetSearchForBoat(boatSearchTerm);
            result.Wait();
            return result.Result;
        }
    }
}