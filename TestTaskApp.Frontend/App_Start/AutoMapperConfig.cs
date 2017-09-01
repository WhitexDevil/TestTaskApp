using AutoMapper;
using TestTaskApp.EntityFramework.Entities;
using TestTaskApp.Frontend.DTOs.Request;
using TestTaskApp.Frontend.DTOs.Response;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DbTestEntity, TestEntity>();
                cfg.CreateMap<TestEntity, DbTestEntity>()
                    .ForMember(e => e.CreatedDate, opt => opt.Ignore())
                    .ForMember(e => e.UpdatedDate, opt => opt.Ignore());

                cfg.CreateMap<TestEntity, TestEntityResponseDto>();
                cfg.CreateMap<TestEntityRequestDto, TestEntity>();
            });
        }
    }
}