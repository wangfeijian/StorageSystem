using AutoMapper;
using StorageSystem.Shared.Context;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.API.Extensions
{
    public class AutoMapperProFile : Profile
    {
        public AutoMapperProFile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<StorageStatus, StorageStatusDto>().ReverseMap();
            CreateMap<StorageDetail, StorageDetailDto>().ReverseMap();
            CreateMap<StorageOutDetail, StorageOutDetailDto>().ReverseMap();
        }
    }
}
