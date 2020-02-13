using System;
using OffloadWebApi.Models.Dtos;

namespace OffloadWebApi.Repository
{
    public interface IOffloadRepo
    {
        public OffloadDetailDto? GetOffloadById(int id);
    }
}