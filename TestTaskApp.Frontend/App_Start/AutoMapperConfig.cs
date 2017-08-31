using AutoMapper;
using TestTaskApp.EntityFramework.Entities;
using TestTaskApp.Frontend.Dto.Request;
using TestTaskApp.Frontend.Dto.Response;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DbTestEntity, TestEntity>()
                .ReverseMap();
                cfg.CreateMap<TestEntity, TestEntityResponseDto>();
                cfg.CreateMap<TestEntityRequestDto, TestEntity>();
            });
        }
    }
}