using System;
using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Services
{
    public interface IOffloadService
    {
        public OffloadDto GetOffloads();

        public OffloadDetailDto GetOffloadById(int id);
    }
}
