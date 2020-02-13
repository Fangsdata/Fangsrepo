using System.Collections.Generic;

namespace OffloadWebApi.Models.Dtos
{
    public class BoatDto
    {
        public int Id { get; set; }

        public string RegistrationId { get; set; }

        public string RadioSignalId { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public string Town { get; set; }

        public double Length { get; set; }

        public int Weight { get; set; }

        public int BuiltYear { get; set; }

        public int EnginePower { get; set; }

        public string FishingGear { get; set; }

        public string Image { get; set; }

        public List<MapDataDto> MapData { get; set; }
    }
}