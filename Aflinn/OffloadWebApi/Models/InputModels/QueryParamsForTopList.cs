namespace OffloadWebApi.Models.InputModels
{
    public class QueryParamsForTopList
    {
        public string count { get; set; }
        public string fishingGear { get; set; }
        public string boatLength { get; set; }
        public string landingTown { get; set; }
        public string landingState { get; set; }
        public string month { get; set; }
        public string year { get; set; }
    }
}