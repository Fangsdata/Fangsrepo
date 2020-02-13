namespace OffloadWebApi.Models.Dtos
{
    public class BoatSimpleDto
    {
        public int Id { get; set; }

        public string Registration_id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public string Nationality { get; set; }

        public int Length { get; set; }

        public string FishingGear { get; set; }

        public string Image { get; set; }
    }
}