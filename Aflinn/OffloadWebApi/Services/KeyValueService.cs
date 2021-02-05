using OffloadWebApi.Repository;

namespace OffloadWebApi.Services
{
    public class KeyValueService : IKeyValueService
    {
        private IOffloadRepo _offloadRepo;

        public KeyValueService(IOffloadRepo offloadRepo)
        {
            this._offloadRepo = offloadRepo;
        }

        public string GetValue(string key)
        {
            return _offloadRepo.GetValue(key);
        }
    }
}